using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimationEventsConnector : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayOneShotGlobal(Object param1) {
        if(param1 is AudioClip)
        {
            AudioClip clip = (AudioClip)param1;
            audioSource.PlayOneShot(clip);
        }
    }
}
