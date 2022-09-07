using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    private Transform canvasTransform;
    private TextMeshProUGUI diceText;
    private Rigidbody rb;
    
    private Vector3 startPosition;
    private Vector3 endPosition => new Vector3(startPosition.x,transform.position.y,startPosition.z);
    [SerializeField]
    private int diceValue = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canvasTransform = transform.GetChild(0);
        diceText = canvasTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
        startPosition = transform.position;
        diceText.text = "";
    }
    
    void Update()
    {
        canvasTransform.position = transform.position + (Vector3.up);
        canvasTransform.eulerAngles = Vector3.zero;
        //Debug.Log(Camera.main.transform.name);
        //canvasTransform.LookAt(Camera.main.transform);
        
        if(transform.position.y > .25f)
            transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * 2);
        
        if(rb.velocity.magnitude > .01f)
        {
            diceText.text = "";
        }
        else if(diceText.text == "")
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.up, out hit, .2f);
            diceText.text = hit.collider.transform.name;
            diceValue = int.Parse(diceText.text);
        }
    }
}
