using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    //Detecta todos os objetos que tem a tag "Player" e atribui ao array PlayersInGame
    public GameObject[] PlayersInGame;
    [SerializeField] private float PlayerWalkSize = 1.152f;
    private int PlayerSelected = 0;
    private GameObject Player;
    private RaycastHit houseRayPoint = new RaycastHit();
    private bool choseDirection = false;
    private int dice;
    private Quaternion directions;
    
    void Start()
    {
        PlayersInGame = GameObject.FindGameObjectsWithTag("Player");
        Player = PlayersInGame[0];
        Debug.Log($"Jogador 1");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerSelection();
        if(choseDirection)
            ChoseDirection();
    }

    void PlayerSelection()
    {
        //Seleciona o primeiro jogador
        Player = PlayersInGame[PlayerSelected];
        dice = RollDice();
        Debug.Log($"Tirou {dice} no dado.");
        StartCoroutine(PlayerMovement());
    }

    IEnumerator PlayerMovement(bool ignoreFirst = false)
    {
        //Move o jogador para a posição do dado
        for (int i = 1; i <= dice; i++)
        {
            var position = Player.transform.position + Vector3.up;
            Physics.Raycast(position, Vector3.down, out houseRayPoint, 10, LayerMask.GetMask("House"));
            Debug.DrawLine(position, houseRayPoint.point, Color.red);
            HouseScript hs = houseRayPoint.collider.GetComponent<HouseScript>();
            //Debug.Log(hs.IsSpecial);
            if (!hs.IsSpecial || ignoreFirst )
            {
                Player.transform.Translate(Vector3.left * PlayerWalkSize);
            }
            else
            {
                dice -= (i-1);
                directions = hs.HouseDirections;
                choseDirection = true;
                Debug.Log("Escolha o seu caminho.");
                break;
            }
            ignoreFirst = false;
            yield return new WaitForSeconds(1);
        }

        NextPlayer();
    }

    private void NextPlayer()
    {
        if (PlayerSelected+1 == PlayersInGame.Length)
        {
            PlayerSelected = 0;
            Debug.Log("Fim da Rodada!");
        }
        else
            PlayerSelected++;
        
        Debug.Log($"Jogador {PlayerSelected+1}");
    }

    private void ChoseDirection()
    {
        Vector3 rotation = Vector3.up;

        if (Input.GetKeyDown(KeyCode.W) && directions.x == 1)
            rotation = new Vector3(0,90,0);        
        
        if (Input.GetKeyDown(KeyCode.S) && directions.y == 1)
            rotation = new Vector3(0,270,0);          
        
        if (Input.GetKeyDown(KeyCode.D) && directions.z == 1)
            rotation = new Vector3(0,180,0);           
        
        if (Input.GetKeyDown(KeyCode.A) && directions.w == 1)
            rotation = Vector3.zero;

        if (rotation != Vector3.up)
        {
            choseDirection = false;
            Player.transform.eulerAngles = rotation;
            StartCoroutine(PlayerMovement(true));
        }
    }

    //Roda um dado de 6 lados
    public int RollDice()
    {
        return Random.Range(1, 7);
    }
}
