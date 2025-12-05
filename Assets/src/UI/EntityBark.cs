using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EntityBark : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject dialogueCanvas;

    /// <summary>
    /// How much time the bark appears on screen.
    /// </summary>
    [SerializeField] private float barkTimePerCharacter = 0.01f;

    [Header("Bark Group Settings")]
    [SerializeField] private RuntimeAudioManager runtimeAudioManager;

    [SerializeField] private IntScriptableValue languageID;

    [SerializeField] private float delayBetweenDialogues = 0.6f;

    [Header("Event Management")]
    [SerializeField] private UnityEvent onGroupBarkStarted;

    [SerializeField] private UnityEvent onGroupBarkFinished;

    private Queue<string> _pendantBarks = new Queue<string>();

    private Coroutine _currentBarkCoroutine;

    public void BarkDelay(float delay)
    {
        _currentBarkCoroutine = StartCoroutine(BarkDelayCor(delay));

    }

    public void Bark(string dialogue) {
        if (_currentBarkCoroutine != null)
        {
            _pendantBarks.Enqueue(dialogue);
            return;
        }
        _currentBarkCoroutine = StartCoroutine(BarkCor(dialogue));
    }

    private IEnumerator BarkCor(string dialogue) {
        dialogueCanvas.gameObject.SetActive(true);
        dialogueText.text = dialogue;
        yield return new WaitForSeconds(barkTimePerCharacter * dialogue.Length);
        dialogueCanvas.gameObject.SetActive(false);
        // Flag release
        _currentBarkCoroutine = null;
        if (_pendantBarks.Count > 0) {
            Bark(_pendantBarks.Dequeue());
        }
    }

    public void BarkGroup(DialogueBarkGroup dialogueBarkGroup) {
        _currentBarkCoroutine = StartCoroutine(BarkGroupCor(dialogueBarkGroup));
    }

    private IEnumerator BarkGroupCor(DialogueBarkGroup dialogueBarkGroup){        
        dialogueCanvas.gameObject.SetActive(true);
        onGroupBarkStarted?.Invoke();
        Coroutine multiDialogueCoroutine = null;
        int currentDialogue = 0;
        foreach (AudioDialoguePair audioDialoguePair in dialogueBarkGroup.Dialogues) {
            runtimeAudioManager.PlayLocalizedInterruptable(audioDialoguePair.GetLanguageDialogue(languageID.Value));
            multiDialogueCoroutine =
                StartCoroutine(MultiDialogueBarkCor(dialogueBarkGroup.Dialogues[currentDialogue], languageID.Value));
            yield return new WaitForSeconds(audioDialoguePair.GetLanguageDialogue(languageID.Value).length + delayBetweenDialogues);
            StopCoroutine(multiDialogueCoroutine);
            currentDialogue++;
        }
        dialogueCanvas.gameObject.SetActive(false);
        onGroupBarkFinished?.Invoke();
    }

    private IEnumerator MultiDialogueBarkCor(AudioDialoguePair dialoguePair, int languageID) {
        DialogueDivisions[] dialogueDivisions = dialoguePair.LanguageStrings;
        for (int i = 0; i < dialoguePair.DialogueDivisionsAmount; i++) {
            dialogueText.text = dialogueDivisions[languageID].DialogueStrings[i];
            yield return new WaitForSeconds(dialogueDivisions[languageID].UpdateTimes[i]);
        }
    }

    private IEnumerator BarkDelayCor(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Flag release
        _currentBarkCoroutine = null;
        if (_pendantBarks.Count > 0)
        {
            Bark(_pendantBarks.Dequeue());
        }
    }
}
