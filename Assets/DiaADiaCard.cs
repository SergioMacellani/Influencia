using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiaADiaCard : MonoBehaviour
{
    [SerializeField] 
    private DiaADiaData data;
    private DaDData dad;
    private int timerValue = 60;
    [SerializeField] private TimerUI timer;
    [SerializeField] private TextMeshProUGUI option1;
    [SerializeField] private TextMeshProUGUI option2;

    public GameController gm;

    private void Start()
    {
        SetCard();
    }

    private void SetCard()
    {
        dad = data.GetData();
        timer.SetTimer(timerValue);
        option1.text = dad.option1.GetOption();
        option2.text = dad.option2.GetOption();
    }

    public void RandomSelect()
    {
        Select((int)Random.Range(0,2));
    }
    
    public void Select(int i)
    {
        //Esquerda
        if (i == 0)
        {
            
        }
        //Direita
        else
        {
            
        }
        
        gm.NextPlayer();
        transform.parent.gameObject.SetActive(false);
    }
}
