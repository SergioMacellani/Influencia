using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField]
    private ShopCardSelected data;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject statsContent;
    [SerializeField] private GameObject statsPrefab;

    public void SetCard(ShopCardSelected _data)
    {
        data = _data;
        title.text = data.title;
        icon.sprite = data.icon;
        
        var statA = Instantiate(statsPrefab, statsContent.transform);
        statA.GetComponent<ShopStatsUI>().SetStats(data.stats.levelA, data.stats.statsA);
        
        var statB = Instantiate(statsPrefab, statsContent.transform);
        statB.GetComponent<ShopStatsUI>().SetStats(data.stats.levelB, data.stats.statsB);
        
        var statC = Instantiate(statsPrefab, statsContent.transform);
        statC.GetComponent<ShopStatsUI>().SetStats(data.stats.levelC, data.stats.statsC);
    }
}
