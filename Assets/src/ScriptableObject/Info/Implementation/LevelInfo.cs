using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Info", menuName = "WordATT/Level Info")]
public class LevelInfo : GeneralLevelInfo {
    
    [SerializeField] private List<ComboDefinition> combos;
    public List<ComboDefinition> Combos => combos;
}
