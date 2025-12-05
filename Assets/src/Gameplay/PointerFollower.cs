using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    [SerializeField] private BoolScriptableValue followPointer;

    [SerializeField] private Vector3ScriptableValue pointerWorldPositionValue;
    private Vector3 pointerWorldPosition => pointerWorldPositionValue.Value;

    // Update is called once per frame
    void Update()
    {
        if (!followPointer.Value) {
            return;
        }
        transform.position = pointerWorldPosition;
    }
}
