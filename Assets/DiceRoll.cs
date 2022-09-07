using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceRoll : MonoBehaviour
{
    public Animator[] dices;
    public GameController gm;
    public int Dice1;
    public int Dice2;
    public int Result;

    public void RollDice()
    {
        GetComponentInChildren<Button>().interactable = false;
        
        Dice1 = Random.Range(1, 7);
        Dice2 = Random.Range(1, 7);
        Result = Dice1 + Dice2;

        PlayAnim(dices[0], Dice1);
        PlayAnim(dices[1], Dice2);
        
        gm.PlayerMove();
        StartCoroutine(WaitToEnd());
    }

    private IEnumerator WaitToEnd()
    {
        yield return new WaitForSeconds(1.7f);
        transform.parent.gameObject.SetActive(false);
        GetComponentInChildren<Button>().interactable = true;
        PlayAnim(dices[0], 7);
        PlayAnim(dices[1], 8);
    }

    private void PlayAnim(Animator a, int dice)
    {
        switch (dice)
        {
            case 1:
                a.Play("1Dice");
                break;
            case 2:
                a.Play("2Dice");
                break;
            case 3:
                a.Play("3Dice");
                break;
            case 4:
                a.Play("4Dice");
                break;
            case 5:
                a.Play("5Dice");
                break;
            case 6:
                a.Play("6Dice");
                break;
            case 7:
                a.Play("DiceIdle");
                break;
            case 8:
                a.Play("DiceIdle2");
                break;
        }
    }
}
