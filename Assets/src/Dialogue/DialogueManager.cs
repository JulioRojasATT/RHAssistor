using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour {

    [Header("Input Actions")]
    [SerializeField] private InputActionReference continueInputActionReference;
    
    [Header("Conversation Events")]
    public UnityEvent onConversationStarted;
    
    public UnityEvent onConversationContinued;
    
    public UnityEvent onConversationEnded;

    public UnityEvent onDecisionNodeFound;
    
    [SerializeField] private Conversation currentConversation;

    private Dictionary<string, Conversation> _conversationsById;
    
    [SerializeField] private List<Conversation> _conversations;

    [SerializeField] private string playerName;

    public int CurrentConversationScore => currentConversation.Score;

    [Header("Audio Management")]
    [SerializeField] private AudioSource dialogueAudioSource;
    
    [Header("Dialogue UI")]
    [SerializeField] private DialogueUI _dialogueUI;

    [SerializeField] private bool inConversation;
    public bool InConversation => inConversation;

    private void Awake() {
        _conversationsById = _conversations.ToDictionary(x => x.ID);
        //continueInputActionReference.action.performed += OnContinuePerformed;
    }

    private void OnContinuePerformed(InputAction.CallbackContext obj) {
        ContinueConversation();
    }

    public void AddConversation(Conversation conversation)
    {
        _conversationsById.Add(conversation.ID, conversation);
    }

    public void RemoveConversation(Conversation conversation)
    {
        if (_conversationsById.ContainsKey(conversation.ID)) {
            _conversationsById.Remove(conversation.ID);
        }        
    }

    public void StartConversation(string conversationID) {
        StartConversation(_conversationsById[conversationID]);
    }

    public IEnumerator ConverseWith(ConversatingNPC conversatingNPC) {
        conversatingNPC.Conversations.ForEach(conversation => AddConversation(conversation));
        StartConversation(conversatingNPC.DefaultConversationName);
        yield return new WaitWhile(() => InConversation);
        conversatingNPC.Conversations.ForEach(conversation => RemoveConversation(conversation));
    }

    public void StartConversation(Conversation conversation) {
        inConversation = true;
        currentConversation = conversation;
        currentConversation.StartConversation();
        onConversationStarted?.Invoke();
        currentConversation.Reset();
        PlayNode(currentConversation.CurrentNode);
    }

    public void ContinueConversation() {
        if (dialogueAudioSource && dialogueAudioSource.isPlaying){
            dialogueAudioSource.Stop();
        }
        if (currentConversation==null)
        {
            return;
        }
        if (currentConversation.IsOnLastNode())
        {
            inConversation = false;
            currentConversation.EndConversation();
            onConversationEnded?.Invoke();
            return;
        }
        onConversationContinued?.Invoke();        
        currentConversation.NextNode();
        if (currentConversation.IsOnDecisionNode()) {
            onDecisionNodeFound?.Invoke();
            _dialogueUI.SetDecisionOptions(currentConversation.GetCurrentNodeDecisionOptions());
        }
        PlayNode(currentConversation.CurrentNode);
    }

    public void PlayNode(ConversationNode node) {
        _dialogueUI.SetActorName(node.DialogueActorName);
        _dialogueUI.SetDialogue(node.Dialogue);
        node.OnNodePlayed?.Invoke();
        if (!node.DialogueAudio|| !dialogueAudioSource) {
            return;
        }
        dialogueAudioSource.clip = currentConversation.CurrentNode.DialogueAudio;
        dialogueAudioSource.Play();
    }

    public void EndCurrentConversation(){
        if (currentConversation==null){
            return;
        }
        inConversation = false;
        currentConversation.EndConversation();
        onConversationEnded?.Invoke();
    }

    public void ProcessDecision(int decisionOptionUIINdex) {
        DecisionOptionUI decisionOptionUI = _dialogueUI.AnswerOptionsUIs[decisionOptionUIINdex];
        EndCurrentConversation();
        StartConversation(decisionOptionUI.TargetConversationName);
    }
}