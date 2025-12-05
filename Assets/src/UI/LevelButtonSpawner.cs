using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonSpawner : ScreenSizeAdapter {

    [SerializeField] private int levelNumber;

    [SerializeField] private LevelButtonPool levelButtonPool;

    [SerializeField] private List<LevelButton> alreadyExistingInstances;

    [SerializeField] private IntScriptableValue maxLevelPlayedValue;
    private int maxLevelPlayed => maxLevelPlayedValue.Value;

    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private RectTransform contentContainer;

    [SerializeField] private StringScriptableValue levelPrefixValue;


    private string _levelPrefix => levelPrefixValue.Value;

    private float originalLayoutHeight = 1483, originalLayoutWidth = 828;

    private void Awake() {
        levelButtonPool.RegisterAsAvailable(alreadyExistingInstances);
    }
    
    public void ResizeGridLayoutAfterSeconds(float seconds) {
        StartCoroutine(ResizeGridLayoutAfterSecondsCor(seconds));
    }

    public IEnumerator ResizeGridLayoutAfterSecondsCor(float seconds) {
        yield return new WaitForSeconds(seconds);
        ResizeGridLayoutUsingContainerDimensions();
    }

    public void ResizeGridLayoutUsingContainerDimensions() {
        gridLayoutGroup.padding.left = Mathf.FloorToInt(Adapt(originalLayoutWidth, contentContainer.rect.width, gridLayoutGroup.padding.left));
        gridLayoutGroup.padding.right = Mathf.FloorToInt(Adapt(originalLayoutWidth, contentContainer.rect.width,gridLayoutGroup.padding.right));
        gridLayoutGroup.padding.bottom = Mathf.FloorToInt(Adapt(originalLayoutHeight, contentContainer.rect.height,gridLayoutGroup.padding.bottom));
        gridLayoutGroup.padding.top = Mathf.FloorToInt(Adapt(originalLayoutHeight, contentContainer.rect.height,gridLayoutGroup.padding.top));
        gridLayoutGroup.spacing = new Vector2(Adapt(originalLayoutWidth, contentContainer.rect.width,gridLayoutGroup.spacing.x),
            Adapt(originalLayoutHeight, contentContainer.rect.height,gridLayoutGroup.spacing.y));
        gridLayoutGroup.cellSize = Vector2.one * (contentContainer.rect.width - gridLayoutGroup.padding.left - 
                                                  gridLayoutGroup.padding.right - gridLayoutGroup.spacing.x * 3f) / 4f;
    }

    public void SpawnLevelButtons() {
        /*NaviVariable maxLevelPlayedVariable= NaviSaveDataManager.currentSaveData.GetVariable("MaxPlayedLevel");
        if(maxLevelPlayedVariable== null || !int.TryParse(maxLevelPlayedVariable.value.ToString(), out maxLevelPlayed))
        {
            return;
        }*/
        //NaviSaveDataManager.currentSaveData.TryGetVariableValue("MaxPlayedLevel", out maxLevelPlayed);
        levelButtonPool.ForEachAvailable(x=>x.gameObject.SetActive(false));
        int i = 0;
        for (; i < maxLevelPlayed; i++) {
            LevelButton instanceUsed = levelButtonPool.OccupyOne(out bool createdNewButton);
            instanceUsed.gameObject.SetActive(true);
            instanceUsed.Initialize(i + 1,_levelPrefix + "Level" + (i + 1));
        }
        for (; i < levelNumber; i++) {
            LevelButton instanceUsed = levelButtonPool.OccupyOne(out bool createdNewButton);
            instanceUsed.gameObject.SetActive(true);
            instanceUsed.Initialize(i + 1,_levelPrefix + "Level" + (i + 1));
            instanceUsed.DisableInteraction();
        }
    }

    public override void AutoAdapt()
    {
        return;
    }
}