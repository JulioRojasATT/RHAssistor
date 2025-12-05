using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizationReplacer : MonoBehaviour {    

    [SerializeField] private IntScriptableValue languageID;

    [SerializeField] private LocalizedString autoLoadString;

    [SerializeField] private bool autoLoadAtEnable;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (!autoLoadAtEnable) {
            return;            
        }
        LoadLocalizedString(autoLoadString);
    }

    public void LoadLocalizedString(LocalizedString localizedString) {
        text.text = localizedString.GetLanguageString(languageID.Value);
    }
}