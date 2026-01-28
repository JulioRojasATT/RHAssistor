using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ATTCustomerChat : MonoBehaviour {
    
    [Header("Connection")]
    [SerializeField] private string openAIApiKey;

    [Header("Memory initialization")]
    [SerializeField] private TextAsset initialSystemContext;

    public List<string> systemMessages;
    
    List<TrainingSimMessage> conversation = new List<TrainingSimMessage>();
    public List<TrainingSimMessage> Conversation => conversation;

    private TrainingSimMessage _systemMessage;
    
    private const string OpenAIUrl = "https://api.openai.com/v1/chat/completions";

    [Header("Summarize parameters")]
    [SerializeField] [Range(1,5)] private int summarizeThreshold;

    [SerializeField] private StringScriptableValue chatResponse;
    public StringScriptableValue ChatResponse => chatResponse;

    [Header("Test Mode")]
    [SerializeField] private TextAsset testJson;

    private void Awake() {
        // Adding initial context
        AddMessageToConversation("system", initialSystemContext.text);
    }

    public void SendMessageToChatGPT(string userMessage)
    {
        StartCoroutine(SendRequest(userMessage));
    }

    public IEnumerator SendMessageAndWaitResponse(string userMessage) {
        yield return SendRequest(userMessage);
    }

    private IEnumerator SendRequest(string userMessage)
    {
        AddMessageToConversation("user", userMessage);
        var requestBody = new ChatRequest
        {
            model = "gpt-4o-mini",
            messages = conversation.ToArray()
        };

        string jsonBody = JsonUtility.ToJson(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using UnityWebRequest request = new UnityWebRequest(OpenAIUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {openAIApiKey}");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"OpenAI Error: {request.error}\n{request.downloadHandler.text}");
        }
        else
        {
            Debug.Log("OpenAI Response:");
            Debug.Log(request.downloadHandler.text);
            ProcessChatGPTResponse(request.downloadHandler.text);
        }
    }

    #region JSON Processing

    void ProcessChatGPTResponse(string jsonContent){
        var root = JSON.Parse(jsonContent);
        foreach (var kvp in root) {
            ProcessChatGPTResponse(kvp);
        }
    }
    
    void ProcessChatGPTResponse(KeyValuePair<string, JSONNode> subgroup) {
        foreach (var kvp in subgroup.Value) {
            Debug.Log(kvp.Key + " value is " + kvp.Value + " and has type " + kvp.GetType());
            if (subgroup.Key.Equals("choices") || kvp.Key.Equals("message")) {
                ProcessChatGPTResponse(kvp);
            }
            if (kvp.Key.Equals("content")) {
                Debug.Log("Message content is: " + kvp.Value);
                chatResponse.SetValue(kvp.Value);
                AddMessageToConversation("assistant", kvp.Value);
                ProcessChatGPTResponse(kvp);
            }
        }
    }

    #endregion

    #region Memory Management

    public void AddMessageToConversation(string role, string content) {
        conversation.Add(new TrainingSimMessage(role, content));
        systemMessages.Add(role + " : " + content);
    }
    
    public void RemoveMessageFromConversation(int conversationIndex) {
        systemMessages.RemoveAt(conversationIndex);
    }

    #endregion

    #region Summarize

    bool ShouldSummarize()
    {
        return conversation.Count > summarizeThreshold;
    }
    
    string BuildSummarizationPrompt(List<TrainingSimMessage> oldMessages)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Summarize this conversation into memory facts:\n");

        foreach (var msg in oldMessages)
        {
            if (msg.role != "system")
                sb.AppendLine($"{msg.role.ToUpper()}: {msg.content}");
        }

        return sb.ToString();
    }

    #endregion
    
    #region Test mode

    public void ParseTestJson() {
        Debug.Log("Parsing " + testJson.text);
        ProcessChatGPTResponse(testJson.text);
    }

    #endregion
}