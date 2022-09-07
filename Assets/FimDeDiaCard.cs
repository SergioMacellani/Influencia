using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private void Start()
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
            Debug.Log("Acertou");
        }
        else
        {
            Debug.Log("Errou");
        }
        
        gm.NextPlayer();
        transform.parent.gameObject.SetActive(false);
    }
}
