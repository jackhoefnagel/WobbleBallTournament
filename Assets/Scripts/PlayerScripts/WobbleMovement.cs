using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WobbleMovement : MonoBehaviour {

    public int playerID;

    public Rigidbody baseBody;
    public Rigidbody[] allRigidbodies;
    public Vector3[] allInitRigidbodyPositions;
    public Quaternion[] allInitRigidbodyRotations;
    public GameObject directionObject;

    public float movementSpeed = 1.0f;
    public float maxSpeed = 10.0f;

    private Vector3 inputDirection;
    private Vector3 lastInputDirection;

    private Vector3 inverseDirection;

    private string horInputString = "Horizontal1";
    private string vertInputString = "Vertical1";

    [Header("Player Graphics")]
    public Renderer playerRenderer;
    public Renderer playerDirectionRenderer;
    public Text playerTextIndicator;

    void Start(){
        lastInputDirection = Vector3.zero;

    }

    public void InitPlayer(){

        playerTextIndicator.text = "P"+playerID.ToString();

        allInitRigidbodyPositions = new Vector3[allRigidbodies.Length];
        allInitRigidbodyRotations = new Quaternion[allRigidbodies.Length];

        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allInitRigidbodyPositions[i] = allRigidbodies[i].position;
            allInitRigidbodyRotations[i] = allRigidbodies[i].rotation;
        }

        switch (playerID) {
            case 1 : 
                horInputString = "Horizontal1";
                vertInputString = "Vertical1";
                break;
            case 2 : 
                horInputString = "Horizontal2";
                vertInputString = "Vertical2";
                break;
            case 3 : 
                horInputString = "Horizontal3";
                vertInputString = "Vertical3";
                break;
            case 4 : 
                horInputString = "Horizontal4";
                vertInputString = "Vertical4";
                break;
        }

        InitPlayerColor();
    }

    public void ResetPlayer(){

        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].velocity = Vector3.zero;
            allRigidbodies[i].angularVelocity = Vector3.zero;
            allRigidbodies[i].position = allInitRigidbodyPositions[i];
            allRigidbodies[i].rotation = allInitRigidbodyRotations[i];
        }
        //baseBody.position = Vector3.zero;
    }

    private void InitPlayerColor(){
        int playerIndex = playerID-1;

        Color newPlayerColor = GameManager.instance.playerColors[playerIndex];
        newPlayerColor.a = playerRenderer.material.color.a;
        playerRenderer.material.SetColor("_EmissionColor", newPlayerColor);

        Color newPlayerDirColor = GameManager.instance.playerColors[playerIndex];
        newPlayerDirColor.a = playerRenderer.material.color.a;
        playerDirectionRenderer.material.SetColor("_EmissionColor", newPlayerDirColor);

        playerTextIndicator.color = GameManager.instance.playerColors[playerIndex];
    }

    void FixedUpdate(){

        if(Input.GetAxis(horInputString) == 0 && Input.GetAxis(vertInputString) == 0){            
            inputDirection = lastInputDirection;
        }
        else
        {
            inputDirection = new Vector3(Input.GetAxis(horInputString) * movementSpeed, 0, Input.GetAxis(vertInputString) * movementSpeed);
            lastInputDirection = inputDirection;
            baseBody.AddForce(inputDirection, ForceMode.Impulse);
        }

        if(baseBody.velocity.magnitude > maxSpeed)
        {
            baseBody.velocity = baseBody.velocity.normalized * maxSpeed;
        }

        inverseDirection = -baseBody.velocity;

        Quaternion lookDirectionQ = Quaternion.LookRotation(inverseDirection);
        Quaternion newLookRotation = Quaternion.Slerp(directionObject.transform.rotation, lookDirectionQ, 0.95f);

        directionObject.transform.rotation = newLookRotation;

        //directionObject.transform.LookAt(inverseDirection, Vector3.up);
    }

}
