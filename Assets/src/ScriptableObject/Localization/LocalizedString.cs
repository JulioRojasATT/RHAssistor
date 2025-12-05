using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Localized String", menuName = "Values/Localized String")]
public class LocalizedString : ScriptableObject
{

    [SerializeField] private string[] languageStrings;

    public string GetLanguageString(int languageID) => languageStrings[languageID];

}