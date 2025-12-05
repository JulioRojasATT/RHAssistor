using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ComboManager : MonoBehaviour{

    [Header("Level flow")]
    [SerializeField] private int currentComboIndex;

    [SerializeField] private RuntimeLevelInfo runtimeLevelInfo;
    List<ComboDefinition> _combos;

    [SerializeField] private List<UnityEvent> onComboDetectedEvents;

    [SerializeField] private ConceptCreator conceptCircleCreator;

    [Header("Combo info management")]
    [SerializeField] private TextMeshProUGUI comboClueText;

    [SerializeField] private Image comboUIImage;

    [SerializeField] private Image comboUIImageZoom;

    [SerializeField] private RuntimeAudioManager runtimeAudioManager;

    [Header("Level Management")]
    [SerializeField] private BoolScriptableValue isGameWon;
    
    [SerializeField] private UnityEvent onComboFailed;

    [SerializeField] private UnityEvent onComboDetected;

    [SerializeField] private UnityEvent onGameWon;
    
    /// <summary>
    /// Called when the user wants to continue the game but has already won.
    /// </summary>
    [SerializeField] private UnityEvent onGameContinue;

    /// <summary>
    /// Called when the user wants to continue the game but has already won.
    /// </summary>
    [SerializeField] private UnityEvent onGameContinueWhenWon;

    private void Awake()
    {        
        LevelInfo attLinksLevelInfo = (LevelInfo)runtimeLevelInfo.Value;
        if (!attLinksLevelInfo)
        {
            return;
        }
        _combos = attLinksLevelInfo.Combos;
        _combos.ForEach(c => c.Initialize());
        comboClueText.text = _combos[currentComboIndex].Clue;
        comboUIImage.sprite = _combos[currentComboIndex].PromoSprite;
        comboUIImageZoom.sprite = _combos[currentComboIndex].PromoSprite;
    }

    private void Start() {
        conceptCircleCreator.UpdateComboNamesAndIds(_combos[currentComboIndex].IDS, _combos[currentComboIndex].RowIndexes);
    }
    
    public void ContinueGame() {
        if (!isGameWon.Value) {
            onGameContinue?.Invoke();
            return;
        }
        onGameContinueWhenWon?.Invoke();
    }

    public void CheckCurrentComboIsDetected(string[] ids)
    {
        if(currentComboIndex>=_combos.Count || ids.Length<=0)
        {
            return;
        }
        if (!_combos[currentComboIndex].IsSameCombo(ids))
        {
            onComboFailed?.Invoke();
            return;
        }
        comboUIImage.sprite = _combos[currentComboIndex].PromoSprite;
        comboUIImageZoom.sprite = _combos[currentComboIndex].PromoSprite;
        runtimeAudioManager.PlayInterruptable(_combos[currentComboIndex].DescriptiveAudio);
        onComboDetectedEvents[currentComboIndex].Invoke();
        currentComboIndex++;
        onComboDetected?.Invoke();
        if(currentComboIndex< _combos.Count) {
            comboClueText.text = _combos[currentComboIndex].Clue;
            conceptCircleCreator.UpdateComboNamesAndIds(_combos[currentComboIndex].IDS, _combos[currentComboIndex].RowIndexes);
            return;
        }
        onGameWon?.Invoke();
    }
}