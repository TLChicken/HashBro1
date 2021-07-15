using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public virtual bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {

        //HB Can enter by default if entity is not collidable and has no special properties
        return true;
    }
}
