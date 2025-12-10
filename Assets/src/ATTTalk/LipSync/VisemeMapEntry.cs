using System;
using UnityEngine;

[Serializable]
public class VisemeMapEntry
{
    public string viseme;
    public int blendShapeIndex;
    [Range(0, 1)] public float weight = 1f; // multiplier
}