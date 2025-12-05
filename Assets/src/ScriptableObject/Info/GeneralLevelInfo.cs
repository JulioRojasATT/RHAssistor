using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralLevelInfo : GeneralInfo
{
    [SerializeField] private int levelNumber;
    public int LevelNumber => levelNumber;

    [SerializeField] private string levelName;
    public string LevelName => levelName;

    [SerializeField] private string nextLevelName;
    public string NextLevelName => nextLevelName;

    [SerializeField] private string nextSceneName;
    public string NextSceneName => nextSceneName;

    [SerializeField] private bool isLastLevel;
    public bool IsLastLevel => isLastLevel;
}
