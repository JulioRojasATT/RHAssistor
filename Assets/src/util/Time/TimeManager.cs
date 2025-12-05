using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Default Speeds")]
    [Range(0f, 2f)] public float normalTimeScale = 1f;
    [Range(0f, 1f)] public float slowTimeScale = 0.5f;
    [Range(1f, 5f)] public float fastTimeScale = 2f;

    [Header("Optional Events")]
    public UnityEvent onPause;
    public UnityEvent onUnpause;
    public UnityEvent onSlow;
    public UnityEvent onFast;
    public UnityEvent onNormal;

    private float previousTimeScale = 1f;
    private bool isPaused = false;

    // Call this to pause time
    public void PauseTime()
    {
        if (!isPaused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            isPaused = true;
            onPause?.Invoke();
        }
    }

    // Call this to resume time from pause
    public void UnpauseTime()
    {
        if (isPaused)
        {
            Time.timeScale = previousTimeScale > 0f ? previousTimeScale : normalTimeScale;
            isPaused = false;
            onUnpause?.Invoke();
        }
    }

    // Set normal time scale
    public void SetNormalTime()
    {
        Time.timeScale = normalTimeScale;
        isPaused = false;
        onNormal?.Invoke();
    }

    // Set slow motion
    public void SetSlowTime()
    {
        Time.timeScale = slowTimeScale;
        isPaused = false;
        onSlow?.Invoke();
    }

    // Set fast motion
    public void SetFastTime()
    {
        Time.timeScale = fastTimeScale;
        isPaused = false;
        onFast?.Invoke();
    }

    // Toggle pause/unpause
    public void TogglePause()
    {
        if (isPaused)
            UnpauseTime();
        else
            PauseTime();
    }

    // Get current time scale
    public float GetTimeScale()
    {
        return Time.timeScale;
    }

    // Check if the game is paused
    public bool IsPaused()
    {
        return isPaused;
    }
}
