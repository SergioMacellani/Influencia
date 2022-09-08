using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerText : MonoBehaviour
{
    public GameController gm;
    [TextArea] public string Text;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        gm ??= GameObject.Find("GameController").GetComponent<GameController>();
        TryGetComponent(out tmp);
    }

    void OnEnable()
    {
        if(Text.Contains("{name}")) tmp.text = Text.Replace("{name}", gm.PlayerNow.charData.name);
        if (Text.Contains("{random}"))
        {
            List<PlayerData> pData = new List<PlayerData>();
            pData.AddRange(gm.PlayerGameList);
            pData.Remove(gm.PlayerNow);
            tmp.text = Text.Replace("{random}", pData[Random.Range(0, pData.Count)].charData.name);
        }
        if(Text.Contains("{winner}")) tmp.text = Text.Replace("{winner}", gm.PlayerWinner.charData.name);
    }
}
