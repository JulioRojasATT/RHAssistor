using System;
using System.Collections.Generic;

[Serializable]
public class PhonemeTimeline
{
    public string audioFile;
    public float duration;
    public List<PhonemeEntry> phonemes;
}