using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxCollidableEntity : CollidableEntity {

    [Header("Destination Things")]
    [Tooltip("Remember to drag the Ending Point gameobject to your desired position.")]
    public bool hasDestination = false;
    public string textOnBox = "";
    public string textAtDestination = "";



    public BoxDestinationTrigger destInvisTrigger;

    private bool boxAtDest = false;

    [Space(10f)]
    [Header("Do not modify:")]
    public float heightWhenFloatingInWater = -0.85f;

    [SerializeField]
    private TileBase waterWithBoxTile;



    public void Reset() {
        hasDestination = false;
        heightWhenFloatingInWater = -0.85f;
        textOnBox = "";
        textAtDestination = "";
        boxAtDest = false;
    }

    public override void onEnterGeneralWaterTile(GeneralWaterTile theTile) {
        base.onEnterGeneralWaterTile(theTile);

        //Make box go downward to -0.85
        if (moverComponent == null) {
            Debug.Log("ERROR: Box has no mover component attached.");
            return;
        }

        moverComponent.GetComponent<BoxMover>().startSinkIntoWaterMovement();

        //Remove box from active interactable entities list
        LevelMasterSingleton.LM.stopDetectingForThisEntity(this);
        // this.gameObject.transform.SetParent(LevelMasterSingleton.LM.allOtherMiscObjsInLvlParent.transform);
        // LevelMasterSingleton.LM.removeEntityFromActiveCheckingList(this);

        //Tell water tile to Make itself walkable by HB and others on tilemap and also in lvlObjRef
        LevelMasterSingleton.LM.GetMapController().setTileInTMCollidable(theTile.getTilePosition(), waterWithBoxTile);

    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.BOX;
    }



    public void arriveAtDest() {
        boxAtDest = true;
    }

    public void leaveDest() {
        boxAtDest = false;
    }

}
