// VisemeAnimationBaker.cs (Editor)
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class VisemeAnimationBaker : EditorWindow
{
    public TextAsset phonemeJson;
    public SkinnedMeshRenderer skinnedMesh;
    public string clipPath = "Assets/lipsync_clip.anim";
    public List<LipSyncRuntime.VisemeMapEntry> mapping = new List<LipSyncRuntime.VisemeMapEntry>();
    public float crossfade = 0.04f;
    [MenuItem("Tools/Viseme Animation Baker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(VisemeAnimationBaker));
    }

    void OnGUI()
    {
        GUILayout.Label("Bake Viseme Animation", EditorStyles.boldLabel);
        phonemeJson = EditorGUILayout.ObjectField("Phoneme JSON", phonemeJson, typeof(TextAsset), false) as TextAsset;
        skinnedMesh = EditorGUILayout.ObjectField("Skinned Mesh", skinnedMesh, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;
        crossfade = EditorGUILayout.FloatField("Crossfade", crossfade);
        clipPath = EditorGUILayout.TextField("Output Clip Path", clipPath);
        if (GUILayout.Button("Bake Clip"))
        {
            Bake();
        }
    }

    void Bake()
    {
        if (phonemeJson == null || skinnedMesh == null)
        {
            Debug.LogError("Provide JSON and SkinnedMeshRenderer.");
            return;
        }

        var timeline = JsonUtility.FromJson<LipSyncRuntime.PhonemeTimeline>(phonemeJson.text);
        // build viseme keys using runtime mapping function:
        var runner = ScriptableObject.CreateInstance<LipSyncRuntime>();
        // copy mapping entries from UI? For simplicity call embedded mapping
        // We'll reuse the runtime PhonemeToViseme mapping by reflection-ish approach:
        // Simpler: create viseme key list similar to runtime (quick inline)
        List<VisemeKey> keys = new List<VisemeKey>();
        foreach (var p in timeline.phonemes)
        {
            string vis = BasicPhonemeToViseme(p.ph);
            if (string.IsNullOrEmpty(vis)) continue;
            keys.Add(new VisemeKey { viseme = vis, start = p.start, end = p.end });
        }

        // Merge identical consecutive
        for (int i = keys.Count - 1; i > 0; --i)
            if (keys[i].viseme == keys[i - 1].viseme) { keys[i - 1].end = keys[i].end; keys.RemoveAt(i); }

        AnimationClip clip = new AnimationClip();
        clip.name = Path.GetFileNameWithoutExtension(clipPath);

        // build curves per blendshape
        int blendCount = skinnedMesh.sharedMesh.blendShapeCount;
        var curveMap = new AnimationCurve[blendCount];
        for (int i = 0; i < blendCount; ++i) curveMap[i] = new AnimationCurve();

        // sample timeline at small steps (e.g. 1/60 sec) and evaluate viseme weights
        float dt = 1f / 60f;
        for (float t = 0f; t <= timeline.duration + 0.001f; t += dt)
        {
            // compute per-viseme weight at time t
            Dictionary<string, float> visWeights = new Dictionary<string, float>();
            foreach (var k in keys)
            {
                float fadeIn = Mathf.InverseLerp(k.start - crossfade, k.start + crossfade, t);
                float fadeOut = 1f - Mathf.InverseLerp(k.end - crossfade, k.end + crossfade, t);
                float w = Mathf.Clamp01(Mathf.Min(fadeIn, fadeOut));
                if (w > 0.001f) visWeights[k.viseme] = Mathf.Max(visWeights.ContainsKey(k.viseme) ? visWeights[k.viseme] : 0f, w);
            }
            // map visemes to blend shapes: search mapping list from UI
            foreach (var m in mapping)
            {
                float v = visWeights.ContainsKey(m.viseme) ? visWeights[m.viseme] : 0f;
                float final = v * 100f * m.weight;
                // add keyframe
                var curve = curveMap[m.blendShapeIndex];
                curve.AddKey(new Keyframe(t, final));
            }
            // handle other blendshapes not mapped: leave zero
        }

        // assign curves to clip
        for (int i = 0; i < blendCount; ++i)
        {
            string relativePath = AnimationUtility.CalculateTransformPath(skinnedMesh.transform, (Transform)skinnedMesh.transform.root);
            // Proper binding path: the skinned mesh renderer usually lives in child; we use its path relative to root
            var binding = EditorCurveBinding.FloatCurve(AnimationUtility.CalculateTransformPath(skinnedMesh.transform, skinnedMesh.transform.root),
                                                        typeof(SkinnedMeshRenderer),
                                                        "blendShape." + skinnedMesh.sharedMesh.GetBlendShapeName(i));
            AnimationUtility.SetEditorCurve(clip, binding, curveMap[i]);
        }

        AssetDatabase.CreateAsset(clip, clipPath);
        AssetDatabase.SaveAssets();
        Debug.Log("Saved clip at " + clipPath);
    }

    // small local definitions to reuse the structure:
    string BasicPhonemeToViseme(string ph)
    {
        ph = ph.ToLowerInvariant();
        if (ph == "sil" || ph == "sp") return "Sil";
        if (ph == "p" || ph == "b" || ph == "m") return "MBP";
        if (ph == "f" || ph == "v") return "FF";
        if (ph == "th" || ph == "ð" || ph == "?") return "TH";
        if (ph == "t" || ph == "d" || ph == "s" || ph == "z") return "SS";
        if (ph == "k" || ph == "g") return "kk";
        if (ph == "ch" || ph == "sh" || ph == "jh") return "CH";
        if (ph == "n" || ph == "l") return "NN";
        if (ph == "aa" || ph == "ah" || ph == "ae" || ph == "a") return "AH";
        if (ph == "ow" || ph == "o" || ph == "ao") return "OH";
        if (ph == "iy" || ph == "ih" || ph == "ee") return "EE";
        if (ph.Length > 0 && "aeiou".IndexOf(ph[0]) >= 0) return "AH";
        return null;
    }

    // small helper structs
    class VisemeKey { public string viseme; public float start; public float end; }
}
#endif
