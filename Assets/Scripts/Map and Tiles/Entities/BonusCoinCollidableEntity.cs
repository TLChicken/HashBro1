using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoinCollidableEntity : CollidableEntity {

    public float rotatingSpeed = 90f;

    void Update() {
        this.transform.Rotate(new Vector3(0, 1, 0), rotatingSpeed * Time.deltaTime, Space.Self);
    }

    void Reset() {
        solidToHashBro = false;
        rotatingSpeed = 90f;
    }

    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {
        //Collect Bonus Coin Logic
        Debug.Log("Collecting Bonus Coin at: " + this.gameObject.transform.position);
        LevelMasterSingleton.LM.stopDetectingForThisEntity(this);


        LevelMasterSingleton.LM.collectBonusCoin(this);

        this.gameObject.SetActive(false);

        return true;
    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.BONUS_COIN;
    }

}
