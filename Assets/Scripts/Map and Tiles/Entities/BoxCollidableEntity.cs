using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoxCollidableEntity : CollidableEntity, PuzzlePieceInterface {

    [Header("Destination Things")]
    [Tooltip("Remember to drag the Ending Point gameobject to your desired position.")]
    public bool hasDestination = false;
    public string textOnBox = "";
    public string textAtDestination = "";


    [Header("Do not modify:")]
    public BoxDestinationTrigger destInvisTrigger;
    public Text boxTextObj;
    public Text destTextObj;
    public GameObject boxCanvasParent;
    public Image boxDestFloorSquareImg;

    [SerializeField]
    private bool boxAtDest = false;

    [Space(10f)]

    public float heightWhenFloatingInWater = -0.85f;

    [SerializeField]
    private TileBase waterWithBoxTile;


    public void Start() {

        boxTextObj.text = textOnBox;
        destTextObj.text = textAtDestination;

        if (hasDestination) {
            boxCanvasParent.gameObject.SetActive(true);
            destInvisTrigger.gameObject.SetActive(true);
            LevelMasterSingleton.LM.activateInvisEventTrigger(destInvisTrigger);
        }


    }

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

    public bool isCurrentlyCorrect() {
        return isBoxAtDest();
    }

    //What if box starts at destination?
    public bool isBoxAtDest() {

        bool isNearEnough = GameMgrSingleton.isCloseEnoughToXZ(this.gameObject.transform.position, destInvisTrigger.transform.position);
        //Debug.Log(this.gameObject.transform.position + " is " + isNearEnough + " to be near enough to " + destInvisTrigger.transform.position);
        boxAtDest = isNearEnough;
        return isNearEnough;

    }


    public void changeColor(Material colorMaterial) {
        // Color changeToThisColor = colorMaterial.color;
        // boxDestFloorSquareImg.color = changeToThisColor;
    }

}
