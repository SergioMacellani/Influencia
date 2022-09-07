using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Influencia/Cards/Shop Data")]
public class ShopCardData : ScriptableObject
{
    public List<ShopCardInfo> data;
    
    public ShopCardInfo GetRandom(out int level)
    {
        ShopCardInfo info = data[Random.Range(0, data.Count)];
        level = Random.Range(0, info.levelStats.Count);
        return info;
    }
}

[System.Serializable]
public class ShopCardInfo
{
    public string title;
    public Sprite icon;
    public List<ShopCardStatsList> levelStats;

    public string GetTitle(int level)
    {
        return title + GetLevel(level);
    }
    
    private string GetLevel(int num)
    {
        string[] romanNumerals = { "I", "II", "III", "IV", "V" };
        return romanNumerals[num - 1];
    }
}

[System.Serializable]
public class ShopCardStatsList
{
    public ShopCardStats statsA;
    [Range(1, 5)] 
    public int levelA;
    
    [Space(2f)]
    public ShopCardStats statsB;
    [Range(1, 5)] 
    public int levelB;
    
    [Space(2f)]
    public ShopCardStats statsC;
    [Range(1, 5)] 
    public int levelC;
}

[System.Serializable]
public class ShopCardSelected
{
    public string title;
    public Sprite icon;
    public ShopCardStatsList stats;

    public void SetCard(ShopCardInfo info, int level)
    {
        title = $"{info.title} {GetLevel(level)}";
        icon = info.icon;
        stats = info.levelStats[level];
    }

    private string GetLevel(int num)
    {
        string[] romanNumerals = { "I", "II", "III", "IV", "V" };
        return romanNumerals[num];
    }
}
