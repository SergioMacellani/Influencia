using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    private GameObject[] dices;
    [SerializeField]
    private float diceRollForce = 25;
    void Start()
    {
        dices = GameObject.FindGameObjectsWithTag("Dice");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            RollDice();
    }

    void RollDice()
    {
        foreach (var dice in dices)
        {
            Rigidbody rb = dice.GetComponent<Rigidbody>();
        
            if(dice.transform.position.y < 2)
                rb.AddForce(Vector3.up * diceRollForce, ForceMode.Impulse);
        
            rb.AddTorque(Random.Range(0,500),Random.Range(0,500),Random.Range(0,500));
        }
    }
}
