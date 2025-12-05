using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralUIManager : MonoBehaviour
{
    public void TryWriteText(TextMeshProUGUI uiText, string text)
    {
        if(uiText!=null)
        {
            uiText.text = text;
        }
    }
}
