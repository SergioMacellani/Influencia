using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    //Detecta todos os objetos que tem a tag "Player" e atribui ao array PlayersInGame
    public bool AutoPlay;
    public PlayerList pList;
    public List<PlayerData> PlayerGameList;
    public PlayerData PlayerNow => PlayerGameList[PlayerSelected];
    [SerializeField] private float PlayerWalkSize = 1.152f;
    [SerializeField] private float PlayerUpSize = 1.425f;
    [Range(0,2)]
    [SerializeField] private float PlayerWalkTime = 1;
    private int PlayerSelected = 0;
    public GameObject PlayerPrefab;
    public Transform PlayerParent;
    private GameObject Player;
    private RaycastHit houseRayPoint = new RaycastHit();
    private bool choseDirection = false;
    private int dice;
    private Quaternion directions;

    public DiceRoll diceRoll;
    public GameObject GreenCard;
    public GameObject GameCardMenu;
    public GameObject FdDCard;
    public GameObject ShopMenu;
    
    void Start()
    {
        PlayerGameList = pList.players;

        int i = 0;
        foreach (var p in PlayerGameList)
        {
            p.charObject = Instantiate(PlayerPrefab, new Vector3(0, 1.425f*i, 0), transform.rotation);
            p.charObject.GetComponent<Renderer>().material = p.charData.material;
            p.charObject.transform.parent = PlayerParent;
            i++;
        }

        Player = PlayerGameList[0].charObject;
        Debug.Log($"Jogador 1");
        PlayerSelection();
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
        Player = PlayerGameList[PlayerSelected].charObject;
        diceRoll.transform.parent.gameObject.SetActive(true);
    }

    public void PlayerMove()
    {
        dice = diceRoll.Result;
        StartCoroutine(PlayerMovement());
    }

    IEnumerator PlayerMovement(bool ignoreFirst = false)
    {
        yield return new WaitForSeconds(2f);
        //Move o jogador para a posição do dado
        HouseScript hs = null;
        HouseScript hsNext = null;
        
        for (int i = 1; i <= dice; i++)
        {
            hs = HouseRaycast();
            //Debug.Log(hs.IsSpecial);
            if ((!hs.IsSpecial && !hs.IsCenter) || ignoreFirst )
            {
                Player.transform.Translate(Vector3.left * PlayerWalkSize);
                hsNext = HouseRaycast();
                OtherPlayerInHouse(hsNext);
            }
            else
            {
                dice -= (i-1);
                directions = hs.HouseDirections;
                choseDirection = true;
                Debug.Log("Escolha o seu caminho.");
                yield break;
            }

            if (i == 1 && hs.PlayerInHouse!=0)
                hs.PlayerInHouse = 0;
            
            ignoreFirst = false;
            yield return new WaitForSeconds(PlayerWalkTime);
        }
        
        hsNext.PlayerInHouse++;
        Debug.Log(hsNext.name);
        if (hsNext.HaveCard)
        {
            switch (hsNext.gameObject.name)
            {
                case "Green":
                    GreenCard.SetActive(true);
                    break;
                case "Blue":
                    GameCardMenu.SetActive(true);
                    break;
                case "Special_Purple":
                    GameCardMenu.SetActive(true);
                    break;
                case "Special_Red":
                    GameCardMenu.SetActive(true);
                    break;
                case "Special_Yellow":
                    ShopMenu.SetActive(true);
                    break;
            }
        }
        else
            NextPlayer();
    }

    private HouseScript HouseRaycast()
    {
        HouseScript hs;
        Vector3 position;
        
        position = Player.transform.position + Vector3.up;

        Physics.Raycast(position, Vector3.down, out houseRayPoint, 10, LayerMask.GetMask("House"));
        Debug.DrawLine(position, houseRayPoint.point, Color.red);
        hs = houseRayPoint.collider.GetComponent<HouseScript>();
        return hs;
    }

    private void OtherPlayerInHouse(HouseScript hs)
    {
        if (hs.PlayerInHouse != 0)
            Player.transform.Translate(Vector3.up * (hs.PlayerInHouse * PlayerUpSize));
        else if (Player.transform.position.y > 0)
            Player.transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
    }

    public void NextPlayer()
    {
        if (PlayerSelected+1 == PlayerGameList.Count)
        {
            PlayerSelected = 0;
            Debug.Log("Fim da Rodada!");
            FdDCard.SetActive(true);
        }
        else
            PlayerSelected++;

        Debug.Log($"Jogador {PlayerSelected+1}");
        
        if(AutoPlay)
            PlayerSelection();
    }

    private void ChoseDirection()
    {
        Vector3 rotation = Vector3.up;

        /*
        if (!AutoPlay)
        {
            if (Input.GetKeyDown(KeyCode.W) && directions.x == 1)
                rotation = new Vector3(0, 90, 0);

            if (Input.GetKeyDown(KeyCode.S) && directions.y == 1)
                rotation = new Vector3(0, 270, 0);

            if (Input.GetKeyDown(KeyCode.D) && directions.z == 1)
                rotation = new Vector3(0, 180, 0);

            if (Input.GetKeyDown(KeyCode.A) && directions.w == 1)
                rotation = Vector3.zero;
        }
        else
        {
            int rnd = Random.Range(0, 4);
            if (rnd == 0 && directions.x == 1)
                rotation = new Vector3(0, 90, 0);

            if (rnd == 1 && directions.y == 1)
                rotation = new Vector3(0, 270, 0);

            if (rnd == 2 && directions.z == 1)
                rotation = new Vector3(0, 180, 0);

            if (rnd == 3 && directions.w == 1)
                rotation = Vector3.zero;
        }*/

        int rnd = Random.Range(0, 4);
        if (rnd == 0 && directions.x == 1)
            rotation = new Vector3(0, 90, 0);

        if (rnd == 1 && directions.y == 1)
            rotation = new Vector3(0, 270, 0);

        if (rnd == 2 && directions.z == 1)
            rotation = new Vector3(0, 180, 0);

        if (rnd == 3 && directions.w == 1)
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
