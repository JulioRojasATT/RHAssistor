using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Conversation {

    [SerializeField] private string id;
    public string ID => id;

    /// <summary>
    /// The score of the conversation (Used for karma and other purposes)
    /// </summary>
    [SerializeField] private int score;
    public int Score => score;

    [SerializeField] private List<ConversationActor> actors;

    [SerializeField] private List<ConversationNode> nodes;
    
    private ConversationNode _currentNode;
    public ConversationNode CurrentNode => _currentNode;

    private int _currentNodeIndex = 0;

    [Header("Events")]
    public UnityEvent onConversationStarted;
    
    public UnityEvent onConversationEnded;

    public void Start() {
        _currentNode = nodes[_currentNodeIndex];
    }

    public void StartConversation() {
        onConversationStarted?.Invoke();
        actors.ForEach(x=>x.onConversationStarted?.Invoke());
    }

    public void EndConversation() {
        onConversationEnded?.Invoke();
        actors.ForEach(x=>x.onConversationEnded?.Invoke());
    }

    public void Reset() {
        _currentNodeIndex=0;
        _currentNode = nodes[_currentNodeIndex];
    }

    public void NextNode() {
        _currentNodeIndex++;
        _currentNode = nodes[_currentNodeIndex];
    }

    public bool IsOnDecisionNode()
    {
        return CurrentNode.DecisionOptions.Count > 0;
    }

    public List<DecisionOptionData> GetCurrentNodeDecisionOptions()
    {
        return CurrentNode.DecisionOptions;
    }

    public bool IsOnLastNode() {
        return _currentNodeIndex == nodes.Count - 1;
    }
}