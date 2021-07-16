using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollidableEntity : CollidableEntity {

    public float heightWhenFloatingInWater = -0.85f;

    public override void onEnterGeneralWaterTile(GeneralWaterTile theTile) {
        base.onEnterGeneralWaterTile(theTile);

        //Make box go downward to -0.85
        if (moverComponent == null) {
            Debug.Log("ERROR: Box has no mover component attached.");
            return;
        }

        moverComponent.GetComponent<BoxMover>().startSinkIntoWaterMovement();

        //Remove box from active interactable entities list
        this.gameObject.transform.SetParent(LevelMasterSingleton.LM.allOtherMiscObjsInLvlParent.transform);
        LevelMasterSingleton.LM.removeEntityFromActiveCheckingList(this);

        //Tell water tile to Make itself walkable by HB and others on tilemap and also in lvlObjRef


    }


}
