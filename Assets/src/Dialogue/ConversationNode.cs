using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ConversationNode {
    
    [SerializeField] private ConversationActor dialogueActor;
    public string DialogueActorName => dialogueActor.ActorName;

    [TextAreaAttribute]
    [SerializeField] private string dialogue;
    public string Dialogue => dialogue;

    [SerializeField] private AudioClip dialogueAudio;

    [SerializeField] private UnityEvent onNodePlayed;
    public UnityEvent OnNodePlayed => onNodePlayed;

    public AudioClip DialogueAudio => dialogueAudio;

    [SerializeField] private string action;

    [SerializeField] private List<DecisionOptionData> decisionOptions;
    public List<DecisionOptionData> DecisionOptions => decisionOptions;
}