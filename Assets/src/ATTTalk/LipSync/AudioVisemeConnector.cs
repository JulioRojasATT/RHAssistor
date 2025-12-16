using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AudioVisemeConnector : MonoBehaviour
{
    [SerializeField] private LipSyncRuntime lipSync;

    [Header("Data")]
    [SerializeField] private AudioClipScriptableValue aiAnswerAudioClip;

    [SerializeField] private StringScriptableValue transcript;

    [Header("Events")]
    [SerializeField] private UnityEvent onVisemeKeysCalculated;

    [SerializeField] private UnityEvent onTalkingFinished;

    private AudioAnalyzer audioAnalyzer;

    public void CalculateAndSetVisemeKeys()
    {
        audioAnalyzer = new AudioAnalyzer();
        List<VisemeKey> visemeKeys = audioAnalyzer.GenerateVisemeTiming(aiAnswerAudioClip.Value, transcript.Value);
        Debug.Log("Audio is separated in the following visemes:");
        visemeKeys.ForEach(key => Debug.Log("Viseme " + key.viseme + " happens at " + key.start + "and ends at " + key.end + "."));
        lipSync.SetVisemeKeys(visemeKeys);        
        onVisemeKeysCalculated.Invoke();
        Talk();
    }

    public void Talk()
    {
        StartCoroutine(WaitForAudioToStop(aiAnswerAudioClip.Value.length));
    }

    public IEnumerator WaitForAudioToStop(float audioDurationInSeconds)
    {
        float currentTime = 0;
        while(currentTime<audioDurationInSeconds)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }        
        onTalkingFinished.Invoke();
    }
}
