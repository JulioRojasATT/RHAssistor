using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public void Print(string text) {
        Debug.Log(text);
    }

    public void Print(StringScriptableValue text)
    {
        Debug.Log(text.Value);
    }
}
