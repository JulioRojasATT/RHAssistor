using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimationUtils : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private string animationStateNameToListen;

    [SerializeField] private UnityEvent onAnimationStarted;

    [SerializeField] private UnityEvent onAnimationEnded;

    [SerializeField] private AudioSource sfxAudioSource;
    
    [SerializeField] private AudioSource dialogueAudioSource;
    
    [SerializeField] private AudioSource generalAudioSource;

    public void WaitForAnimationCameraWorkToFinish()
    {
        StartCoroutine(WaitUntilAnimationStartsAndFinishes(animator, Animator.StringToHash(animationStateNameToListen), 0));
    }
    
    public IEnumerator WaitUntilAnimationStartsAndFinishes(Animator animator, int stateHash, int layerIndex)
    {
        onAnimationStarted.Invoke();
        yield return new WaitUntil(()=>animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash== stateHash);
        yield return new WaitWhile(()=>animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash== stateHash);
        onAnimationEnded.Invoke();
    }

    public void PlaySFXOneShot(AudioClip audioClip) {
        PlayOneShot(sfxAudioSource,audioClip);
    }
    
    public void PlayDialogueOneShot(AudioClip audioClip) {
        PlayOneShot(dialogueAudioSource,audioClip);
    }
    
    public void PlayGeneralAudioOneShot(AudioClip audioClip) {
        PlayOneShot(generalAudioSource,audioClip);
    }

    public void PlayOneShot(AudioSource audioSource, AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }
}
