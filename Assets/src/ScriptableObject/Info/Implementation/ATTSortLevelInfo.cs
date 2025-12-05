using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ATTSort Level", menuName = "GX/ATTSort/New ATTSort Level")]
public class ATTSortLevelInfo : GeneralLevelInfo
{
    public List<string> conceptsNames;

    public List<GameObject> conceptPrefabs;

    public int queueMaxItems;
}
