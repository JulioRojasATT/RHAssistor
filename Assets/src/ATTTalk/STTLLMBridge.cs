using UnityEngine;
using LLMUnitySamples;

public class STTLLMBridge : MonoBehaviour
{
    public ChatBot chatBot;
    
    public void Bridge(StringScriptableValue userSpeech) {
        chatBot.SubmitPrompt(userSpeech.Value);
    }
}