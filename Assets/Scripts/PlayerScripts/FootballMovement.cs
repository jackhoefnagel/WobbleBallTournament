using UnityEngine;
using System.Collections;

public class FootballMovement : MonoBehaviour {

    public Rigidbody football;


    public float movementSpeed = 1.0f;

    private Vector3 inputDirection;

    void Start(){
      
    }

    void FixedUpdate(){
        
            inputDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0, Input.GetAxis("Vertical") * movementSpeed);
            football.AddForce(inputDirection, ForceMode.Impulse);

    }
}
