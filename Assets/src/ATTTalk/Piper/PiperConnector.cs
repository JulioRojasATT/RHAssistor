using Abuksigun.Piper;
using System;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class PiperConnector : MonoBehaviour
{
    [SerializeField] string modelPath;
    [SerializeField] string espeakDataPath;

    [SerializeField] private AudioSource audioSource;

    Piper piper;
    PiperVoice voice;
    PiperSpeaker piperSpeaker;

    [SerializeField] private UnityEvent<AudioClip> onAudioClipGenerated;

    public void SpeakToMe(string speechText)
    {
        Run(speechText);
    }

    async void Run(string text)
    {
        if (gameObject.scene.name == null)
            throw new InvalidOperationException("This script must be attached to a game object in a scene, otherwise AudioSource can't play :(");

        string fullModelPath = Path.Join(Application.streamingAssetsPath, modelPath);
        string fullEspeakDataPath = Path.Join(Application.streamingAssetsPath, espeakDataPath);

        piper ??= await Piper.LoadPiper(fullEspeakDataPath);
        voice ??= await PiperVoice.LoadPiperVoice(piper, fullModelPath);
        piperSpeaker ??= new PiperSpeaker(voice);
        _ = piperSpeaker.ContinueSpeach(text).ContinueWith(x => Debug.Log($"Generation finished with status: {x.Status}"));
        // Audio Clip creation        
        AudioClip copyAudioClip = AudioClip.Create("A", 500, 500, 500, false);

        //
        audioSource.clip = piperSpeaker.AudioClip;
        audioSource.loop = true;
        //audioSource.Play();
        onAudioClipGenerated.Invoke(piperSpeaker.AudioClip);
    }

    public void FinishAudioGeneration(Task task)
    {
        Debug.Log($"Generation finished with status: {task.Status}");        
    }
}
