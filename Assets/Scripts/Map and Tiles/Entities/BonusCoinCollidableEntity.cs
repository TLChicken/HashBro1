using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoinCollidableEntity : CollidableEntity {

    void Reset() {
        solidToHashBro = false;
    }

    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {
        //Collect Bonus Coin Logic
        Debug.Log("Collecting Bonus Coin at: " + this.gameObject.transform.position);
        LevelMasterSingleton.LM.collectBonusCoin(this);

        this.gameObject.SetActive(false);

        return true;
    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.BONUS_COIN;
    }

}
