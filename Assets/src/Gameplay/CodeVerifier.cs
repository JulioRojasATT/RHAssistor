using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CodeVerifier : MonoBehaviour {

    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private UnityEvent onIncorrectCodeSet;

    [SerializeField] private List<CodeAction> codeActions;

    [SerializeField] private Dictionary<string, CodeAction> actionsByName;

    private void Awake() {
        actionsByName = codeActions.ToDictionary(x => x.code);
    }

    public void TestCode() {
        if(!actionsByName.ContainsKey(inputField.text)) {
            onIncorrectCodeSet.Invoke();
            return;
        }
        actionsByName[inputField.text].onCorrectCodeSet?.Invoke();
    }

    public void AddTextToField(string text) {
        inputField.text += text;
    }

    public void ClearTextField() {
        inputField.text = "";
    }

    public void TrimLastCharacter() {
        if(inputField.text.Length<= 0)
        {
            return;
        }
        inputField.text = inputField.text.TrimEnd(inputField.text[inputField.text.Length - 1]);
    }
}


[Serializable]
public class CodeAction {
    public string code;

    public UnityEvent onCorrectCodeSet;
}