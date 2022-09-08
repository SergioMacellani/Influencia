using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopStatsUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Image _typeIcon;
    [SerializeField]
    private Image _typeIcon2;
    [SerializeField] 
    private TextMeshProUGUI[] _valuesText;
    [SerializeField] 
    private ValuesColors _colors;
    
    [Space(2)]
    [SerializeField] 
    private Image _background;
    [SerializeField] 
    private Image _fillImage;

    [Space] [Header("Values")] 
    [SerializeField] [Range(1,5)]
    private int _value;

    [SerializeField] 
    private Sprite _typeSprite;
    
    [SerializeField] 
    private float[] _valuePos = new float[6] {0, 1, 1.95f, 2.92f, 3.93f, 5};

    public void SetStats(int value, Sprite spr)
    {
        _value = value;
        _typeSprite = spr;
        
        SetStats();
    }

    private void SetStats()
    {
        _slider.value = _valuePos[_value];
        _fillImage.enabled = _value > 0;
        _typeIcon.sprite = _typeSprite;
        _typeIcon2.sprite = _typeSprite;

        for (int i = 0; i < 5; i++)
        {
            _valuesText[i].color = GetTextColor(i);
        }
    }

    private Color GetTextColor(int i)
    {
        i++;
        if (i < _value)
        {
            return _colors._valuePrev;
        }
        else if(i == _value)
        {
            return _colors._valueSelect;
        }
        else
        {
            return _colors._valueNext;
        }
    }

    private void OnValidate()
    {
        _background.color = _colors._background;
        _fillImage.color = _colors._fill;
        SetStats();
    }
}

[System.Serializable]
public class ValuesColors
{
    public Color _background = Color.HSVToRGB(40,74,53);
    public Color _fill = Color.HSVToRGB(47,100,100);
    public Color _valuePrev = Color.HSVToRGB(0,0,100);
    public Color _valueSelect = Color.HSVToRGB(47,48,100);
    public Color _valueNext = Color.HSVToRGB(0,0,25);
}
