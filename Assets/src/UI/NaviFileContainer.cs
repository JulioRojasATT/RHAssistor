using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NaviFileContainer : MonoBehaviour {

    public Image saveImage;

    [SerializeField] private TextMeshProUGUI saveNameText;
    public string saveName;

    [SerializeField] private TextMeshProUGUI lastSceneSeenText;

    public InputField inputFieldText;

    public int slot;

    public void UpdateWithSaveData(Image saveImage, string saveName, string lastSceneSeenName, int slot)
    {
        this.saveImage = saveImage;
        this.saveName = saveName;
        saveNameText.text = saveName;
        lastSceneSeenText.text = lastSceneSeenName;
        this.slot = slot;
    }

    public void SetSaveName(string saveName)
    {
        this.saveName = saveName;
        if (this.saveNameText!= null) {
            this.saveNameText.text = saveName;
        }
    }

    public void SetSaveImage(Sprite newSaveImage)
    {
        if(this.saveImage!= null)
        {
            this.saveImage.sprite = newSaveImage;
        }
    }
}
