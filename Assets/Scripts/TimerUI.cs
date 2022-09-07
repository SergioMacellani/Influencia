using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] [Range(0, 240)]
    private int timerSeconds;
    [SerializeField] [Range(0, 240)]
    private int timerValue = 999;
    private float TimerFill => Mathf.Abs(1-((float)timerValue / (float)timerSeconds));

    private Vector3 TimerAngle => new Vector3(0, 0, -(TimerFill * 360));
    
    [Space]
    [Header("UI")]
    [SerializeField] 
    private Image timerImage;
    [SerializeField] 
    private RectTransform timerPointer;
    [SerializeField] 
    private RectTransform timerPointerBackground;
    [SerializeField] 
    private TextMeshProUGUI[] timerText;

    [SerializeField] private bool onlyShowSeconds = false;
    [SerializeField] private bool startOnEnable = false;

    public UnityEvent OnTimerEnd;

    private void OnEnable()
    {
        if(!startOnEnable) return;
        
        SetTimer(timerSeconds);
    }

    public void SetTimer(int seconds)
    {
        timerSeconds = seconds;
        timerValue = seconds;
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        UpdateUI();
        if(timerValue <= 0) OnTimerEnd.Invoke();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateUI();
    }
#endif

    private void UpdateUI()
    {
        timerImage.fillAmount = TimerFill;
        timerPointer.localEulerAngles = TimerAngle;
        if(timerPointerBackground != null)
            timerPointerBackground.localEulerAngles = TimerAngle;
        
        foreach (var text in timerText)
        {
            if (onlyShowSeconds)
                text.text = timerValue.ToString();
            else
                text.text = GetTime();
        }
    }

    private string GetTime()
    {
        int minutes = timerValue / 60;
        int secondsLeft = timerValue % 60;
        return $"{minutes:00}:{secondsLeft:00}";
    }
    
    private IEnumerator StartTimer()
    {
        while (timerValue > 0)
        {
            yield return new WaitForSeconds(1);
            timerValue--;
        }
    }
}
