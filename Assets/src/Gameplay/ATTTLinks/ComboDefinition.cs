using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="New Combo", menuName ="ATTWords/Combo")]
public class ComboDefinition : ScriptableObject{
    [SerializeField] List<string> ids;
    public List<string> IDS => ids;

    [SerializeField] List<int> rowIndexes;
    public List<int> RowIndexes => rowIndexes;

    [SerializeField] List<string> correctIds;

    private HashSet<string> idsHashSet;

    [SerializeField] private Sprite promoSprite;
    public Sprite PromoSprite => promoSprite;

    [SerializeField] private string clue;
    public string Clue => clue;

    /// <summary>
    /// Audio that describes the combo
    /// </summary>
    [SerializeField] private AudioClip descriptiveAudio;
    public AudioClip DescriptiveAudio => descriptiveAudio;

    public void Initialize()
    {
        idsHashSet = correctIds.ToHashSet();
    }

    public bool IsSameCombo(string[] ids)
    {
        if(ids.Length!=idsHashSet.Count)
        {
            return false;
        }
        // If hashset doesn't contain an id, the combo isn't the same
        foreach (string id in ids)
        {
            if (!idsHashSet.Contains(id))
            {
                return false;
            }
        }
        return true;
    }
}
