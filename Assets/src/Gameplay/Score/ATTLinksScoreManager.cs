using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATTLinksScoreManager : ScoreManager
{
    [SerializeField] private FloatScriptableValue remainingCountdownTimeValue;

    [SerializeField] private IntScriptableValue countDownStarBonus;

    [SerializeField] private IntScriptableValue starScoreBonus;

    [Header("Score update management")]
    [SerializeField] private RuntimeLevelInfo currentLevelInfoValue;
    private GeneralLevelInfo currentLevelInfo => currentLevelInfoValue.Value;

    [SerializeField] private RuntimeLevelScoreInfo[] levelsScoreInfo;

    [Header("UI Elements")]
    [SerializeField] private GameObject[] starsGameObjects;

    [SerializeField] private GameObject[] starsGameObjectTexts;

    [SerializeField] private TextMeshProUGUI timeBonusText;

    public override void CalculateScore()
    {        
        for (int i = 0 ;i < starsGameObjects.Length && i<countDownStarBonus.Value; i++)
        {
            starsGameObjects[i].SetActive(true);
            starsGameObjectTexts[i].SetActive(true);
        }
        timeBonusText.text = "+ " + Mathf.Floor(remainingCountdownTimeValue.Value);
        scoreValue.SetValue(Mathf.FloorToInt(remainingCountdownTimeValue.Value) + countDownStarBonus.Value * starScoreBonus.Value + defaultScore.Value);
        levelsScoreInfo[currentLevelInfo.LevelNumber - 1].Value.score = scoreValue.Value;
    }    
}
