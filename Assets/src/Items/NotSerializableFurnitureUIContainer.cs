using TMPro;
using UnityEngine;

public class NotSerializableFurnitureUIContainer: MonoBehaviour {

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI descriptionText;

    public void SetName(string newName)
    {
        nameText.text = newName;
    }

    public void SetDescription(string newDescription){
        descriptionText.text = newDescription;
    }

    public void SetDescription(TextAsset textAsset)
    {
        descriptionText.text = textAsset.text;
    }
}
