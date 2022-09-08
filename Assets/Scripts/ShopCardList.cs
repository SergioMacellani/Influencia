using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardList : MonoBehaviour
{
    [SerializeField] 
    private ShopCardData data;
    [SerializeField]
    private ShopCard[] shopCards;

    public GameController gm;

    private void Start()
    {
        gm ??= FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        UpdateCards();
    }

    private void UpdateCards()
    {
        foreach (var card in shopCards)
        {
            ShopCardSelected selected = new ShopCardSelected();
            int level;
            selected.SetCard(data.GetRandom(out level), level);
            card.SetCard(selected);
        }
    }

    public void SelectCard(int card)
    {
        foreach (var cards in gm.PlayerNow.cards)
        {
            if(cards.getShopCardSelected.title == shopCards[card].getShopCardSelected.title)
            {
                gm.SetDesperdicio(5);
            }
        }
        
        gm.PlayerNow.cards.Add(shopCards[card]);
        gm.GetInfluencia(shopCards[card].getShopCardSelected.stats.Influencia);
        gm.SetDisposicao(shopCards[card].getShopCardSelected.stats.Disposição);
        if(shopCards[card].getShopCardSelected.stats.Rodada > 0) gm.GetRound();
    }
}
