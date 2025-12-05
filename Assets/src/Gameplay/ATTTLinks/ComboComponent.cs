using UnityEngine;
using TMPro;

public class ComboComponent : Interactable
{
    [SerializeField] private string id;

    [SerializeField] private int row;
    public int Row => row;

    public string ID
    {
        get => id;
        set => id = value;
    }

    [SerializeField] public TextMeshProUGUI uiText;

    public void SetUIText(string newText)
    {
        uiText.text = newText;
    }

    public void SetRow(int row)
    {
        this.row = row;
    }
    
}
