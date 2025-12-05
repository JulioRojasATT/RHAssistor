using System.Collections.Generic;
using System;
using UnityEngine;
/*using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;*/
using System.Collections.Specialized;
using System.Net;

public class GoogleSheetsConnector : MonoBehaviour
{
    /*[Header("Sheets API Connection")]
    private string serviceAccountEmail = "attwuser@attwords.iam.gserviceaccount.com";

    [SerializeField] private string spreadSheetId = "1UDc2Jhb8FyCCOTheeAQXPJdQgC1kmvg3hkALIDF9x3Y";

    [SerializeField] private string testPlayerName= "TestPlayer";

    private int nameRowNumber = 0;

    [SerializeField] private TextAsset jsonFile;

    ServiceAccountCredential credential;

    [Header("Data")]
    [SerializeField] private KeyCode tryUpdateKey;

    [SerializeField] private IntScriptableValue maxLevelPlayed;

    [SerializeField] private RuntimeLevelScoreInfo[] levelsScoreInfo;

    [SerializeField] private BoolScriptableValue registerNewView;

    private void Update()
    {
        if(Input.GetKeyDown(tryUpdateKey)) {
            UpdatePlayedData(testPlayerName, DateTime.Now, 50, null);
        }
    }

    private void Awake()
    {
        GetServiceAccountCredentials();
        Initialize(testPlayerName);
    }

    public void TryRegisterNewView() {
        if (registerNewView.Value) {
            RegisterView();
        }
    }

    public void TryUpdateTestData()
    {
        UpdatePlayedData(testPlayerName, DateTime.Now, 50, levelsScoreInfo);
    }

    public void GetCell()
    {
        WebClient client = new WebClient();
        NameValueCollection nameValue = new NameValueCollection();
        nameValue.Add("entry.1044457958", name);// You will find these in name (not id) attributes of the input tags 
        Uri uri = new Uri("https://docs.google.com/forms/d/e/1FAIpQLSeg1WbQK5vHWbilt3DRJZga0lbcPkApeU21qiBhr0UgR9j5qg/formResponse");
        byte[] response = client.UploadValues(uri, "POST", nameValue);
    }

    public void GetServiceAccountCredentials()
    {
        Debug.Log("Obtaining account credentials");
        ServiceAccountCredential serviceAccountCredential;
        string[] Scopes = { SheetsService.Scope.Spreadsheets };
        string jsonFileContents = jsonFile.text;
        serviceAccountCredential = (ServiceAccountCredential)
                GoogleCredential.FromJson(jsonFileContents).UnderlyingCredential;
        Debug.Log("Obtained service account credential");

        ServiceAccountCredential.Initializer initializer = new ServiceAccountCredential.Initializer(serviceAccountCredential.Id)
        {
            User = serviceAccountEmail,
            Key = serviceAccountCredential.Key,
            Scopes = Scopes
        };
        serviceAccountCredential = new ServiceAccountCredential(initializer);
        credential = serviceAccountCredential;
        Debug.Log("Service Account Credential obtained");
    }    

    public ValueRange GetSingleValue(string valueRange)
    {
        SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = credential });
        Debug.Log("Obtaining single value");
        SpreadsheetsResource.ValuesResource.GetRequest getRequest = sheetsService.Spreadsheets.Values.Get(spreadSheetId, valueRange);
        Debug.Log("Obtained single value");
        return getRequest.Execute();
    }

    public void RemoveSingleValue(string valueRange)
    {
        SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = credential });
        var removeValuesRequest = sheetsService.Spreadsheets.Values.Clear(new ClearValuesRequest(), spreadSheetId, valueRange);
        removeValuesRequest.Execute();
    }

    /// <summary>
    /// Gets the row number in which the user's name is found, or the new one in the case in which it is created
    /// </summary>
    /// <param name="playerName"></param>
    public void Initialize(string playerName)
    {
        Debug.Log("Initializing");
        string namesValueRange = "Hoja1!A2:A1000";
        nameRowNumber = 2;
        ValueRange valueRange = GetSingleValue(namesValueRange);
        for (int i = 0; i < valueRange.Values.Count; i++)
        {
            nameRowNumber++; 
            if (playerName.Equals(valueRange.Values[i][0].ToString()))
            {
                nameRowNumber = i + 2;
                return;
            }
        }
        AppendDummyData(testPlayerName, DateTime.Now, 50, null);
    }

    public void RegisterView()
    {
        Debug.Log("Trying to register view");
        string viewsRange = "General!A2:A2";
        ValueRange viewsValueRange = GetSingleValue(viewsRange);
        if (!int.TryParse(viewsValueRange.Values[0][0].ToString(), out int views)) {
            Debug.LogError("Error, views on spread sheet isn't an int.");
            return;            
        }
        Debug.Log("Views are " + viewsValueRange.Values[0][0].ToString());
        ValueRange dataRow = new ValueRange();
        dataRow.Values = new List<IList<object>>()
            {
                new List<object>()
                {
                    (views + 1) + ""
                }
            };
        // Request creation & execution
        SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = credential });
        SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest =
            sheetsService.Spreadsheets.Values.Update(dataRow, spreadSheetId, viewsRange);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();        
    }

    public void UpdatePlayedData(string name, DateTime timeOfPlay, int timePlayed, RuntimeLevelScoreInfo[] levelsScoreInfo)
    {
        Debug.Log("Updating player data");
        string updateRange = "Hoja1!A" + nameRowNumber + ":L" + nameRowNumber;

        ValueRange dataRow = new ValueRange();
        // Level row population
        List<IList<object>> newValues = new List<IList<object>>();
        List<object> rowData = new List<object>()
            {
                name, timeOfPlay.ToString(), "" + timePlayed
            };
        for(int i = 0; i<levelsScoreInfo.Length; i++) {
            rowData.Add(levelsScoreInfo[i].Value.status);
            rowData.Add(levelsScoreInfo[i].Value.elapsedTime);
            rowData.Add(levelsScoreInfo[i].Value.score);
        }
        newValues.Add(rowData);
        dataRow.Values = newValues;
        // Request creation & execution
        SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = credential });
        SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest =
            sheetsService.Spreadsheets.Values.Update(dataRow, spreadSheetId, updateRange);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();
    }

    public void AppendDummyData(string name, DateTime timeOfPlay, int timePlayed, RuntimeLevelScoreInfo[] levelsScoreInfo)
    {
        string appendRange = "Hoja1!A2:L1000";
        ValueRange dataRow = new ValueRange();
        // Level row population
        List<IList<object>> newValues = new List<IList<object>>();
        List<object> rowData = new List<object>()
            {
                name, timeOfPlay.ToString(), "" + timePlayed
            };
        for(int i = 0; i<levelsScoreInfo.Length; i++) {
            rowData.Add(levelsScoreInfo[i].Value.status);
            rowData.Add(levelsScoreInfo[i].Value.elapsedTime);
            rowData.Add(levelsScoreInfo[i].Value.score);
        }
        newValues.Add(rowData);
        dataRow.Values = newValues;
        // Request creation & execution
        SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = credential });
        Debug.Log("Created append request");
        SpreadsheetsResource.ValuesResource.AppendRequest appendRequest = 
            sheetsService.Spreadsheets.Values.Append(dataRow, spreadSheetId, appendRange);
        appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        Debug.Log("Insert value parameters added");
        appendRequest.Execute();
        Debug.Log("Executing append request");
    }*/
}
