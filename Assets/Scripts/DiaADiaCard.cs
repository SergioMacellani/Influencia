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
    private bool replayRound = false;

    private void OnEnable()
    {
        SetCard();
    }

    private void SetCard()
    {
        dad = data.GetData();
        replayRound = false;
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
            ExecuteStats(dad.option1.stats[0]);
        }
        //Direita
        else
        {
            ExecuteStats(dad.option2.stats[0]);
        }
        
        transform.parent.gameObject.SetActive(false);
    }

    private void ExecuteStats(DaDStats stats)
    {
        switch (stats.stats)
        {
            case DaDStats.StatsType.Influencia:
                gm.GetInfluencia(stats.value);
                break;
            case DaDStats.StatsType.Desperdicio:
                gm.SetDesperdicio(stats.value);
                break;
            case DaDStats.StatsType.Disposição:
                gm.SetDisposicao(stats.value);
                break;
            case DaDStats.StatsType.Rodada:
                replayRound = true;
                break;
        }
        
        if(!replayRound) gm.NextPlayer();
        else gm.GetRound();
    }
}
