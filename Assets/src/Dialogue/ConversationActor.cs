using UnityEngine;
using UnityEngine.Events;

public class ConversationActor : MonoBehaviour {
    [SerializeField] private string actorName;
    public string ActorName => actorName;

    public UnityEvent onConversationStarted;
    
    public UnityEvent onConversationEnded;
}
