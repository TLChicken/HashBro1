using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashBroMover : MonoBehaviour
{
    public float movementWidth = 1.0f;
    public bool flyMode = false;
    public bool creativeMode = false;
    private CharacterController cC;
    public Transform moveToThisSpot;



    // Start is called before the first frame update
    void Start()
    {
        cC = gameObject.GetComponent<CharacterController>();
        moveToThisSpot.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Continue Moving HashBro
        transform.position = Vector3.MoveTowards(transform.position, moveToThisSpot.position, movementWidth * Time.deltaTime);

        if (creativeMode) {
            float currVerticalMovement = 0.0f;

            if (Input.GetKey("space") && flyMode) {
                currVerticalMovement = 0.5f;
            } else if (Input.GetKey("left shift") && flyMode) {
                currVerticalMovement = -0.5f;
            }

            Vector3 moveWhereVector = new Vector3(Input.GetAxis("Horizontal"), currVerticalMovement, Input.GetAxis("Vertical"));
            cC.Move(moveWhereVector * Time.deltaTime * movementWidth);

        } else if (Vector3.Distance(gameObject.transform.position, moveToThisSpot.position) <= 0.1f) {
            //HashBro finished moving, accept input
            if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.05) {
                //Move Left/Right by 1 block

                float moveXBy = Input.GetAxis("Horizontal") > 0 ? 1.0f : -1.0f;

                moveToThisSpot.position = moveToThisSpot.position + new Vector3(moveXBy, 0.0f, 0.0f);
                
            } else if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.05) {
                //Move Up/Down by 1 block
                float moveZBy = Input.GetAxis("Vertical") > 0 ? 1.0f : -1.0f;

                moveToThisSpot.position = moveToThisSpot.position + new Vector3(0.0f, 0.0f, moveZBy);
                
            }


        } else {
            
            return;

            
        }
    }
}
