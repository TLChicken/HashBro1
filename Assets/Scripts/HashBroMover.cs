using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashBroMover : MonoBehaviour {
    /** Sets the speed at which HB moves. */
    public float movementWidth = 1.0f;
    /** Allows flight during CREATIVE MODE. */
    public bool flyMode = false;
    /** Creative Mode toggle. */
    public bool creativeMode = false;

    /** References to other components. */
    private CharacterController cC;
    public MapControllerScript mapControllerObj;
    public Transform moveToThisSpot;

    // Start is called before the first frame update
    void Start() {
        cC = gameObject.GetComponent<CharacterController>();
        //Detach the moveToThisSpot Game Object from HB so that it does not also move when we tell HB to move. 
        moveToThisSpot.parent = null;
    }

    // Update is called once per frame
    void Update() {
        if (LevelMasterSingleton.LM.paused) {
            //Game is paused so HB does not move
            return;
        }

        //Continue Moving HashBro towards destination
        transform.position = Vector3.MoveTowards(transform.position, moveToThisSpot.position, movementWidth * Time.deltaTime);

        //Creative Mode (Alternate Non-Grid Based Movement)
        if (creativeMode) {
            float currVerticalMovement = 0.0f;

            if (Input.GetKey("space") && flyMode) {
                currVerticalMovement = 0.5f;
            } else if (Input.GetKey("left shift") && flyMode) {
                currVerticalMovement = -0.5f;
            }

            Vector3 moveWhereVector = new Vector3(Input.GetAxis("Horizontal"), currVerticalMovement, Input.GetAxis("Vertical"));
            cC.Move(moveWhereVector * Time.deltaTime * movementWidth);

            //HASHBRO MOVEMENT SCRIPT
        } else if (Vector3.Distance(gameObject.transform.position, moveToThisSpot.position) <= 0.02f) { //Sensitivity
            //HashBro has almost finished moving, start accepting input again
            if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.05) {
                //Move Left/Right by 1 block

                float moveXBy = Input.GetAxis("Horizontal") > 0 ? 1.0f : -1.0f;

                Vector3 destPosition = moveToThisSpot.position + new Vector3(moveXBy, 0.0f, 0.0f);

                //Tell MapController that HB wants to go there, checks whether HB can go there or not
                if (mapControllerObj.canGo(destPosition)) {

                    GameMgrSingleton.MoveDirection currDirHBMoving = moveXBy > 0
                        ? GameMgrSingleton.MoveDirection.RIGHT
                        : GameMgrSingleton.MoveDirection.LEFT;

                    //If HB can go then check if got entity there etc and perform the entity action if needed
                    if (mapControllerObj.checkEntityBeforeHBEnter(currDirHBMoving)) {
                        moveToThisSpot.position = destPosition;
                        mapControllerObj.onHBEnterTile(destPosition);
                    }

                }

            } else if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.05) {
                //Move Up/Down by 1 block
                float moveZBy = Input.GetAxis("Vertical") > 0 ? 1.0f : -1.0f;

                Vector3 destPosition = moveToThisSpot.position + new Vector3(0.0f, 0.0f, moveZBy);

                //Tell MapController that HB wants to go there, checks whether HB can go there or not
                if (mapControllerObj.canGo(destPosition)) {

                    GameMgrSingleton.MoveDirection currDirHBMoving = moveZBy > 0
                        ? GameMgrSingleton.MoveDirection.UP
                        : GameMgrSingleton.MoveDirection.DOWN;

                    //If HB can go then check if got entity there etc and perform the entity action if needed
                    if (mapControllerObj.checkEntityBeforeHBEnter(currDirHBMoving)) {
                        moveToThisSpot.position = destPosition;
                        mapControllerObj.onHBEnterTile(destPosition);
                    }
                }

            }


        } else {

            return;


        }
    }
}
