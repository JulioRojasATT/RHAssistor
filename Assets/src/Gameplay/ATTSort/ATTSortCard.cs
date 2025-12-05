using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATTSortCard : Interactable {
    /// <summary>
    /// Queue this sort card is in
    /// </summary>
    public TransformQueue queue;

    /// <summary>
    /// Last queue this card was in.
    /// </summary>
    public TransformQueue lastQueue;

    public string conceptName;

    public void SetConceptName(string conceptName, GameObject cardPrefab)
    {
        this.conceptName = conceptName;
        cardPrefab.transform.position = transform.position;
        cardPrefab.transform.parent = transform;        
    }
}
