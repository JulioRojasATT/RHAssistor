using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class GoogleFormConnector : MonoBehaviour{

    [SerializeField] private IntScriptableValue scoreValue;

    [SerializeField] private RuntimeLevelInfo runtimeLevelInfo;

    [SerializeField] private bool testMode; 

    [Header("Data")]
    [SerializeField] private IntScriptableValue maxLevelPlayed;

    [SerializeField] private IntScriptableValue maxLevelWon;

    [SerializeField] private IntScriptableValue totalLevelNumber;

    [SerializeField] private RuntimeLevelScoreInfo[] levelsScoreInfo;

    [SerializeField] private BoolScriptableValue registerNewView;

    [SerializeField] private BoolScriptableValue isWebGLMobile;
    
    [SerializeField] private BoolScriptableValue sendGameCompletedForm;

    private string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSeg1WbQK5vHWbilt3DRJZga0lbcPkApeU21qiBhr0UgR9j5qg/formResponse";

    private string microsoftFormsURL = "https://forms.office.com/formapi/api/2e716fbe-24c8-4fce-9588-dcb5ff25b01d/forms('vm9xLsgkzk-ViNy1_yWwHcb-9S15MuZGtHFctDoZ4PpUN1lISk5SSktCR0E2UTZIVkZVRkMxTkE0SC4u')/responses";

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K) && testMode)
        {
            OnLevelWon();
        }
    }

    public void OnLevelWon() {
        SubmitData("TestPlayer55", DateTime.Now, Time.realtimeSinceStartup);
        TrySubmitGameCompleted();
    }

    public void SubmitData(string name, DateTime timeOfPlay, float timePlayed)    
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("entry.1044457958", name);// You will find these in name (not id) attributes of the input tags          
        webForm.AddField("entry.1073947506_year", timeOfPlay.Year + ""); // Add date year
        webForm.AddField("entry.1073947506_month", timeOfPlay.Month + ""); // Add date month
        webForm.AddField("entry.1073947506_day", timeOfPlay.Day + ""); // Add date day
        webForm.AddField("entry.591391290", levelsScoreInfo[0].Value.score + ""); // Add level 1 score
        webForm.AddField("entry.1191256690", levelsScoreInfo[1].Value.score + ""); // Add level 2 score0
        webForm.AddField("entry.620225667", PlayedTimeToString(timePlayed)); // Add time played        
        // Add exact time played
        webForm.AddField("entry.388655228", Mathf.FloorToInt(timePlayed / 60f) + ":" + Mathf.FloorToInt(timePlayed % 60f)); 
        StartCoroutine(SendWebRequest(formURL,
            webForm));
    }

    public void TrySubmitGameCompleted() {
        if (!sendGameCompletedForm.Value) {
            Debug.Log("Can't sent game won form. Game already won or level not enough.");
            return;
        }
        Debug.Log("Sending game won forms request");
        WWWForm webForm = new WWWForm();
        // Tell if the game was completed
        webForm.AddField("entry.858098752", "Sí");
        StartCoroutine(SendWebRequest(formURL,
            webForm));
    }
    
    public void TryRegisterNewView()
    {
        if (!registerNewView.Value) {
            Debug.Log("Should not register new view");
            return;
        }
        Debug.Log("New view should be registered");
        WWWForm webForm = new WWWForm();
        webForm.AddField("entry.451634737", isWebGLMobile.Value ? "Android" : "PC");// You will find these in name (not id) attributes of the input tags          
        webForm.AddField("entry.1424358090","Sí"); // Add date year
        StartCoroutine(SendWebRequest(formURL,
            webForm));
    }

    private IEnumerator SendWebRequest(string formURL, WWWForm form)
    {
        UnityWebRequest webRequest = UnityWebRequest.Post(formURL, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success) {
            Debug.Log("Form sent successfully");
        } else {
            Debug.Log("Error while sending form");
            Debug.Log(webRequest.error);
        }

        yield break;
    }

    public string PlayedTimeToString(float playedTimeSeconds)
    {
        if (playedTimeSeconds > 600)
        {
            return ">10 min";
        }
        if (playedTimeSeconds > 300)
        {
            return "5-10 min";
        }
        if (playedTimeSeconds > 180)
        {
            return "3-5 min";
        }
        if (playedTimeSeconds > 60)
        {
            return "1-3 min";
        }
        return "<1 min";
    }
}

