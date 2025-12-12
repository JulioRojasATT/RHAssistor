using System;
using System.Collections.Generic;
using UnityEngine;

public class LipSyncRuntime : MonoBehaviour
{
    [Header("References")]
    public SkinnedMeshRenderer skinnedMesh;
    public AudioSource audioSource;

    [Header("Settings")]
    [Tooltip("JSON file (TextAsset) with phoneme timings.")]
    public TextAsset phonemeJson;

    [Tooltip("Map viseme name to blendshape index and multiplier in the format: viseme->(index,weight)")]
    public List<VisemeMapEntry> visemeMap = new List<VisemeMapEntry>();

    [Tooltip("Crossfade time (seconds) between visemes")]
    public float crossfade = 0.04f;

    // internal
    private PhonemeTimeline timeline;
    private List<VisemeKey> visemeKeys;

    private Dictionary<string, List<VisemeMapEntry>> visemeToBlend;
    void Awake()
    {
        if (skinnedMesh == null) skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        BuildMap();
        if (phonemeJson != null)
        {
            timeline = JsonUtility.FromJson<PhonemeTimeline>(phonemeJson.text);
            visemeKeys = BuildVisemeKeysFromPhonemes(timeline.phonemes);
        }
    }

    void BuildMap()
    {
        visemeToBlend = new Dictionary<string, List<VisemeMapEntry>>();
        foreach (VisemeMapEntry e in visemeMap)
        {
            if (!visemeToBlend.ContainsKey(e.viseme)) visemeToBlend[e.viseme] = new List<VisemeMapEntry>();
            visemeToBlend[e.viseme].Add(e);
        }
    }

    public void SetVisemeKeys(List<VisemeKey> visemeKeys)
    {
        this.visemeKeys = visemeKeys;
    }

    List<VisemeKey> BuildVisemeKeysFromPhonemes(List<PhonemeEntry> phonemes)
    {
        var keys = new List<VisemeKey>();
        foreach (var p in phonemes)
        {
            var vis = PhonemeToViseme(p.ph);
            if (string.IsNullOrEmpty(vis)) continue;
            keys.Add(new VisemeKey { viseme = vis, start = p.start, end = p.end });
        }
        // merge consecutive identical visemes
        for (int i = keys.Count - 1; i > 0; --i)
        {
            if (keys[i].viseme == keys[i - 1].viseme)
            {
                keys[i - 1].end = keys[i].end;
                keys.RemoveAt(i);
            }
        }
        return keys;
    }

    string PhonemeToViseme(string ph)
    {
        // basic mapping - expand as needed
        ph = ph.ToLowerInvariant();
        if (ph == "sil" || ph == "sp") return "Sil";
        if (ph == "p" || ph == "b" || ph == "m") return "MBP";
        if (ph == "f" || ph == "v") return "FF";
        if (ph == "th") return "TH";
        if (ph == "t" || ph == "d" || ph == "s" || ph == "z") return "SS";
        if (ph == "k" || ph == "g") return "kk";
        if (ph == "ch" || ph == "sh" || ph == "jh") return "CH";
        if (ph == "n" || ph == "l") return "NN";
        if (ph == "aa" || ph == "ah" || ph == "ae" || ph == "a") return "AH";
        if (ph == "ow" || ph == "o" || ph == "ao") return "OH";
        if (ph == "iy" || ph == "ih" || ph == "ee") return "EE";
        // fallback: if vowel-like -> AH
        if ("aeiou".IndexOf(ph[0]) >= 0) return "AH";
        return null;
    }    

    void Update()
    {
        if (audioSource == null || visemeKeys == null || skinnedMesh == null) return;
        if (audioSource.isPlaying)
        {
            float t = audioSource.time;
            ApplyVisemeAtTime(t);
        }
    }

    void ApplyVisemeAtTime(float t)
    {
        // zero all blends first (or optionally lerp from previous)
        // We'll zero all then apply weighted visemes around t
        int blendCount = skinnedMesh.sharedMesh.blendShapeCount;
        // optionally cache last weights for smoothing - for brevity we compute fresh
        for (int i = 0; i < blendCount; i++) skinnedMesh.SetBlendShapeWeight(i, 0f);

        // find active key(s)
        for (int i = 0; i < visemeKeys.Count; ++i)
        {
            var k = visemeKeys[i];
            float fadeIn = Mathf.InverseLerp(k.start - crossfade, k.start + crossfade, t);
            float fadeOut = 1f - Mathf.InverseLerp(k.end - crossfade, k.end + crossfade, t);
            float weight = Mathf.Clamp01(Mathf.Min(fadeIn, fadeOut));
            if (weight > 0.001f)
            {
                ApplyVisemeWeight(k.viseme, weight * 100f); // map 0..1 to 0..100 percent
            }
        }
    }

    void ApplyVisemeWeight(string viseme, float weightPercent)
    {
        if (!visemeToBlend.ContainsKey(viseme)) return;
        foreach (var entry in visemeToBlend[viseme])
        {
            float w = weightPercent * entry.weight;
            skinnedMesh.SetBlendShapeWeight(entry.blendShapeIndex, w);
        }
    }

    // helper to start playback using the timeline's audioFile (optional)
    public void PlayWithClip(AudioClip clip)
    {
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}