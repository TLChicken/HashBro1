using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWaterTile : MonoBehaviour, TileBlockInterface {

    private Vector3 tilePositionOnGrid;
    //HB cant enter water
    public void onHBEnter() {

    }


    public void onHBExit() {

    }

    public void setTilePosition(Vector3 pos) {
        tilePositionOnGrid = pos;
    }

    public Vector3 getTilePosition() {
        return tilePositionOnGrid;
    }

    public void onEntityEnterTileFully(Entity currEntity) {
        //Tell the entity that it has entered water
        currEntity.onEnterGeneralWaterTile(this);

    }
}
