using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NaviSaveDataManager : MonoBehaviour {

    [SerializeField] private NaviGeneralUIManager uiManager;

    [SerializeField] private TextAsset defaultSaveFile;

    [SerializeField] private IntScriptableValue currentLevelNumber;
    
    public static NaviSaveData currentSaveData;
    
    protected static bool isWebGL;
    
    private static string saveRegisterDefaultText = "\n\n";
    
    public static string appDataPath;
    
    private static readonly string RESOURCES_DATA_PATH = "/Resources/Data";
    
    public List<string> saveRegisterLines;
    
    public TextAsset defaultSaveDataFile;

    [SerializeField] private UnityEvent onPreSave;
    
    #if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();
    #endif

    protected virtual void Awake() {
        // First, app data path is set depending if using WebGL or not
        isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
        appDataPath = Application.dataPath;
        if (isWebGL) {
            Debug.Log("WebGL detected. Changing datapath");
            appDataPath = Application.persistentDataPath;
        }
        TryCreateSaveFileSystem();
        LoadSave(defaultSaveFile.name);
    }

    public void UpdateFileContainerWithCurrentSaveData(NaviFileContainer fileContainer)
    {
        if (currentSaveData==null)
        {
            return;            
        }
        currentSaveData.TryGetVariableValue("LastSeenScene", out string lastSceneName);
        currentSaveData.TryGetVariableValue("SaveName", out string saveName);
        fileContainer.UpdateWithSaveData(null, saveName, lastSceneName, fileContainer.slot);
    }
    
    /// <summary>
    /// Loads the save in the given container
    /// </summary>
    /// <param name="container"></param>
    public NaviSaveData LoadSave(string saveName) {
        return LoadSaveFile(appDataPath + RESOURCES_DATA_PATH + "/save/" + saveName + ".txt");
    }
    
    public NaviSaveData LoadSaveFile(string filePath) {
        string fileText = File.ReadAllText(filePath);
        currentSaveData = ParseData(fileText);
        Debug.Log("Trying to load save file at: " + filePath + "\n Contents: " + currentSaveData);
        return currentSaveData;
    }
    
    public NaviSaveData ParseData(string dataText) {
        string [] variablesText = Regex.Split(dataText, @"\n");
        Dictionary<string, NaviVariable> variables = new Dictionary<string, NaviVariable>();
        string[] tokens;
        for (int i = 0; i < variablesText.Length; i++) {
            tokens = Regex.Split(TextProcessing.Clean(variablesText[i], @"\s+"), @"=");
            Type variableType = TextProcessing.DetermineTypeOfString(tokens[1]);
            if (variableType != null) {
                variables.Add(tokens[0], new NaviVariable(variableType,TextProcessing.GetStringTValue(variableType, tokens[1])));
            }
        }
        return new NaviSaveData(variables);
    }
    
    public void CreateNewGameData(string playerName) {
        currentSaveData = ParseData(defaultSaveDataFile.text);        
        currentSaveData.SetVariableValue("SaveName", "temporalNotSaved");
    }
    
    public void CreateDummyData() {
        CreateNewGameData("Dummy Player");
    }

    public virtual void AutoSave(NaviFileContainer container)
    {
        Save(container.slot, GetPlayerName());
    }

    public void Save(int slot, string newSaveName) {
        // Updates the save register lines on the container index        
        saveRegisterLines[slot] = newSaveName;
        UpdateSaveRegister();
        currentSaveData.SetVariableValue("SaveName", newSaveName);        
        SaveCurrentDataToFile();
    }
    
    /// <summary>
    /// Writes the current save data, writing to its save name
    /// </summary>
    public virtual void SaveCurrentDataToFile() {
        onPreSave?.Invoke();
        string filePath = appDataPath + RESOURCES_DATA_PATH + "/save/" + currentSaveData.GetVariable("SaveName").value +
                          ".txt";
        Debug.Log("Trying to save file at: " + filePath + "\n Contents: " + currentSaveData);
        currentSaveData.SetVariableValue("LastSeenScene", SceneManager.GetActiveScene().name);
        currentSaveData.SetVariableValue("LastPlayedLevel", currentLevelNumber.Value);
        // Update last played level
        currentSaveData.TryGetVariableValue("MaxPlayedLevel", out int maxLevelPlayed);
        if (currentLevelNumber.Value > maxLevelPlayed) {
            currentSaveData.SetVariableValue("MaxPlayedLevel", currentLevelNumber.Value);
        }
        File.WriteAllText(filePath, "" + currentSaveData.ToString());
        if (isWebGL) {
            #if UNITY_WEBGL
            Debug.Log("Syncing");
            JS_FileSystem_Sync();
            #endif
        }
    }
    
    public void UpdatePlayerName(string newPlayerName) {
        currentSaveData.SetVariableValue("PlayerName", newPlayerName);
    }
    
    public void UpdateSaveRegister() {
        string result = "";
        for (var i = 0; i < saveRegisterLines.Count; i++) {
            result += saveRegisterLines[i];
            if (i < saveRegisterLines.Count - 1) {
                result += "\n";
            }
        }
        File.WriteAllText(appDataPath + RESOURCES_DATA_PATH + "/SaveFiles.txt",result);
        // Webgl sync
        #if UNITY_WEBGL
            if (isWebGL) {
            Debug.Log("Syncing");
            JS_FileSystem_Sync();
        }
        #endif
        
    }    

    /// <summary>
    /// Tries to create the save file register and directories.  
    /// </summary>
    public void TryCreateSaveFileSystem() {
        Directory.CreateDirectory(appDataPath + @"/Resources/Data/save");
        #if UNITY_WEBGL
            if (isWebGL) {
                        Debug.Log("Syncing");
                        JS_FileSystem_Sync();
                    }
        #endif
        
        // If save file register doesn't exist, create it
        if (!File.Exists(appDataPath + @"/Resources/Data/SaveFiles.txt")) {
            File.WriteAllText(appDataPath + @"/Resources/Data/SaveFiles.txt",
                saveRegisterDefaultText);
            // Webgl sync
            #if UNITY_WEBGL
            if (isWebGL) {
                        Debug.Log("Syncing");
                        JS_FileSystem_Sync();
                    }
            #endif
        }
        saveRegisterLines = File.ReadAllText(appDataPath + RESOURCES_DATA_PATH + "/SaveFiles.txt").Split('\n').ToList();
    }

    public string GetPlayerName()
    {
        return (string)currentSaveData.GetVariable("PlayerName").value;
    }
}
