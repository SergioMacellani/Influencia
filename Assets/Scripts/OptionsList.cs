using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsList : MonoBehaviour
{
    public TextMeshProUGUI[] options;
    
    public void SetOptions(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            this.options[i].text = GetOptionText(options[i],i);
        }
    }
    
    private string GetOptionText(string option, int index)
    {
        string opt = "";
        switch (index)
        {
            case 0:
                opt = "A) ";
                break;
            case 1:
                opt = "B) ";
                break;
            case 2:
                opt = "C) ";
                break;
            case 3:
                opt = "D) ";
                break;
        }
        return opt + option;
    }
    
}
