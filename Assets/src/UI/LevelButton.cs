using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI levelNumberText;

    [SerializeField] private NaviManager levelManager;

    private string _levelSceneName;

    public void DisableInteraction() {
        button.interactable = false;
    }
    
    public void Initialize(int levelNumber, string levelSceneName) {
        levelNumberText.text = levelNumber + "";
        _levelSceneName = levelSceneName;
    }

    public void LoadSceneInButtonData() {
        levelManager.LoadScene(_levelSceneName);
    }
}