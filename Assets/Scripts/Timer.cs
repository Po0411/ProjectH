using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;
    enum TimerType { Countdown, Stopwatch}
    [SerializeField] private TimerType timeType;

    [SerializeField] private float timeToDisplay = 60.0f;

    private bool _isRunning;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;

    private void EventManagerOnTimerStop() => _isRunning = false;

    private void EventManagerOnTimerUpdate(float value) => timeToDisplay = value;

    private void Update()
    {
        if (!_isRunning) return;
        if (timeType == TimerType.Countdown && timeToDisplay < 0.0f) return;
        timeToDisplay += timeType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeSpan.ToString(format: @"mm\:ss\:ff");
    }
}
