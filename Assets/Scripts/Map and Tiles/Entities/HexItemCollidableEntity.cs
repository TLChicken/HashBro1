using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexItemCollidableEntity : CollidableEntity {

    void Reset() {
        solidToHashBro = false;
    }

    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {

        if (LevelMasterSingleton.LM.getCurrLogicCtrl().isInventoryFull()) {
            //If inventory is full then HB cannot enter
            return false;
        } else {
            //HB can collect HexItems, so it should return true
            return !solidToHashBro;
        }

    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.HEXITEM;
    }

}
