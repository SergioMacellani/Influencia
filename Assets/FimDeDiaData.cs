using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FDD Data", menuName = "Influencia/Cards/FDD Data")]
public class FimDeDiaData : ScriptableObject
{
    public List<FDDData> data;

    public FDDData GetData()
    {
        FDDData d = data[Random.Range(0, data.Count)];
        
        if (d.isUsed)
            return GetData();
        else
            return d;
    }
}

[System.Serializable]
public class FDDData
{
    [TextArea(2, 5)] 
    public string title;
    public int id;
    public int timer;
    public bool isUsed;
    
    [Space]
    public string[] options;
    public int correctOption;
}
