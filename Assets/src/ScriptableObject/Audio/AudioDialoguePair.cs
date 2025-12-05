using UnityEngine;
[System.Serializable]
public class AudioDialoguePair {

    [SerializeField] private AudioClip[] languageDialogues;
    public AudioClip[] LanguageDialogues => languageDialogues;

    [SerializeField] private DialogueDivisions[] languageStrings;
    public DialogueDivisions[] LanguageStrings => languageStrings;

    public int DialogueDivisionsAmount => languageStrings[0].DialogueStrings.Length;  

    public AudioClip GetLanguageDialogue(int languageID) => languageDialogues[languageID];

    public string GetLanguageString(int languageID, int dialogueIndex) => languageStrings[dialogueIndex].DialogueStrings[languageID];    
}