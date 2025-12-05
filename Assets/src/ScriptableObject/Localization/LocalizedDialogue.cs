using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Localized Dialogue", menuName ="Audio/Localized Dialogue")]
public class LocalizedDialogue : ScriptableObject
{
    [SerializeField] private AudioClip[] languageDialogues;

    public AudioClip GetLanguageDialogue(int languageID) => languageDialogues[languageID];
}
