using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressUI : MonoBehaviour {

    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI progressText;

    public void SetProgress(int progress) {
        slider.value = progress;
        progressText.text = "Progreso: " + progress + "/" + Mathf.RoundToInt(slider.maxValue);
    }
}
