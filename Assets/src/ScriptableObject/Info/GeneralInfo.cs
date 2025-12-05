using UnityEngine;

public class GeneralInfo : ScriptableObject {

    [Header("UI Configuration")]
    public string Label;
    
    public string displayName;

    [TextArea(4,4)]
    public string Description;
    
    public Sprite uiSprite;
}
