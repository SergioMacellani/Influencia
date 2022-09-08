using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField]
    private ShopCardSelected data;
    public ShopCardSelected getShopCardSelected { get { return data; } }
    
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject statsContent;
    [SerializeField] private GameObject statsPrefab;
    [SerializeField] private Sprite[] icons;

    public void SetCard(ShopCardSelected _data)
    {
        data = _data;
        title.text = data.title;
        icon.sprite = data.icon;
        
        var statA = Instantiate(statsPrefab, statsContent.transform);
        statA.GetComponent<ShopStatsUI>().SetStats(data.stats.Influencia, icons[0]);
        
        var statB = Instantiate(statsPrefab, statsContent.transform);
        statB.GetComponent<ShopStatsUI>().SetStats(data.stats.Disposição, icons[1]);
        
        var statC = Instantiate(statsPrefab, statsContent.transform);
        statC.GetComponent<ShopStatsUI>().SetStats(data.stats.Rodada, icons[2]);
    }
}
