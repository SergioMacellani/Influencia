using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public bool IsSpecial;
    public bool IsCenter;
    public bool HaveCard;
    public int CardId;
    public int PlayerInHouse;
    
    [SerializeField][Tooltip("N,S,L,O")]
    private Quaternion houseDirections;

    public Quaternion HouseDirections 
    {
        get => houseDirections;
        set => houseDirections = value;
    }
    
    public Sprite sprite => GetComponent<SpriteRenderer>().sprite;
}
