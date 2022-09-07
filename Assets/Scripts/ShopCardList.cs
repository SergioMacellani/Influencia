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

    private void Start()
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
}
