using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RWBY Requiem/Audio/Audio Clip Group", fileName = "New Audio Clip Group")]
public class AudioClipGroup : ScriptableObject {

    public AudioClip[] AudioClips;

    /// <summary>
    /// Labels that this audio clip is identified by
    /// </summary>
    public List<string> labels;

    public AudioClip GetRandom() {
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }

}
