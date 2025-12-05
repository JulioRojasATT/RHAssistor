using UnityEngine;

[System.Serializable]
public class DialogueDivisions {
    [SerializeField] private float[] updateTimes;
    public float[] UpdateTimes => updateTimes;
    
    [SerializeField] private string[] dialogueStrings;
    public string[] DialogueStrings => dialogueStrings;
}
