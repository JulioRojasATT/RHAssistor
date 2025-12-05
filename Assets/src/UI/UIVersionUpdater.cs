using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVersionUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    public void UpdateVersionText()
    {
        versionText.text = "v" + Application.version;
    }
}
