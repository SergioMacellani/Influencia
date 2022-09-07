using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player List Data", menuName = "Influencia/Player/Player List")]
public class PlayerList : ScriptableObject
{
    public int index;
    public List<PlayerData> players;

    public PlayerData GetPlayer()
    {
        return players[index];
    }
}

[System.Serializable]
public class PlayerData
{
    public CharacterData charData;
    public GameObject charObject;
    public int influencia;
    public int disposicao;
    public List<ShopCard> cards;
}
