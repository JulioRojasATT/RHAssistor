using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaviManager : MonoBehaviour {
    
    [SerializeField] public NaviGeneralUIManager naviGeneralUIManager;

    [SerializeField] protected RuntimeAudioManager audioManagerReference;
    public RuntimeAudioManager AudioManager => audioManagerReference;

    [SerializeField] protected RuntimeLevelInfo levelInfoReference;
    protected GeneralLevelInfo levelInfo => levelInfoReference.Value;

    [Header("Save management")]
    [SerializeField] protected NaviSaveDataManager _saveDataManager;

    [SerializeField] private IntScriptableValue currentLevelValue;

    [SerializeField] private IntScriptableValue maxLevelPlayedValue;
    private int maxLevelPlayed => maxLevelPlayedValue.Value;

    public NaviSaveData currentSaveData;    

    [SerializeField] private StringScriptableValue levelPrefix;

    protected virtual void Start() {
        currentSaveData = NaviSaveDataManager.currentSaveData;
    }

    public void LoadSave(NaviFileContainer container) {
        LoadSave(container.saveName);
    }

    public void UpdateLevelInfoOnUI()
    {
        naviGeneralUIManager.UpdateLevelTexts(levelInfo);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        LoadScene(levelInfo.NextSceneName);
    }

    public void UpdateCurrentLevelValue() {
        currentLevelValue.SetValue(levelInfo.LevelNumber);
    }

    public void LoadMaxLevelPlayed() {
        LoadScene(levelPrefix.Value + "Level" + maxLevelPlayed);
    }
    
    /// <summary>
    /// Loading a file sets the name and flag so that when the scene loads, it loads the data correctly.
    /// </summary>
    /// <param name="saveName"></param>
    public void LoadSave(string saveName) {
        currentSaveData = _saveDataManager.LoadSave(saveName);
        LoadScene((string)currentSaveData.GetVariable("LastSeenScene").value);
    }

    public virtual void AutoSave(NaviFileContainer container){
        _saveDataManager.AutoSave(container);
        container.SetSaveName(_saveDataManager.GetPlayerName());
    }
    
    public virtual void ForceSave(NaviFileContainer container) {
        if (container.saveName == "") {
            Debug.LogError("Error, save name can't be empty");
            return;
        }
        _saveDataManager.Save(container.slot, container.inputFieldText.text);
        container.SetSaveName(container.inputFieldText.text);
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneCor(sceneName));
    }

    public IEnumerator LoadSceneCor(string sceneName) {
        yield return naviGeneralUIManager.OnSceneLoadTriggered();
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Gets all the save files in the 
    /// </summary>
    public void LoadSaveNamesOnSaveFileContainers() {
        List<string> saveRegisterLines = _saveDataManager.saveRegisterLines;
        Sprite currentSaveSprite = null;
        // Updates all the file save containers
        for (int i = 0; i < saveRegisterLines.Count && i<naviGeneralUIManager.saveFileContainers.Length; i++) {
            string currentSaveName = saveRegisterLines[i];
            // If save name is different from null, update it in the ui Manager
            if (naviGeneralUIManager.saveFileContainers!=null && !currentSaveName.Equals(String.Empty)) {
                naviGeneralUIManager.UpdateFileContainer(i,currentSaveName,currentSaveSprite);
            }
        }
    }
}