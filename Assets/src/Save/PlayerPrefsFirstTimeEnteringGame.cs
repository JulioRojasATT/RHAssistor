using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsFirstTimeEnteringGame : MonoBehaviour
{
    [SerializeField] private UnityEvent onFirstTimePlaying;

    [SerializeField] private UnityEvent onNotFirstTimePlaying;
    
    public void CheckIfFirstTimeEnteringGame()
    {
        if(PlayerPrefs.GetInt("FirstTimeEnteringGame", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTimeEnteringGame", 1);
            PlayerPrefs.Save();
            onFirstTimePlaying?.Invoke();
            return;
        }
        onNotFirstTimePlaying?.Invoke();
    }
}
