using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEnableStateManager : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectReference;
    public void DisableAfter(float seconds)
    {
        StartCoroutine(SetEnabledStateAfter(seconds, false));
    }

    public void EnableAfter(float seconds)
    {
        StartCoroutine(SetEnabledStateAfter(seconds, true));
    }

    private IEnumerator SetEnabledStateAfter(float seconds, bool enabledState)
    {
        yield return new WaitForSeconds(seconds);
        gameObjectReference.SetActive(enabledState);
    }
}
