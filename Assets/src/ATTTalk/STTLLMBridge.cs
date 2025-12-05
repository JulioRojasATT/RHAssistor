using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LLMUnitySamples;

public class STTLLMBridge : MonoBehaviour
{
    public ChatBot chatBot;
    public void Bridge(StringScriptableValue userSpeech) {
        chatBot.onInputFieldSubmit(userSpeech.Value);
    }
}