using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemVersionsUI : MonoBehaviour
{
    [SerializeField] private ObjectSpawner objectSpawner;

    [SerializeField] private ItemInfo currentItemDisplayed;

    [SerializeField] private List<Button> versionsButtons;

    public void UpdateInfo()
    {
        if (objectSpawner.spawnedObject)
        {
            LoadInfo(objectSpawner.spawnedObject);
        }
    }

    public void LoadInfo(MonoBehaviourGeneralInfo objectToSpawn)
    {
        ItemInfo itemInfo = (ItemInfo) objectToSpawn;
        if (!itemInfo)
        {
            return;
        }
        if(itemInfo.Versions.Count <= 1)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        currentItemDisplayed = itemInfo;
        versionsButtons.ForEach(button => { button.gameObject.SetActive(false); });
        int i;
        for (i = 0; i < itemInfo.Versions.Count; i++)
        {
            versionsButtons[i].gameObject.SetActive(true);
            versionsButtons[i].image.color = itemInfo.Versions[i].UIColor;
            versionsButtons[i].image.sprite = itemInfo.Versions[i].UIImage;
            versionsButtons[i].onClick.RemoveAllListeners();
            string versionToSwapTo = itemInfo.Versions[i].ID;
            versionsButtons[i].onClick.AddListener(() => SwapItemVersion(versionToSwapTo));
        }
    }

    public void SwapItemVersion(string versionToSwapTo)
    {
        currentItemDisplayed.SwapToVersion(versionToSwapTo);
    }
}
