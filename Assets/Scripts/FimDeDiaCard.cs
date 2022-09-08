using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FimDeDiaCard : MonoBehaviour
{
    [SerializeField]
    private FimDeDiaData FDD;
    [SerializeField]
    private Sprite[] cardSprites;
    
    private FDDData data;

    [SerializeField] 
    private TextMeshProUGUI title;
    [SerializeField] 
    private TimerUI timer;
    [SerializeField] 
    private FimDiaOptions options;

    public GameController gm;
    public UnityEvent OnCardEnd;
    private void OnEnable()
    {
        SetCard();
    }

    private void SetCard()
    {
        data = FDD.GetData();
        title.text = data.title;
        timer.SetTimer(data.timer);
        options.SetOptions(data.options);

        ChangeCardSprite();
    }

    private void ChangeCardSprite()
    {
        switch (data.options.Length)
        {
            case 2:
                GetComponent<Image>().sprite = cardSprites[0];
                break;
            case 3:
                GetComponent<Image>().sprite = cardSprites[1];
                break;
            case 4:
                GetComponent<Image>().sprite = cardSprites[2];
                break;
        }
    }

    public void RandomAnswer()
    {
        SelectAnswer(Random.Range(1,data.options.Length+1));
    }
    
    public void SelectAnswer(int answer)
    {
        if (answer == data.correctOption)
        {
            ChangePosition();
        }
        else
        {
            Debug.Log("Errou");
        }
        
        OnCardEnd.Invoke();
    }

    private void ChangePosition()
    {
        List<PlayerData> pDatas = gm.PlayerGameList;
        pDatas = pDatas.OrderByDescending(x => x.influencia).ToList();
        PlayerData top = pDatas[0];
        PlayerData bottom = pDatas[pDatas.Count - 1];
        int topValue = top.influencia;
        
        top.influencia = bottom.influencia;
        bottom.influencia = topValue;

        foreach (var player in gm.PlayerGameList)
        {
            if(player.charData == top.charData) player.influencia = top.influencia;
            else if(player.charData == bottom.charData) player.influencia = bottom.influencia;
        }
    }
}
