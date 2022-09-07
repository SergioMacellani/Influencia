using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimDiaOptions : MonoBehaviour
{
    [SerializeField] private OptionsList twoOptions;
    [SerializeField] private OptionsList threeOptions;
    [SerializeField] private OptionsList fourOptions;

    public void SetOptions(string[] options)
    {
        switch (options.Length)
        {
            case 2:
                twoOptions.gameObject.SetActive(true);
                twoOptions.SetOptions(options);
                break;
            case 3:
                threeOptions.gameObject.SetActive(true);
                threeOptions.SetOptions(options);
                break;
            case 4:
                fourOptions.gameObject.SetActive(true);
                fourOptions.SetOptions(options);
                break;
        }
    }
}
