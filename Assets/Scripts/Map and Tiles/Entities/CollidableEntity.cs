using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pushable Collidable Entity
public class CollidableEntity : Entity {

    public bool solidToHashBro = true;
    //public bool solidToOtherEntities = true;

    //Contains ref to this entity's mover
    //If got no mover then HB cannot enter if this thing is solid
    //If got mover then onHBWantToEnter will ask the mover by calling the same fn there 
    public MovableEntityMover moverComponent;

    [SerializeField]
    private float amtOfHeightToMoveOnGateAction = 1f;


    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {

        if (moverComponent == null) {
            //If collidable entities cannot be moved by HB then hb cannot enter
            return !solidToHashBro;
        } else {
            return moverComponent.onHBWantsToEnter(direction);
        }

    }

    //Moves the entity DOWN if it has a mover component
    public override void onGateBelowMeOpen() {
        base.onGateBelowMeOpen();
        if (moverComponent != null) {
            moverComponent.SwitchSpeed(MovableEntityMover.BoxSpeedTypes.GATE_PUSH);
            moverComponent.MoveSpecificAmt(new Vector3(0f, -amtOfHeightToMoveOnGateAction, 0f));
        }
    }

    //Moves the entity UP if it has a mover component
    public override void onGateBelowMeClose() {
        base.onGateBelowMeClose();
        if (moverComponent != null) {
            moverComponent.SwitchSpeed(MovableEntityMover.BoxSpeedTypes.GATE_PUSH);
            moverComponent.MoveSpecificAmt(new Vector3(0f, amtOfHeightToMoveOnGateAction, 0f));
        }
    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.COLLIDABLE;
    }

}
