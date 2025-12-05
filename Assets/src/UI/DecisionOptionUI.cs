using System;
using TMPro;
using UnityEngine;

public class DecisionOptionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionText;

    private DecisionOptionData currentData;
    public DecisionOptionData CurrentData => currentData;

    /// <summary>
    /// Conversation that selecting this option triggers.
    /// </summary>
    public string TargetConversationName => currentData.TargetConversationName;

    public void LoadData(DecisionOptionData newData)
    {
        currentData = newData;
        optionText.text = currentData.OptionName;
    }
}