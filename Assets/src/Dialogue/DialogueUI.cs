using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI actorName;
    
    [SerializeField] private TextMeshProUGUI actorDialogue;

    [SerializeField] private List<DecisionOptionUI> answerOptionsUIs;
    public List<DecisionOptionUI> AnswerOptionsUIs => answerOptionsUIs;

    public void SetDecisionOptions(List<DecisionOptionData> answersData) {
        answerOptionsUIs.ForEach(answerOption => answerOption.gameObject.SetActive(false));
        for (int i = 0; i < answerOptionsUIs.Count && i<answersData.Count; i++){
            answerOptionsUIs[i].gameObject.SetActive(true);
            answerOptionsUIs[i].LoadData(answersData[i]);
        }
    }

    public void SetActorName(string actorName) {
        this.actorName.text = actorName;
    }
    
    public void SetDialogue(string dialogue) {
        actorDialogue.text = dialogue;
    }
}