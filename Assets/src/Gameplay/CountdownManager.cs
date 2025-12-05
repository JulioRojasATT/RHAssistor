using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private float countDown;

    [SerializeField] private FloatScriptableValue remainingTimeValue;

    [Header("Events")]
    [SerializeField] private UnityEvent onCountDownEnded;

    [SerializeField] private List<CountdownTimeReachedEvent> timeReachedEvents;

    [Header("UI")]
    [SerializeField] private Slider timeSlider;

    private Coroutine _countdownCoroutine;

    private bool _paused;

    public void StartCountdown(float refreshRate)
    {
        StopCounting();
        _countdownCoroutine = StartCoroutine(StartCountdownCor(countDown, refreshRate));
    }

    public IEnumerator StartCountdownCor(float time, float refreshRate = 1)
    {
        float elapsedTime = 0;
        timeSlider.maxValue = time;        
        while (elapsedTime < time)
        {
            yield return new WaitForSeconds(refreshRate);
            if (_paused)
            {
                continue;
            }
            elapsedTime += refreshRate;
            remainingTimeValue.SetValue(time - elapsedTime);
            timeSlider.value = time - elapsedTime;
            timeReachedEvents.ForEach(e =>
            {
                if (time - elapsedTime > e.Time || e.executed) return;
                e.OnTimeReached?.Invoke();
                e.executed = true;
            });
        }
        remainingTimeValue.SetValue(0);
        timeSlider.value = 0;
    }

    public void StopCounting()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }
    }

    public void Pause() => _paused = true;

    public void UnPause() => _paused = false;
}

[System.Serializable]
public class CountdownTimeReachedEvent {

    [SerializeField] private float time;
    public float Time => time;

    public bool executed;

    [SerializeField] private UnityEvent onTimeReached;
    public UnityEvent OnTimeReached => onTimeReached;
}