using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ContainerUI<T> : MonoBehaviour where T: GeneralInfo {

    public T infoDisplayed;

    public Image UIImage;

    public abstract void LoadInfo(T entityInfo);

    public abstract void OnContainerHovered();

    public abstract void OnContainerSelected();

    protected void TryWriteText(TextMeshProUGUI textToUpdate, string text) {
        if(textToUpdate) {
            textToUpdate.text = text;
        }
    }
}
