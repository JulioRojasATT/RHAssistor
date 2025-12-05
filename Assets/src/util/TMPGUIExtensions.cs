using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPGUIExtensions : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;

    public void SetColor(ScriptableColor color)
    {
        text.color = color.Value;
    }
}
