using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //Returns whether HB can enter the destPos with the entity there
    //If u return true or false it will def determine whether HB can enter or not
    //So u can perform HB X Entity interaction actions here
    public virtual bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {

        //HB Can enter by default if entity is not collidable and has no special properties
        return true;
    }

    //Checks if an entity can enter another entity by being pushed by HB
    //Do not perform any actions that results when the entity moves because whether it moves is not guaranteed even if you return true
    //Return True if can, false if cannot
    public virtual bool onEntityWantsToEnter(Entity theEntityThatIsEntering) {
        return false;
    }

    //For things that can enter water
    public virtual void onEnterGeneralWaterTile(GeneralWaterTile theTile) {

    }

    //Return type of entity to get type without using GetComponent
    public virtual EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.ENTITY;
    }
}
