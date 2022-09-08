using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOpen : MonoBehaviour
{
    public GameObject descanso;
    public GameObject gravacao;
    public GameObject parceria;
    public GameObject trend;
    public GameObject cansado;

    public GameController gm;

    private void Awake()
    {
        gm ??= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void Detector(HouseScript hs)
    {
        switch (hs.gameObject.name)
        {
            case "White":
                descanso.SetActive(true);
                break;
            case "Blue":
                DisposicaoGasta(3, gravacao);
                break;
            case "Special_Purple":
                DisposicaoGasta(5, parceria);
                break;
            case "Special_Red":
                DisposicaoGasta(5, trend);
                break;
        }
    }

    private void DisposicaoGasta(int disp, GameObject cnv)
    {
        if(gm.PlayerNow.disposicao - disp <= 0)
        {
            cansado.SetActive(true);
        }
        else
        {
            cnv.SetActive(true);
        }
    }
}
