using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class NaviGeneralUIManager : GeneralUIManager {

    [Header("Data Management")]
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [SerializeField] private TextMeshProUGUI levelCompletedText;

    [SerializeField] private TextMeshProUGUI levelNameCompletedText;

    [SerializeField] private TextMeshProUGUI nextLevelName;

    [SerializeField] private DarkenerController darkenerController;

    [SerializeField] private IntScriptableValue maxLevelPlayedValue;
    private int maxLevelPlayed => maxLevelPlayedValue.Value;

    /// <summary>
    /// UI containers for the save files
    /// </summary>
    public NaviFileContainer[] saveFileContainers;

    public void UpdateFileContainer(int index, String newSaveText, Sprite newSaveSprite) {
        NaviFileContainer container = saveFileContainers[index];
        container.SetSaveName(newSaveText);
        container.SetSaveImage(newSaveSprite);
    }

    public void UpdateContinueLevelTextWithMaxLevelPlayed()
    {
        nextLevelName.text = "Nivel " + maxLevelPlayed;
    }

    public void UpdateLevelTexts(GeneralLevelInfo levelInfo)
    {
        currentLevelText.text = levelInfo.LevelName;
        levelCompletedText.text = "Nivel " + levelInfo.LevelNumber + " completado!";
        levelNameCompletedText.text = levelInfo.LevelName;        
        nextLevelName.text = levelInfo.NextLevelName;
    }

    public IEnumerator OnSceneLoadTriggered()
    {
        darkenerController.gameObject.SetActive(true);
        yield return darkenerController.OpaqueDarkenerCor();
    }
}
