using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOpen : MonoBehaviour
{
    public GameObject descanso;
    public GameObject gravacao;
    public GameObject parceria;
    public GameObject trend;

    public void Detector(HouseScript hs)
    {
        switch (hs.gameObject.name)
        {
            case "White":
                descanso.SetActive(true);
                break;
            case "Blue":
                gravacao.SetActive(true);
                break;
            case "Special_Purple":
                parceria.SetActive(true);
                break;
            case "Special_Red":
                trend.SetActive(true);
                break;
        }
    }
}
