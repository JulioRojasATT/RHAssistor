using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicUI : MonoBehaviour
{
    [SerializeField] private Slider starsSlider;

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI descriptionText;

    public void LoadInfo(Characteristic characteristic)
    {
        starsSlider.value = characteristic.Stars;
        nameText.text = characteristic.Label;
        descriptionText.text = characteristic.Description;
    }
}