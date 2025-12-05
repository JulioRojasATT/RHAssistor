using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsLinker : MonoBehaviour{

    [SerializeField] private UnityEvent onFirstTimePlaying;

    [SerializeField] private UnityEvent onFirstTimeNotPlaying;

    [SerializeField] private KeyCode resetPrefsKeyCode = KeyCode.R;

    [SerializeField] private KeyCode deletePrefsKeyCode = KeyCode.J;    

    [SerializeField] private BoolScriptableValue canResetPlayerPrefs;
    
    [SerializeField] private BoolScriptableValue hasCompletedGame;
    
    [SerializeField] private BoolScriptableValue sendGameCompletedForm;

    [SerializeField] protected RuntimeLevelInfo levelInfoReference;
    protected GeneralLevelInfo levelInfo => levelInfoReference.Value;
    public int currentlevelNumber => levelInfo.LevelNumber;

    [Header("Level Data Load")]
    [SerializeField] private IntScriptableValue maxLevelPlayedValue;

    [SerializeField] private IntScriptableValue maxLevelNumberValue;
    private int maxLevelNumber => maxLevelNumberValue.Value;

    [SerializeField] private IntScriptableValue maxLevelWonValue;

    [SerializeField] private RuntimeLevelScoreInfo[] levelScoreInfos;


    private void Update() {
        if (!canResetPlayerPrefs.Value)
        {
            return;
        }
        if (Input.GetKeyDown(resetPrefsKeyCode)) {
            ResetPlayerPrefs();
        }
        if (Input.GetKeyDown(deletePrefsKeyCode))
        {
            DeleteAllPreferences();
        }
    }

    public void OnLevelWon()
    {
        int maxLevelWon = PlayerPrefs.GetInt("MaxLevelWon", 0);
        maxLevelPlayedValue.SetValue(maxLevelWon);
        if (levelInfoReference && levelInfoReference.Value.LevelNumber > maxLevelWon)
        {
            maxLevelWonValue.SetValue(levelInfo.LevelNumber);
            PlayerPrefs.SetInt("MaxLevelWon", maxLevelWonValue.Value);
            PlayerPrefs.Save();
        }
        // Only send a game completed form if we won the last level and haven't completed the game before.
        sendGameCompletedForm.SetValue(currentlevelNumber >= maxLevelNumberValue.Value && !hasCompletedGame.Value);
        PlayerPrefs.SetInt("HasCompletedGame", hasCompletedGame.Value || currentlevelNumber >= maxLevelNumberValue.Value ? 1 : 0);
    }

    public void ReadData()
    {
        int maxLevelPlayed = PlayerPrefs.GetInt("MaxLevelPlayed", 1);
        maxLevelPlayedValue.SetValue(maxLevelPlayed);
        if (levelInfoReference && levelInfoReference.Value.LevelNumber > maxLevelPlayed)
        {
            PlayerPrefs.SetInt("MaxLevelPlayed", levelInfoReference.Value.LevelNumber);
            PlayerPrefs.Save();
        }
        int i;
        for(i = 0; i < maxLevelPlayed; i++)
        {
            levelScoreInfos[i].SetValue(new LevelScoreInfo(0,0,"Completed"));
        }
        for (; i < maxLevelNumber; i++)
        {
            levelScoreInfos[i].SetValue(new LevelScoreInfo(0, 0, "Uncompleted"));
        }
        hasCompletedGame.SetValue(PlayerPrefs.GetInt("HasCompletedGame", 0)==1);
    }

    public void CheckIfFirstTimePlaying()
    {
        if(PlayerPrefs.GetInt("FirstTimePlaying", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTimePlaying", 1);
            PlayerPrefs.Save();
            onFirstTimePlaying?.Invoke();
            return;
        }
        onFirstTimeNotPlaying?.Invoke();
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }

    public void ResetPlayerPrefs() {
        Debug.Log("Resetting player prefs");
        PlayerPrefs.SetInt("FirstTimeEnteringGame", 0);
        PlayerPrefs.SetInt("FirstTimePlaying", 0);
        PlayerPrefs.SetInt("MaxLevelPlayed", 1);
        PlayerPrefs.SetInt("HasCompletedGame", 0);
        PlayerPrefs.Save();
    }

    public void DeleteAllPreferences()
    {
        Debug.Log("Deleting player prefs");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
