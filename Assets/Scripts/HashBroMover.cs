using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashBroMover : MonoBehaviour
{
    public float movementWidth = 0.01f;
    public bool flyMode = false;
    private CharacterController cC;
    private Vector3 currVelocity;



    // Start is called before the first frame update
    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float currVerticalMovement = 0.0f;

        if (Input.GetKey("space") && flyMode) {
            currVerticalMovement = 0.5f;
        } else if (Input.GetKey("left shift") && flyMode) {
            currVerticalMovement = -0.5f;
        }

        Vector3 moveWhereVector = new Vector3(Input.GetAxis("Horizontal"), currVerticalMovement, Input.GetAxis("Vertical"));
        cC.Move(moveWhereVector * Time.deltaTime * movementWidth);
    }
}
