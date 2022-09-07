using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    void Update()
    {
        tmp.text = Text.Replace("{name}", gm.PlayerNow.charData.name);
    }
}
