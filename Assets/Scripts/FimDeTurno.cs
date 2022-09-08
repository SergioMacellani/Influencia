using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FimDeTurno : MonoBehaviour
{
    public GameController gm;
    public TextMeshProUGUI rankText;
    
    void Awake()
    {
        gm ??= GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnEnable()
    {
        List<PlayerData> pDatas = gm.PlayerGameList;
        string rank = "";
        int i = 1;
        pDatas = pDatas.OrderByDescending(x => x.influencia).ToList();

        foreach (var pd in pDatas)
        {
            rank += $"{i}º - {pd.charData.name} - {pd.influencia} Influencia\n";
            i++;
        }
        
        rankText.text = rank;
    }
}
