using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMonoBehaviourData : MonoBehaviour{
    [Header("UI Configuration")]
    public string Label;

    public string displayName;

    [TextArea(4, 4)]
    public string Description;

    public Sprite uiSprite;
}
