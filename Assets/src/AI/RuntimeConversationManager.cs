using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RuntimeConversationManager : MonoBehaviour {

    [SerializeField] private Animator obscurerAnimator;

    [SerializeField] private CinemachineVirtualCamera conversationFocusCamera;

    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private UnityEvent onFocusStarted;

    [SerializeField] private UnityEvent onFocusEnded;

    public static RuntimeConversationManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;        
    }

    public void StartRuntimeConversation(ConversatingNPC conversatingNPC, string conversationName) {
        StartCoroutine(StartRuntimeConversationCor(conversatingNPC,conversationName));
    }

    public IEnumerator StartRuntimeConversationCor(ConversatingNPC conversatingNPC, string conversationName) {        
        yield return FocusConversationNPC(conversatingNPC);
        // Wait for the conversation to finish
        conversatingNPC.Conversations.ForEach(conversation => dialogueManager.AddConversation(conversation));
        dialogueManager.StartConversation(conversationName);
        yield return new WaitWhile(() => dialogueManager.InConversation);
        conversatingNPC.Conversations.ForEach(conversation => dialogueManager.RemoveConversation(conversation));
        // Disappear UI
        yield return DefocusConversationNPC();
    }

    public IEnumerator StartAudioConversationCor(ConversatingNPC conversatingNPC)
    {
        yield return FocusConversationNPC(conversatingNPC);
        // Wait for the conversation to finish
        yield return new WaitForSeconds(conversatingNPC.ConversationAudio.length);
        // Disappear UI
        yield return DefocusConversationNPC();
    }

    public IEnumerator FocusConversationNPC(ConversatingNPC conversatingNPC) {
        onFocusStarted?.Invoke();
        obscurerAnimator.SetTrigger("Appear");
        yield return new WaitForSeconds(2.1f);
        obscurerAnimator.SetTrigger("Disappear");
        conversationFocusCamera.gameObject.SetActive(true);
        conversationFocusCamera.Follow = conversatingNPC.ConversationFocusPoint;
        conversationFocusCamera.LookAt = conversatingNPC.ConversationFocusPoint;
    }

    public IEnumerator DefocusConversationNPC() {
        obscurerAnimator.SetTrigger("Appear");
        yield return new WaitForSeconds(2.1f);
        obscurerAnimator.SetTrigger("Disappear");
        conversationFocusCamera.gameObject.SetActive(false);
        onFocusEnded?.Invoke();
    }

    public void StartBarkConversation(){
    }
}