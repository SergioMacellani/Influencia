using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteButtons : MonoBehaviour
{
    public VoteTime voteTime;
    public Button buttonUp;
    public Button buttonDown;

    private void Awake()
    {
        voteTime ??= GetComponentInParent<VoteTime>();
    }

    private void OnEnable()
    {
        buttonUp.interactable = true;
        buttonDown.interactable = true;
    }

    public void ButtonVote(int vote)
    {
        if(vote == 1) voteTime.VoteUp();
        else if(vote == -1) voteTime.VoteDown();
        
        buttonUp.interactable = false;
        buttonDown.interactable = false;
    }
}
