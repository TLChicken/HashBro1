using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEntityMover : MonoBehaviour {

    public Transform moveToThisSpot;
    public float movementSpeed = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start() {

        moveToThisSpot.parent = LevelMasterSingleton.LM.allOtherMiscObjsInLvlParent.transform;

    }

    // Update is called once per frame
    protected virtual void Update() {

        if (LevelMasterSingleton.LM.paused) {
            //Game is paused so enitity does not move
            return;
        }

        //Continue Moving the entity towards destination
        transform.position = Vector3.MoveTowards(transform.position, moveToThisSpot.position, movementSpeed * Time.deltaTime);

    }
}
