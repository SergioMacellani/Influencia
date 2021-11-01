using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHouses : MonoBehaviour
{
    public bool autoUpdate;
    public bool autoHouses;
    
    [SerializeField][Range(0,10)]
    private int HousesPerLine = 3;
    [SerializeField]
    private float HousesDist = 2.88f;
    
    public GameObject SpecialHouse_Prefab;
    public GameObject NormalHouse_Prefab;
    
    public bool debugOn;
    public Sprite spritePlaceHolder;
    public List<SpriteRenderer> houseLocal;
    public List<SpriteRenderer> specialHouseLocal;
    public List<HouseType> HouseTypes;
    public List<SpecialHouseType> SpecialHouseType;

    private Vector2 middleScreen => Vector2.zero;
    public Vector2 StartHousePos => middleScreen - (Vector2.one*(HousesDist*(HousesPerLine+1)));
    
    public void CreateMap()
    {
        
        GameObject centerSpecial = Instantiate(SpecialHouse_Prefab, transform);
        centerSpecial.transform.localPosition = middleScreen;
        specialHouseLocal.Add(centerSpecial.GetComponent<SpriteRenderer>());

        Vector2 pos = StartHousePos + (Vector2.up * HousesDist);
        Vector3 rot = Vector3.zero;
        
        for (int i = 0; i < 4; i++)
        {
            for (int f = 0; f < 2; f++)
            {
                for (int l = 0; l < HousesPerLine; l++)
                {
                    GameObject house = Instantiate(NormalHouse_Prefab, transform);
                    house.transform.localPosition = pos;
                    house.transform.localEulerAngles = rot;
                    houseLocal.Add(house.GetComponent<SpriteRenderer>());
                    pos += HousePosition(i,true);
                }
                
                if (f == 0)
                {
                    GameObject middleSpecial = Instantiate(SpecialHouse_Prefab, transform);
                    middleSpecial.transform.localPosition = pos;
                    middleSpecial.transform.localEulerAngles = rot;
                    specialHouseLocal.Add(middleSpecial.GetComponent<SpriteRenderer>());
                    pos += HousePosition(i,true);
                }
            }

            GameObject special = Instantiate(SpecialHouse_Prefab, transform);
            special.transform.localPosition = pos;
            specialHouseLocal.Add(special.GetComponent<SpriteRenderer>());

            rot -= Vector3.forward * 90;
            pos += HousePosition(i+1,true);
        }

        rot = Vector2.zero;
        for (int i = 1; i < specialHouseLocal.Count; i+=2)
        {
            pos = (Vector2)specialHouseLocal[i].transform.localPosition + HousePosition(i,false);
            rot -= Vector3.forward * 90;
            
            for (int l = 0; l < HousesPerLine; l++)
            {
                GameObject house = Instantiate(NormalHouse_Prefab, transform);
                house.transform.localPosition = pos;
                house.transform.localEulerAngles = rot;
                houseLocal.Add(house.GetComponent<SpriteRenderer>());
                pos += HousePosition(i,false);
            }
        }
        GameObject start = Instantiate(SpecialHouse_Prefab, transform);
        start.transform.localPosition = StartHousePos;
        specialHouseLocal.Add(start.GetComponent<SpriteRenderer>());
        

        CamSize();
    }

    private Vector2 HousePosition(int i, bool normalLine)
    {
        if (normalLine)
        {
            if (i == 0)
            {
                return Vector2.up * HousesDist;
            }
            else if (i == 1)
            {
                return Vector2.right * HousesDist;
            }
            else if (i == 2)
            {
                return Vector2.down * HousesDist;
            }
            else
            {
                return Vector2.left * HousesDist;
            }
        }
        else
        {
            if (i == 7)
            {
                return Vector2.up * HousesDist;
            }
            else if (i == 1)
            {
                return Vector2.right * HousesDist;
            }
            else if (i == 3)
            {
                return Vector2.down * HousesDist;
            }
            else
            {
                return Vector2.left * HousesDist;
            }
        }
    }

    public void CamSize()
    {
        Camera.main.orthographicSize = (((HousesPerLine+3) * 0.375f)*2) + HousesPerLine*.375f;
    }
    
    public void DeleteMap()
    {
        houseLocal.Clear();
        specialHouseLocal.Clear();
        while (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }

    public void RandomHousesType()
    {
        string lastType = "";
        int randomMax = RandomMax()/2;
        foreach (var house in houseLocal)
        {
            int r = Random.Range(0, 101);
            float n = 0;
            foreach (var type in HouseTypes)
            {
                house.sprite = type.HouseSprite;
                house.gameObject.name = type.Name;
                n += type.RarityValue;
                if (n > r && lastType != type.Name)
                {
                    lastType = type.Name;
                    break;
                }
            }
        }

        for (int i = 1; i < specialHouseLocal.Count; i++)
        {
            int r = Random.Range(0, 101);
            float n = 0;
            foreach (var type in SpecialHouseType)
            {
                specialHouseLocal[i].sprite = type.HouseSprite;
                specialHouseLocal[i].gameObject.name = type.Name;
                n += type.RarityValue;
                if (n > r && lastType != type.Name)
                {
                    lastType = type.Name;
                    break;
                }
            }
        }

        if (debugOn)
        {
            foreach (var house in houseLocal)
            {
                Debug.Log(house.gameObject.name);
            }
        }
    }

    public void ResetHouses()
    {
        foreach (var house in houseLocal)
        {
            house.sprite = spritePlaceHolder;
            house.name = NormalHouse_Prefab.name;
        }
    }
    
    int RandomMax()
    {
        int rM = 0;
        foreach (var type in HouseTypes)
        {
            rM += (int)type.RarityValue;
        }

        return rM;
    }
}

[System.Serializable]
public struct HouseType
{
    public enum RarityTypes
    {
        Comum,
        Rare,
        Special,
        Shop
    }
    public string Name;
    public RarityTypes Rarity;
    public Sprite HouseSprite;
    [Range(0, 100)] public float RarityValue;
}

[System.Serializable]
public struct SpecialHouseType
{
    public string Name;
    public Sprite HouseSprite;
    [Range(0, 100)] public float RarityValue;
}
