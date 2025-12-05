using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUpdateConnector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;

    [SerializeField] private string text;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] typewrittingAudioClips;

    [SerializeField] private UnityEvent onFinished;

    public void TypeWrite(float time)
    {
        StartCoroutine(TypeWriteInTime(time));
    }

    public IEnumerator TypeWriteInTime(float animationTime)
    {
        float currentTime = 0, timeDelta = animationTime / text.Length;
        int currentCharacterIndex = 0;
        while (currentTime < animationTime) {
            uiText.text = text.Substring(0, currentCharacterIndex++);
            yield return new WaitForSeconds(timeDelta);
            audioSource.PlayOneShot(typewrittingAudioClips[Random.Range(0,typewrittingAudioClips.Length)]);
            currentTime += timeDelta;
        }
        onFinished.Invoke();
        uiText.text = text;
        Debug.Log("Finishing typewritting");
    }
}
