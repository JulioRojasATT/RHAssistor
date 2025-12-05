using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversatingNPC : MonoBehaviour {
    [SerializeField] private string defaultConversationName;
    public string DefaultConversationName => defaultConversationName;
    
    [SerializeField] private List<Conversation> conversations;
    public List<Conversation> Conversations => conversations;

    [SerializeField] private AudioClip conversationAudio;
    public AudioClip ConversationAudio => conversationAudio;

    [SerializeField] private Transform conversationFocusPoint;
    public Transform ConversationFocusPoint => conversationFocusPoint;

    [SerializeField] private Transform otherConversantSpot;
    public Transform OtherConversantSpot => otherConversantSpot;

    public void StartConversation(string conversationName)
    {
        if (RuntimeConversationManager.instance)
        {
            RuntimeConversationManager.instance.StartRuntimeConversation(this, conversationName);
        }
    }
}
