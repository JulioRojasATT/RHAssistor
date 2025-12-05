using System.Collections.Generic;
using UnityEngine;

public class EntityPath : MonoBehaviour {
    [SerializeField] private List<Transform> pathCheckpoints;
    public List<Transform> PathCheckpoints => pathCheckpoints;
}
