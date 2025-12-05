using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Values/Audio Manager", fileName = "New Audio Manager")]
public class RuntimeAudioManager : NonSerializedScriptableValue<AudioManager>
{
    public void PlayGlobal(AudioClip clip) {
        if (!Value) {
            Debug.LogError("Error, no audio manager found");
            return;
        }
        Value.PlayGlobal(clip);
    }
    
    public void PlayRandomClipGlobal(AudioClipGroup audioClips) {
        if (!Value) {
            Debug.LogError("Error, no audio manager found");
            return;
        }
        Value.PlayRandomClipGlobal(audioClips);
    }

    public void SetGlobalAudioMute(bool mute) => Value.SetGlobalAudioMute(mute);

    public void SetMusicMute(bool mute) => Value.SetMusicMute(mute);
    
    public void SetInterruptableAudioMute(bool mute) => Value.SetInterruptableAudioMute(mute);    

    public void PlayInterruptable(AudioClip clip) {
        Value.PlayInterruptable(clip);
    }

    public void PlayLocalizedInterruptable(AudioClip dialogue)
    {
        Value.PlayInterruptableLocalized(dialogue);
    }

    public void StopInterruptableAudioSource() {
        Value.StopInterruptableAudioSource();
    }
    
    public void ChangeMusic(AudioClip newMusicClip) {
        if (!Value) {
            Debug.LogError("Error, no audio manager found");
            return;
        }
        Value.ChangeMusic(newMusicClip);
    }

    public void SetMusicVolume(float volume)
    {
        value.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        value.SetSFXVolume(volume);        
    }
}
