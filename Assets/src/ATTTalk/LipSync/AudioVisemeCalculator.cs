using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioVisemeCalculator : MonoBehaviour
{
    [SerializeField] private AudioAnalyzer audioAnalyzer;

    [SerializeField] private LipSyncRuntime lipSync;

    [Header("Data")]
    [SerializeField] private AudioClipScriptableValue aiAnswerAudioClip;

    [SerializeField] private StringScriptableValue transcript;

    [Header("Events")]
    [SerializeField] private UnityEvent onVisemeKeysCalculated;

    public void CalculateAndSetVisemeKeys()
    {
        List<VisemeKey> visemeKeys = audioAnalyzer.GenerateVisemeTiming(aiAnswerAudioClip.Value, transcript.Value);
        Debug.Log("Audio is separated in the following visemes:");
        visemeKeys.ForEach(key => Debug.Log("Viseme " + key.viseme + " happens at " + key.start + "and ends at " + key.end + "."));
        lipSync.SetVisemeKeys(visemeKeys);
        onVisemeKeysCalculated.Invoke();
    }
}
