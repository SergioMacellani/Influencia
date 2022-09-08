using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI influenciaPlayer;
    public TextMeshProUGUI disposicaoPlayer;
    public TextMeshProUGUI desperdicioWorld;

    public GameController gm;

    private void Awake()
    {
        gm ??= FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        influenciaPlayer.text = gm.PlayerNow.influencia.ToString();
        disposicaoPlayer.text = gm.PlayerNow.disposicao.ToString();
        desperdicioWorld.text = gm.desperdicioPoints.ToString();
    }
}
