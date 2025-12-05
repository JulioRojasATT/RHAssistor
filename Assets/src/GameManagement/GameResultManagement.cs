using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameResultManagement : MonoBehaviour
{
    [Header("Game won management")]
    [SerializeField] private GenericEventBroadcaster onGameWonEventBroadCaster;
    private EventHandler<EventArgs> onGameWon;
    private UnityEvent onGameWonEvent;

    [Header("Game lost management")]
    [SerializeField] private GenericEventBroadcaster onGameLostEventBroadCaster;
    private EventHandler<EventArgs> onGameLost;
    private UnityEvent onGameLostEvent;

    private void Awake()
    {
        onGameWon += (x,y) => { onGameWonEvent.Invoke(); };
        onGameWonEventBroadCaster.Subscribe(onGameWon);
        onGameLost += (x, y) => { onGameLostEvent.Invoke(); };
        onGameLostEventBroadCaster.Subscribe(onGameLost);
    }

    private void OnDestroy()
    {
        onGameWonEventBroadCaster.Unsubscribe(onGameWon);
        onGameLostEventBroadCaster.Unsubscribe(onGameLost);
    }
}