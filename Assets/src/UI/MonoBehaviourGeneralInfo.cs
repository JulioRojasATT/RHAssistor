using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourGeneralInfo : MonoBehaviour {
    [Header("Item Info")]
    [SerializeField] private string label;
    public string Label => label;
}
