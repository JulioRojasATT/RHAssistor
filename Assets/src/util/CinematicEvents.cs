using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class CinematicEvents : MonoBehaviour
{
    [SerializeField] private PlayableDirector cinematicPlayer;

    [Header("Event Management")]
    [SerializeField] private UnityEvent onCinematicStarted;

    [SerializeField] private UnityEvent onCinematicEnded;

    private void Awake() {
        cinematicPlayer.played += OnCinematicStarted;
        cinematicPlayer.stopped += OnCinematicEnded;
    }

    private void OnCinematicEnded(PlayableDirector obj)
    {
        onCinematicEnded?.Invoke();
    }

    private void OnCinematicStarted(PlayableDirector obj)
    {
        onCinematicStarted?.Invoke();
    }
}
