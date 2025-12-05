using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue Bark Group", menuName = "Audio/Dialogue Bark Group")]
public class DialogueBarkGroup : ScriptableObject {
    [SerializeField] private List<AudioDialoguePair> dialogues;
    public List<AudioDialoguePair> Dialogues => dialogues;
}