using System;
using UnityEngine;

[Serializable]
public class DecisionOptionData
{
    [SerializeField] private string optionName;
    public string OptionName => optionName;

    /// <summary>
    /// Conversation that selecting this option triggers.
    /// </summary>
    [SerializeField] private string targetConversationName;
    public string TargetConversationName => targetConversationName;
}