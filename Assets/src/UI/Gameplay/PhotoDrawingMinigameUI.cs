using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoDrawingMinigameUI : MonoBehaviour {
    
    [Header("Completed")]
    [SerializeField] private Slider completedPercentageSlider;

    [SerializeField] private TextMeshProUGUI completedPercentageText;
    
    [Header("Excess")]
    [SerializeField] private Slider excessPercentageSlider;

    [SerializeField] private TextMeshProUGUI excessPercentageText;

    public void UpdateCompletedPercentage(float completedPercentage) {
        completedPercentageSlider.value = completedPercentage;
        completedPercentageText.text = Mathf.FloorToInt(completedPercentage * 100) + "%";
    }
    
    public void UpdateExcessPercentage(float excessPercentage) {
        excessPercentageSlider.value = excessPercentage;
        excessPercentageText.text = Mathf.FloorToInt(excessPercentage * 100) + "%";
    }
}
