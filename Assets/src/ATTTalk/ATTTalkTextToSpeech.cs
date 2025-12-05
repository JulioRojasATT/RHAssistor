using System;
using System.IO;
using UnityEngine;
/*using Abuksigun.Piper;

public class ATTTalkTextToSpeech : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] string modelPath;
    [SerializeField] string espeakDataPath;

    Piper piper;
    PiperVoice voice;
    PiperSpeaker piperSpeaker;

    public void SpeakToMe(StringScriptableValue speechText)
    {
        Run(speechText.Value);
    }

    async void Run(string speechText)
    {
        if (gameObject.scene.name == null)
            throw new InvalidOperationException("This script must be attached to a game object in a scene, otherwise AudioSource can't play :(");

        string fullModelPath = Path.Join(Application.streamingAssetsPath, modelPath);
        string fullEspeakDataPath = Path.Join(Application.streamingAssetsPath, espeakDataPath);

        piper ??= await Piper.LoadPiper(fullEspeakDataPath);
        voice ??= await PiperVoice.LoadPiperVoice(piper, fullModelPath);
        piperSpeaker ??= new PiperSpeaker(voice);
        _ = piperSpeaker.ContinueSpeach(speechText).ContinueWith(x => Debug.Log($"Generation finished with status: {x.Status}"));
        audioSource.clip = piperSpeaker.AudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}*/