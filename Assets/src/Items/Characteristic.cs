using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Characteristic
{
    public string Label;

    [TextArea][SerializeField] private string description;
    public string Description => description;

    [Range(0f, 5f)][SerializeField] private float stars;
    public float Stars => stars;
}