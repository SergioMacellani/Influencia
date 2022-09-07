using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomHouses))]
public class RandomHousesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RandomHouses randomHouses = (RandomHouses) target;
        if (DrawDefaultInspector())
        {
            if (randomHouses.autoUpdate)
            {
                randomHouses.DeleteMap();
                randomHouses.CreateMap();
                
                if (randomHouses.autoHouses)
                    randomHouses.RandomHousesType();
            }
        }

        if (GUILayout.Button(("Create Map")))
        {
            randomHouses.CreateMap();
        }
        
        if (GUILayout.Button(("Delete Map")))
        {
            randomHouses.DeleteMap();
        }

        if (GUILayout.Button(("Random Houses")))
        {
            randomHouses.RandomHousesType();
        }
        
        if (GUILayout.Button(("Reset Houses")))
        {
            randomHouses.ResetHouses();
        }
    }
}
