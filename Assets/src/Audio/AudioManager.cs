using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    /// <summary>
    /// Audio source that plays 2D Sounds
    /// </summary>
    [SerializeField] private AudioSource globalAudioSource;
    
    /// <summary>
    /// Audio source that plays 3d sounds
    /// </summary>
    [SerializeField] private AudioSource localAudioSource;
    
    [SerializeField] private AudioSource interruptableAudioSource;
    
    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private AudioSourcePool audioSourcePool;

    [SerializeField] private LocalizedDialogue m_localizedDialogue;

    private Dictionary<string, AudioSource> loopedAudioSources = new Dictionary<string, AudioSource>();

    [Header("Mute Control")]
    private float _globalAudioInitialVolume;

    private bool _globalAudioMuted = false;

    [SerializeField] private UnityEvent onGlobalAudioMutedDetectedAtStart;

    private float _musicAudioInitialVolume;

    private bool _musicAudioMuted = false;

    [SerializeField] private UnityEvent onMusicMutedDetectedAtStart;

    private float _interruptableAudioInitialVolume;

    private bool _interruptableAudioMuted = false;

    [Header("Localization")]
    [SerializeField] private IntScriptableValue languageID;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
        audioSourcePool.CreateInstances(1);
        _globalAudioInitialVolume = globalAudioSource.volume;
        _musicAudioInitialVolume = musicAudioSource.volume;
        _interruptableAudioInitialVolume = interruptableAudioSource.volume;        
    }

    private void Start()
    {
        // Try mute if player has decided it
        if (PlayerPrefs.GetFloat("GlobalAudioVolume", _globalAudioInitialVolume) == 0f)
        {
            onGlobalAudioMutedDetectedAtStart?.Invoke();
        }
        else
        {
            Debug.Log("Not muting global audio");
        }
        if (PlayerPrefs.GetFloat("MusicAudioVolume", _musicAudioInitialVolume) == 0f)
        {
            onMusicMutedDetectedAtStart?.Invoke();
        }
        else
        {
            Debug.Log("Not muting music");
        }
    }

    public void SetLocalized(LocalizedDialogue localizedDialogue)
    {
        m_localizedDialogue = localizedDialogue;
    }

    public void PlayLocalizedDelayed(float seconds)
    {
        Invoke("PlayCurrentLocalized", seconds);
    }

    public void PlayCurrentLocalized()
    {

        PlayInterruptable(m_localizedDialogue.GetLanguageDialogue(languageID.Value));
    }

    public void SetGlobalAudioMute(bool muted)
    {
        _globalAudioMuted = muted;
        globalAudioSource.volume = _globalAudioMuted ? 0 : _globalAudioInitialVolume;        
        PlayerPrefs.SetFloat("GlobalAudioVolume", globalAudioSource.volume);
    }

    public void SetMusicMute(bool muted)
    {
        _musicAudioMuted = muted;
        musicAudioSource.volume = _musicAudioMuted ? 0 : _musicAudioInitialVolume;
        PlayerPrefs.SetFloat("MusicAudioVolume", musicAudioSource.volume);
    }

    public void SetInterruptableAudioMute(bool muted)
    {
        interruptableAudioSource.volume = _interruptableAudioMuted ? _interruptableAudioInitialVolume: 0;
        Debug.Log("Setting interruptable audio source volume to " + interruptableAudioSource.volume);
        _interruptableAudioMuted = muted;
        PlayerPrefs.SetFloat("InterruptableAudioVolume", interruptableAudioSource.volume);        
    }

    public void PlayInterruptableLocalized(AudioClip dialogue) {
        PlayInterruptable(dialogue);
    }

    public void PlayInterruptableLocalized(LocalizedDialogue dialogue)
    {
        PlayInterruptable(dialogue.GetLanguageDialogue(languageID.Value));
    }

    public void SetMusicVolume(float volume)
    {
        if (!musicAudioSource)
        {
            return;
        }
        musicAudioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        if (!globalAudioSource)
        {
            return;
        }
        globalAudioSource.volume = volume;
    }

    public void PlayClipAtPosition(AudioClip clip, Vector3 soundPosition) {
        AudioSource.PlayClipAtPoint(clip,soundPosition);
    }

    public void PlayClipAtPosition(AudioClip clip, Vector3 soundPosition, float volume) {
        AudioSource.PlayClipAtPoint(clip,soundPosition,volume);
    }
    
    public void PlayInterruptable(AudioClip clip) {
        interruptableAudioSource.clip = clip;
        interruptableAudioSource.Play();
    }
    
    public void StopInterruptableAudioSource() {
        interruptableAudioSource.Stop();
    }

    public void PlayLooped(string id, AudioClip audioClip) {
        AudioSource toUse = audioSourcePool.OccupyOne(out bool createdNewInstance);
        toUse.clip = audioClip;
        toUse.Play();
        toUse.gameObject.SetActive(true);
        loopedAudioSources.Add(id, toUse);
    }

    public void StopLooped(string id) {
        if (loopedAudioSources.TryGetValue(id, out AudioSource loopedAudioSource)) {
            loopedAudioSource.Stop();
            loopedAudioSource.gameObject.SetActive(false);
            audioSourcePool.ReturnToPool(loopedAudioSource);
            loopedAudioSources.Remove(id);
        }
    }
    
    public void PlayGlobal(AudioClip clip) {
        globalAudioSource.PlayOneShot(clip);
    }
    
    public void PlayRandomClipGlobal(AudioClipGroup audioClips) {
        PlayGlobal(audioClips.GetRandom());
    }
    
    public void PlayRandomClipAtPosition(AudioClipGroup audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(AudioClipGroup audioClips, Vector3 position, float volume) {
        instance.PlayClipAtPosition(audioClips.GetRandom(), position, volume);
    }
    
    public void PlayRandomClipAtPosition(List<AudioClip> audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(List<AudioClip> audioClips, Vector3 position, float volume) {
        localAudioSource.volume = volume;
        if (audioClips.Count == 0) {
            return;
        }
        instance.PlayClipAtPosition(audioClips[UnityEngine.Random.Range(0, audioClips.Count)], position, volume);
    }
    
    public void PlayRandomClipAtPosition(AudioClip[] audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(AudioClip[] audioClips, Vector3 position, float volume) {
        localAudioSource.volume = volume;
        if (audioClips.Length == 0) {
            return;
        }
        instance.PlayClipAtPosition(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volume);
    }
    
    public void ChangeMusic(AudioClip newMusicClip) {
        musicAudioSource.clip = newMusicClip;
        musicAudioSource.Play();
    }
}
