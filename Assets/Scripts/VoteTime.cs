using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteTime : MonoBehaviour
{
    public int voteUp = 0;
    public int voteDown = 0;
    
    public VoteButtons[] voteButtons;
    public GameController gameController;
    private int votesTotal => voteDown + voteUp;

    private void OnEnable()
    {
        voteUp = 0;
        voteDown = 0;
    }

    public void VoteUp()
    {
        voteUp++;
        VoteEnd();
    }
    
    public void VoteDown()
    {
        voteDown++;
        VoteEnd();
    }

    public void VoteEnd(bool timer = false)
    {
        Debug.Log(votesTotal);
        Debug.Log(voteButtons.Length);
        if (votesTotal >= voteButtons.Length || timer)
        {
            gameObject.SetActive(false);
            gameController.GetInfluencia(voteUp);
            gameController.NextPlayer();
        }
    }
}
