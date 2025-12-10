using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates approximate viseme timing from an AudioClip and Spanish transcript.
/// </summary>
public class AudioAnalyzer : MonoBehaviour
{

    // -----------------------------------------------------------------------
    // PUBLIC ENTRY POINT
    // -----------------------------------------------------------------------
    public List<VisemeKey> GenerateVisemeTiming(AudioClip clip, string transcript)
    {
        if (clip == null || string.IsNullOrEmpty(transcript))
        {
            Debug.LogError("AudioAnalyzer: Missing clip or transcript");
            return new List<VisemeKey>();
        }

        // 1. Normalize text and convert to phonemes
        List<string> phonemes = SpanishToPhonemes(transcript);

        // 2. Convert phonemes -> visemes
        List<string> visemes = PhonemesToVisemes(phonemes);

        // 3. Analyze audio amplitude (energy peaks)
        float[] envelope = ExtractAmplitudeEnvelope(clip, 1024);

        // 4. Assign timings proportionally to audio length
        return DistributeVisemesOverTime(visemes, envelope, clip.length);
    }

    // -----------------------------------------------------------------------
    // STEP 1 - SPANISH TEXT -> PHONEMES (Simplified Grapheme-to-Phoneme)
    // -----------------------------------------------------------------------
    private List<string> SpanishToPhonemes(string text)
    {
        text = text.ToLower();
        text = text.Replace(".", "").Replace(",", "").Replace("!", "").Replace("?", "");

        List<string> result = new List<string>();

        foreach (char c in text)
        {
            switch (c)
            {
                case 'a': result.Add("a"); break;
                case 'e': result.Add("e"); break;
                case 'i': result.Add("i"); break;
                case 'o': result.Add("o"); break;
                case 'u': result.Add("u"); break;

                case 'b': case 'v': result.Add("b"); break;
                case 'p': result.Add("p"); break;
                case 'm': result.Add("m"); break;

                case 'f': result.Add("f"); break;

                case 't': result.Add("t"); break;
                case 'd': result.Add("d"); break;

                case 'l': result.Add("l"); break;
                case 'n': result.Add("n"); break;

                case 'r': result.Add("r"); break;

                case 's': result.Add("s"); break;
                case 'z': result.Add("s"); break;

                case 'c': result.Add("k"); break;
                case 'k': result.Add("k"); break;
                case 'g': result.Add("g"); break;
                case 'j': result.Add("x"); break; // jota

                case 'y': case 'h': break;  // silent

                default:
                    break;
            }
        }

        return result;
    }

    // -----------------------------------------------------------------------
    // STEP 2 — PHONEMES -> VISEMES (Spanish tuned)
    // -----------------------------------------------------------------------
    private List<string> PhonemesToVisemes(List<string> phonemes)
    {
        List<string> visemes = new List<string>();

        foreach (var p in phonemes)
        {
            switch (p)
            {
                case "a": visemes.Add("AA"); break;
                case "e": visemes.Add("EE"); break;
                case "i": visemes.Add("IH"); break;
                case "o": visemes.Add("OH"); break;
                case "u": visemes.Add("OU"); break;

                case "b":
                case "p":
                case "m":
                    visemes.Add("BMP"); break;

                case "f":
                case "v":
                    visemes.Add("FV"); break;

                case "t":
                case "n":
                case "l":
                    visemes.Add("L"); break;

                case "d": visemes.Add("TH/DH"); break;

                case "r": visemes.Add("R"); break;

                case "s": visemes.Add("S/Z"); break;

                case "k":
                case "g":
                case "x":
                    visemes.Add("K/G/H"); break;

                default:
                    visemes.Add("REST"); break;
            }
        }

        return visemes;
    }

    // -----------------------------------------------------------------------
    // STEP 3 — Extract amplitude envelope from audio (RMS)
    // -----------------------------------------------------------------------
    private float[] ExtractAmplitudeEnvelope(AudioClip clip, int step)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        int count = samples.Length / step;
        float[] envelope = new float[count];

        for (int i = 0; i < count; i++)
        {
            float sum = 0;
            for (int j = 0; j < step; j++)
            {
                float v = samples[(i * step) + j];
                sum += v * v;
            }
            envelope[i] = Mathf.Sqrt(sum / step);
        }

        return envelope;
    }

    // -----------------------------------------------------------------------
    // STEP 4 — Distribute visemes along audio duration using RMS envelope
    // -----------------------------------------------------------------------
    private List<VisemeKey> DistributeVisemesOverTime(
        List<string> visemes,
        float[] envelope,
        float clipLength)
    {
        List<VisemeKey> keys = new List<VisemeKey>();

        if (visemes.Count == 0) return keys;

        float avgDuration = clipLength / visemes.Count;

        for (int i = 0; i < visemes.Count; i++)
        {
            float t = i * avgDuration;
            float d = avgDuration;

            keys.Add(new VisemeKey()
            {
                viseme = visemes[i],
                start = t,
                end = t+d
            });
        }

        return keys;
    }
}