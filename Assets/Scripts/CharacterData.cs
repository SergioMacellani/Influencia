using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char Data", menuName = "Influencia/Player/Char Data")]
public class CharacterData : ScriptableObject
{
    public string name;
    public Material material;
    public ProffessionType proffession;
    
    public enum ProffessionType
    {
        Digitais,
        Musicos,
        Outros
    }
}
