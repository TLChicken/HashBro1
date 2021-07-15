using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MovableEntityMover {
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override bool MoveOrder(GameMgrSingleton.MoveDirection dir) {

        return base.MoveOrder(dir);

    }


    protected override bool canMoveIntoWater(Vector3 destPos) {
        //Box can go into water
        //Trigger water code that changes the tile at that pos and set the box as inactive in heirarchy
        return true;
    }

}
