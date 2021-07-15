using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEntityMover : MonoBehaviour {

    public Transform moveToThisSpot;
    public float movementSpeed = 4.5f;

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

    //Entry point for HB wanting to enter, will contain stuff to trigger entity moving if needed
    // Return true if HB can enter false if cannot
    public virtual bool onHBWantsToEnter(GameMgrSingleton.MoveDirection dir) {

        //By default, entities will be pushable by HB
        bool moved = MoveOrder(dir);
        return true;
    }

    //This is the entry point to telling the entity to move, so it will check if can move etc
    // Returns true if moved, false if not moved
    public virtual bool MoveOrder(GameMgrSingleton.MoveDirection dir) {
        //Check if the direction its facing can be moved

        return false;
    }

    //This mtd will actually move the entity
    public virtual bool MoveNormal(GameMgrSingleton.MoveDirection dir) {

        float currX = moveToThisSpot.transform.position.x;
        float currY = moveToThisSpot.transform.position.y;
        float currZ = moveToThisSpot.transform.position.z;

        int[] coorDiff = null;
        GameMgrSingleton.movePosCoorsDict.TryGetValue(dir, out coorDiff);

        float newX = currX + coorDiff[0];
        float newZ = currZ + coorDiff[1];

        moveToThisSpot.transform.position = new Vector3(newX, currY, newZ);

        return true;

    }

}
