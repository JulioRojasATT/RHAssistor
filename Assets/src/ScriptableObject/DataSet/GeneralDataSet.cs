using System.Collections.Generic;
using UnityEngine;

public class GeneralDataSet<T> : ScriptableObject where T : GeneralInfo {

    public List<T> items;

    /// <summary>
    /// Obtains the item with the same label as s
    /// </summary>
    /// <param name="s"></param>
    public T GetFromLabel(string s) {
        return items.Find(x => x.Label.Equals(s));
    }
    
    public T GetFromDisplayName(string s) {
        return items.Find(x => x.displayName.Equals(s));
    }
}
