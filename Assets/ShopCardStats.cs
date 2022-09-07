using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Stats", menuName = "Influencia/Data/Shop Stats")]
public class ShopCardStats : ScriptableObject
{
    public StatsType type;
    public CharacterData.ProffessionType proffession;
    public Sprite typeIcon;
    public Sprite proffessionIcon;

    public enum StatsType
    {
        Influencia,
        Rodada,
        Disposição
    }
}