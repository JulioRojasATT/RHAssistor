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
    // -----------------------------------------------------------------------
    // STEP 4 — Distribute visemes along audio duration using RMS envelope
    // -----------------------------------------------------------------------
    private List<VisemeKey> DistributeVisemesOverTime(
        List<string> visemes,
        float[] envelope,
        float clipLength)
    {
        List<VisemeKey> keys = new List<VisemeKey>();
        if (visemes == null || visemes.Count == 0)
            return keys;

        if (envelope == null || envelope.Length == 0 || clipLength <= 0f)
        {
            // fallback to equal distribution
            float avgDur = clipLength / visemes.Count;
            for (int i = 0; i < visemes.Count; i++)
            {
                keys.Add(new VisemeKey()
                {
                    viseme = visemes[i],
                    start = i * avgDur,
                    end = (i * avgDur) + avgDur
                });
            }
            return keys;
        }

        int m = envelope.Length;
        // Build cumulative energy (use energy + epsilon to avoid zero total)
        double[] cumulative = new double[m];
        double sum = 0.0;
        for (int i = 0; i < m; i++)
        {
            // Use squared envelope as energy; envelope is already RMS-ish, but squaring converts to energy
            double e = envelope[i] * envelope[i];
            sum += e;
            cumulative[i] = sum;
        }

        if (sum <= 1e-9)
        {
            // silent clip fallback to equal distribution
            float avgDur = clipLength / visemes.Count;
            for (int i = 0; i < visemes.Count; i++)
            {
                keys.Add(new VisemeKey()
                {
                    viseme = visemes[i],
                    start = i * avgDur,
                    end = (i * avgDur) + avgDur
                });
            }
            return keys;
        }

        // Helper: get time (seconds) corresponding to envelope index
        double samplesPerEnvelopeBin = (double)clipLength / (double)m; // seconds per bin

        // Compute boundaries at energy fractions 0, 1/N, 2/N, ..., 1
        int N = visemes.Count;
        double totalEnergy = cumulative[m - 1];

        // We'll compute N+1 boundary times
        double[] boundaries = new double[N + 1];
        boundaries[0] = 0.0;
        boundaries[N] = clipLength;

        for (int b = 1; b < N; b++)
        {
            double target = (b / (double)N) * totalEnergy;

            // binary search for first index where cumulative >= target
            int lo = 0;
            int hi = m - 1;
            int idx = hi;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                if (cumulative[mid] >= target)
                {
                    idx = mid;
                    hi = mid - 1;
                }
                else
                {
                    lo = mid + 1;
                }
            }

            // convert idx to time (use idx + 0.5 to center in the bin)
            double t = (idx + 0.5) * samplesPerEnvelopeBin;
            // clamp to valid range
            if (t < 0.0) t = 0.0;
            if (t > clipLength) t = clipLength;
            boundaries[b] = t;
        }

        // Create viseme keys using boundaries
        const float minDuration = 0.02f; // avoid extremely short visemes (20 ms)
        for (int i = 0; i < N; i++)
        {
            double start = boundaries[i];
            double end = boundaries[i + 1];
            double dur = end - start;
            if (dur < minDuration)
            {
                // expand tiny durations by borrowing from neighbors where possible
                double add = minDuration - dur;
                // try to extend end
                double newEnd = Math.Min(clipLength, end + add * 0.5);
                double newStart = Math.Max(0.0, start - add * 0.5);
                start = newStart;
                end = newEnd;
                dur = end - start;
                if (dur < 0.001) dur = minDuration; // absolute guard
            }

            keys.Add(new VisemeKey()
            {
                viseme = visemes[i],
                start = (float)start,
                end = (float)start + (float)dur
            });
        }

        // Optional merging: collapse tiny silences between identical visemes
        // (Not done here — LipSyncRuntime/consumer can merge or smooth if desired)

        return keys;
    }
}