using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NaviSaveData {
    
    public Dictionary<string, NaviVariable> variables;

    public NaviSaveData(Dictionary<string, NaviVariable> variables) {
        this.variables = variables;
    }

    public NaviVariable GetVariable(string variableId) {
        if (!variables.ContainsKey(variableId)) {
            Debug.Log("Error, variable " + variableId + " is not contained");
        }
        return variables[variableId];
    }
    
    public bool TryGetVariableValue<T>(string variableId, out T value){
        if (!variables.ContainsKey(variableId)) {
            Debug.Log("Error, variable " + variableId + " is not contained");
            value = default;
            return false;
        }
        TypeConverter foo = TypeDescriptor.GetConverter(typeof(T));
        value = (T) foo.ConvertFromInvariantString(variables[variableId].value.ToString());
        return true;
    }
    
    public void SetVariableValue(string variableId, object newValue) {
        variables[variableId].value = newValue;
    }

    public override string ToString() {
        string result = "";
        int elementCount = 0;
        foreach (KeyValuePair<string,NaviVariable> keyValuePair in variables) {
            result += keyValuePair.Key + "=" + keyValuePair.Value.value;
            if (elementCount < variables.Count - 1) {
                result += "\n";
            }
            elementCount++;
        }

        return result;
    }

    public bool VariableExists(string id) {
        return variables.ContainsKey(id);
    }
}
