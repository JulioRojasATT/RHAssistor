using System;
using UnityEngine;
using Proyecto26;
using RSG;
using UnityEngine.Events;

public class FirebaseRestConnector : MonoBehaviour {
    
    private string targetURL = "https://atkinsdatasave.firebaseio.com/.json";

    [Header("Control Flags")]
    [SerializeField] private bool canRegisterErrors;
    public bool CanRegisterErrors => canRegisterErrors;

    [Header("Scriptable Values")]
    [SerializeField] private IntScriptableValue scoreValue;

    [SerializeField] private RuntimeLevelInfo runtimeLevelInfo;
    private GeneralLevelInfo CurrentLevelInfo => runtimeLevelInfo.Value; 

    [Header("Data")]
    [SerializeField] private StringScriptableValue userID;

    [SerializeField] private StringScriptableValue databasePrefix;
    
    [SerializeField] private IntScriptableValue errorAmount;

    [SerializeField] private RuntimeLevelScoreInfo[] levelsScoreInfo;

    [SerializeField] private BoolScriptableValue registerNewView;

    [SerializeField] private BoolScriptableValue isWebGLMobile;
    
    [SerializeField] private BoolScriptableValue isAndroid;

    [SerializeField] private BoolScriptableValue sendGameCompletedForm;

    [SerializeField] private UnityEvent<FirebaseUserData> onUserDataRetrieved;
    
    [SerializeField] private UnityEvent onUserDataRetrievalFailed;
    
    [Header("Test mode")]
    private static FirebaseUserData _userData;

    public void CreateUserTestData() {
        Debug.Log("Creating user test data");
        _userData = new FirebaseUserData(userID.Value);
    }

    public void TryIncreaseUserViews() {
        if(registerNewView.Value) {
            _userData.uniqueViews++;
        }
    }

    /// <summary>
    /// Gets the view device
    /// </summary>
    public void TrySetViewDevice() {
        if (!registerNewView.Value)
        {
            return;
        }
        if (!isWebGLMobile.Value) {
            _userData.timesPlayedInPC++;
            return;
        }
        if (isAndroid.Value) {
            _userData.timesPlayedInAndroid++;
            return;
        }
        _userData.timesPlayedIniOS++;
    }

    public void TryIncreaseErrors()
    {
        if (_userData==null)
        {
            return;
        }
        _userData.errorAmount++;
    }

    public void SetCanRegisterErrors(bool canRegisterErrors)
    {
        this.canRegisterErrors = canRegisterErrors;
    }

    public void RetrieveUserData() {
        Debug.Log("Retrieving user data");
        RetrieveUserData(userID.Value);
    }

    public void RetrieveUserData(string playerID) {
        IPromise<FirebaseUserData> request = RestClient.Get<FirebaseUserData>("https://atkinsdatasave-default-rtdb.firebaseio.com/" + databasePrefix.Value +
                                                                              "/" + playerID + ".json");
        request.Then(SetUserData);
        request.Catch(HandleRetrieveDataFailed);
    }

    public void HandleRetrieveDataFailed(Exception exception) {
        onUserDataRetrievalFailed.Invoke();
        Debug.Log("User Data request failed. Creating user data.");
    }

    public void OnLevelWon(RuntimeLevelInfo runtimeLevelInfo) {
        Debug.Log("USer data is " + _userData);
        _userData.OnLevelWon(CurrentLevelInfo.IsLastLevel, CurrentLevelInfo.LevelNumber-1, scoreValue.Value, Time.timeSinceLevelLoad);
    }

    public void SetUserData(FirebaseUserData firebaseUserData) {
        _userData = firebaseUserData;
        onUserDataRetrieved.Invoke(firebaseUserData);
    }

    public void PostUserData() {
        // if user data is equal to dummy data, that means an error arised when retrieving user data or it hasn't been received. Either case
        if (_userData.id.Equals("Dummy5064")) {
            return;
        }
        Debug.Log("Posting to firestore database");
        RestClient.Put("https://atkinsdatasave-default-rtdb.firebaseio.com/" + databasePrefix.Value + "/" + _userData.id + ".json", _userData);
    }
}
