using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeviceFeaturesInfoUI : MonoBehaviour
{

    [SerializeField] private List<CharacteristicUI> characteristicsUIs;

    [SerializeField] private TextMeshProUGUI deviceName;

    [SerializeField] private ObjectSpawner objectSpawner;

    public void UpdateInfo()
    {
        if (objectSpawner.spawnedObject)
        {
            LoadInfo(objectSpawner.spawnedObject);
        }
    }

    public void LoadInfo(MonoBehaviourGeneralInfo itemInfo)
    {
        ItemInfo deviceInfo = itemInfo as ItemInfo;
        characteristicsUIs.ForEach(x => x.gameObject.SetActive(false));
        if (deviceInfo) {
            for (int i = 0;i < deviceInfo.Characteristics.Count && i<characteristicsUIs.Count; i++)
            {
                characteristicsUIs[i].LoadInfo(deviceInfo.Characteristics[i]);
                characteristicsUIs[i].gameObject.SetActive(true);
            }
            deviceName.text = deviceInfo.Label;
        }
    }
}
