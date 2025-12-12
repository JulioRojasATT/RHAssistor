using UnityEngine;
using UnityEngine.UI;
using Piper;
using UnityEngine.Events;

public class PiperConnector : MonoBehaviour
{
    public PiperManager piper;

    [SerializeField] private AudioSource _source;

    [SerializeField] private UnityEvent<AudioClip> onAudioClipGenerated;

    public async void TextToSpeech(string text)
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        var audio = piper.TextToSpeech(text);

        _source.Stop();
        if (_source && _source.clip)
            Destroy(_source.clip);

        _source.clip = await audio;
        _source.Play();
        onAudioClipGenerated.Invoke(_source.clip);
    }
}
