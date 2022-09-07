using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dia a Dia Data", menuName = "Influencia/Cards/DAD Data")]
public class DiaADiaData : ScriptableObject
{
    public List<DaDData> data;

    public DaDData GetData()
    {
        return data[Random.Range(0, data.Count)];
    }
}

[System.Serializable]
public class DaDData
{
    public string title;
    public DaDOption option1;
    public DaDOption option2;
}

[System.Serializable]
public class DaDOption
{
    public string option;
    public DaDStats[] stats;

    public string GetOption()
    {
        string text = "";
        text = $"{option}";

        foreach (var s in stats)
        {
            text += $"\n{GetValue(s.value)} {s.stats.ToString()}";
        }
        
        return text;
    }

    private string GetValue(int value)
    {
        string text = "";
        text = (value > 0 ? "+" : "");
        text += value;
        
        return text;
    }
}

[System.Serializable]
public class DaDStats
{
    public StatsType stats;
    public int value;
    public enum StatsType
    {
        Influencia,
        Rodada,
        Disposição,
        Desperdicio
    }
}