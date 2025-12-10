using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioAnalizerConnector : MonoBehaviour
{
    [SerializeField] private AudioAnalyzer audioAnalyzer;

    [SerializeField] private AudioClip audioClip;

    [SerializeField] private string transcript;

    public void GenerateVisemes()
    {
        List<VisemeKey> visemeKeys = audioAnalyzer.GenerateVisemeTiming(audioClip, transcript);
        Debug.Log("Audio is separated in the following visemes:");
        visemeKeys.ForEach(key => Debug.Log("Viseme " + key.viseme + " happens at " + key.start + "and ends at " + key.end + "."));
    }
}
