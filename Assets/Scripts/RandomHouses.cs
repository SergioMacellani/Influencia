using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomHouses : MonoBehaviour
{
    public bool autoUpdate;
    public bool autoHouses;
    public bool changeCamSize;
    public bool debugOn;
    
    [SerializeField][Range(0,8)]
    private int HousesPerLine = 3;
    [SerializeField]
    private float HousesDist = 2.88f;
    
    public GameObject SpecialHouse_Prefab;
    public GameObject NormalHouse_Prefab;

    public Sprite spritePlaceHolder;
    private List<SpriteRenderer> houseLocal = new List<SpriteRenderer>();
    private List<SpriteRenderer> specialHouseLocal = new List<SpriteRenderer>();
    public List<HouseType> HouseTypes;
    [SerializeField]
    private List<SpecialHouseType> SpecialHouseType = new List<SpecialHouseType>(2);
    [SerializeField]
    private List<SpecialHouseType> SpecialHouseCornerType = new List<SpecialHouseType>(2);

    private Vector2 middleScreen => Vector2.zero;
    public Vector2 StartHousePos => middleScreen - (Vector2.one*(HousesDist*(HousesPerLine+1)));
    
    public void CreateMap()
    {
        GameObject centerSpecial = Instantiate(SpecialHouse_Prefab, transform);
        centerSpecial.transform.localPosition = middleScreen;
        centerSpecial.name = "Center House";
        centerSpecial.AddComponent<HouseScript>().HouseDirections = new Quaternion(1,1,1,1);
        centerSpecial.GetComponent<HouseScript>().IsCenter = true;
        centerSpecial.GetComponent<HouseScript>().CardId = 0;
        specialHouseLocal.Add(centerSpecial.GetComponent<SpriteRenderer>());

        Vector2 pos = StartHousePos + (Vector2.up * HousesDist);
        Vector3 rot = Vector3.zero;
        int id = 0;
        
        for (int i = 0; i < 4; i++)
        {
            for (int f = 0; f < 2; f++)
            {
                id++;
                
                for (int l = 0; l < HousesPerLine; l++)
                {
                    GameObject house = Instantiate(NormalHouse_Prefab, transform);
                    house.transform.localPosition = pos;
                    house.transform.localEulerAngles = rot;
                    house.AddComponent<HouseScript>().CardId = id;
                    houseLocal.Add(house.GetComponent<SpriteRenderer>());
                    pos += HousePosition(i,true);
                    id++;
                }

                if (f == 0)
                {
                    GameObject middleSpecial = Instantiate(SpecialHouse_Prefab, transform);
                    middleSpecial.transform.localPosition = pos;
                    middleSpecial.AddComponent<HouseScript>().IsSpecial = true;
                    middleSpecial.GetComponent<HouseScript>().CardId = id;
                    MiddleDirections(i, middleSpecial.GetComponent<HouseScript>());
                    //middleSpecial.transform.localEulerAngles = rot;
                    specialHouseLocal.Add(middleSpecial.GetComponent<SpriteRenderer>());
                    pos += HousePosition(i,true);
                }
            }

            GameObject special = Instantiate(SpecialHouse_Prefab, transform);
            special.transform.localPosition = pos;
            special.AddComponent<HouseScript>().IsSpecial = true;
            special.GetComponent<HouseScript>().CardId = id;
            CornersDirections(i, special.GetComponent<HouseScript>());
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
                id++;
                GameObject house = Instantiate(NormalHouse_Prefab, transform);
                house.transform.localPosition = pos;
                house.transform.localEulerAngles = rot;
                house.AddComponent<HouseScript>().CardId = id;
                houseLocal.Add(house.GetComponent<SpriteRenderer>());
                pos += HousePosition(i,false);
            }
        }

        
        if(changeCamSize)
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

    private void MiddleDirections(int i, HouseScript hs)
    {
        if (i == 0)
        {
            hs.HouseDirections = new Quaternion(1, 1, 1, 0);
        }
        else if (i == 1)
        {
            hs.HouseDirections = new Quaternion(0, 1, 1, 1);
        }
        else if (i == 2)
        {
            hs.HouseDirections = new Quaternion(1, 1, 0, 1);
        }
        else
        {
            hs.HouseDirections = new Quaternion(1, 0, 1, 1);
        }
    }
    
    private void CornersDirections(int i, HouseScript hs)
    {
        if (i == 0)
        {
            hs.HouseDirections = new Quaternion(0, 1, 1, 0);
        }
        else if (i == 1)
        {
            hs.HouseDirections = new Quaternion(0, 1, 0, 1);
        }
        else if (i == 2)
        {
            hs.HouseDirections = new Quaternion(1, 0, 0, 1);
        }
        else
        {
            hs.HouseDirections = new Quaternion(1, 0, 1, 0);
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
                    if (type.Name == "Green")
                        house.GetComponent<HouseScript>().HaveCard = true;
                    break;
                }
            }
        }

        SpecialHouseGen(1,SpecialHouseType);
        SpecialHouseCornerGen(2, SpecialHouseCornerType.OrderBy(house => Random.Range(0,2)).ToList());
        
        if (debugOn)
        {
            foreach (var house in houseLocal)
            {
                Debug.Log(house.gameObject.name);
            }
        }
    }

    private void SpecialHouseGen(int i, List<SpecialHouseType> SPT)
    {
        foreach (var type in SPT)
        {
            if (Random.Range(0, 2) < 1)
            {
                specialHouseLocal[i].sprite = type.HouseSprite;
                specialHouseLocal[i].gameObject.name = type.HouseName;
                i += 2;
                specialHouseLocal[i].sprite = type.AdjacentHouseSprite;
                specialHouseLocal[i].gameObject.name = type.AdjacentHouseName;
                i += 2;
            }
            else
            {
                specialHouseLocal[i].sprite = type.AdjacentHouseSprite;
                specialHouseLocal[i].gameObject.name = type.AdjacentHouseName;
                i += 2;
                specialHouseLocal[i].sprite = type.HouseSprite;
                specialHouseLocal[i].gameObject.name = type.HouseName;
                i += 2;
            }
        }
    }
    
    private void SpecialHouseCornerGen(int i, List<SpecialHouseType> SPT)
    {
        foreach (var type in SPT)
        {
            if (Random.Range(0, 2) < 1)
            {
                specialHouseLocal[i].sprite = type.HouseSprite;
                specialHouseLocal[i].gameObject.name = type.HouseName;
                i += 4;
                specialHouseLocal[i].sprite = type.AdjacentHouseSprite;
                specialHouseLocal[i].gameObject.name = type.AdjacentHouseName;
                i -= 2;
            }
            else
            {
                specialHouseLocal[i].sprite = type.AdjacentHouseSprite;
                specialHouseLocal[i].gameObject.name = type.AdjacentHouseName;
                i += 4;
                specialHouseLocal[i].sprite = type.HouseSprite;
                specialHouseLocal[i].gameObject.name = type.HouseName;
                i -= 2;
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
    public string HouseName;
    public Sprite HouseSprite;
    public string AdjacentHouseName;
    public Sprite AdjacentHouseSprite;
}
