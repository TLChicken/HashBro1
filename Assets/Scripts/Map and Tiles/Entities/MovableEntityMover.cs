using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovableEntityMover : MonoBehaviour {

    public bool movableByHB = true;
    public Transform moveToThisSpot;
    public float movementSpeed = 4.5f;

    //Keeps track of which was the last tile that this thing had entered fully into
    private Vector3 lastTileEnteredFullyCoor;

    // Start is called before the first frame update
    protected virtual void Start() {

        moveToThisSpot.parent = LevelMasterSingleton.LM.allOtherMiscObjsInLvlParent.transform;

        //TRIGGER THE ON TILE ENTER FULLY FOR IT's STARTING TILE HERE
        //In case eg a box starts in water
        LevelMasterSingleton.LM.GetMapController().onEntityEnterTileFully(this.gameObject.GetComponent<Entity>());

        lastTileEnteredFullyCoor = this.transform.position;
    }

    // Update is called once per frame
    protected virtual void Update() {

        if (LevelMasterSingleton.LM.paused) {
            //Game is paused so enitity does not move
            return;
        }



        //Continue Moving the entity towards destination
        transform.position = Vector3.MoveTowards(transform.position, moveToThisSpot.position, movementSpeed * Time.deltaTime);

        //Check if the entity is currently directly on top of a tile
        bool closeEnoughToTileCentre = GameMgrSingleton.nearEnoughToIntTileCoordinate(transform.position);
        if (closeEnoughToTileCentre) {
            //Trigger the onEntityEnterFully of that tile
            Vector3 nearestTileCoor = GameMgrSingleton.nearestYZeroIntCoordinate(this.transform.position);

            //Check if this nearest tile is the last tile that was already entered, if new tile then continue
            if (!nearestTileCoor.Equals(lastTileEnteredFullyCoor)) {
                LevelMasterSingleton.LM.GetMapController().onEntityEnterTileFully(this.gameObject.GetComponent<Entity>()); //enter tile fully

                lastTileEnteredFullyCoor = nearestTileCoor;
            }
        }

    }

    //Entry point for HB wanting to enter, will contain stuff to trigger entity moving if needed
    // Return true if HB can enter false if cannot
    public virtual bool onHBWantsToEnter(GameMgrSingleton.MoveDirection dir) {
        if (movableByHB) {

            //By default, entities will be pushable by HB
            bool moved = MoveOrder(dir);
            return moved;
        } else {
            //Not movable by HB so cannot enter
            return false;
        }
    }

    //This is the entry point to telling the entity to move, so it will check if can move etc
    // Returns true if moved, false if not moved
    public virtual bool MoveOrder(GameMgrSingleton.MoveDirection dir) {
        //Check if the direction its facing can be moved
        //Normal movable entities cannot collide with fixed collidables

        Vector3 destPos = calcDestPos(dir);
        MapControllerScript MCS = LevelMasterSingleton.LM.GetMapController();

        //Check if got entity at the destination position and whether can enter - Abstract this out as its own fn soon

        Entity destPosEntity = MCS.checkEntityAtPos(destPos);
        bool entityCanGoIntoDestEntity = false;

        if (destPosEntity != null) {
            //Check if this entity can move into the other entity else return false

            entityCanGoIntoDestEntity = destPosEntity.onEntityWantsToEnter(this.gameObject.GetComponent<Entity>());
            if (entityCanGoIntoDestEntity == false) {
                //Cannot move into the other entity so this entity shouldn't move
                return false;
            }
        }


        //Check if got fixed collidable at destination position

        TileBase collidableTileAtDest = MCS.getFixedCollidableTileAt(destPos);

        bool canMove = false;

        if (collidableTileAtDest == null) {
            //No collidable tile there so can move
            canMove = true;
        } else {
            //The name of the tile is the same as the name of the sprite.
            Debug.Log("Tile entity wants to move to name: " + collidableTileAtDest.name);

            //{ "wallPlaceholder", "water1", "waterAnime", "hashTableTilePic", "Door" };

            if (collidableTileAtDest.name.Equals("wallPlaceholder")) {
                canMove = canMoveIntoWalls(destPos);
            } else if (collidableTileAtDest.name.Equals("waterAnime") || collidableTileAtDest.name.Equals("water1")) {
                canMove = canMoveIntoWater(destPos);
            } else if (collidableTileAtDest.name.Equals("exitTile")) {
                canMove = false;

            } else {
                //If previous checks dont catch the tile name then check here
                canMove = !LevelMasterSingleton.LM.isFixedCollidable(collidableTileAtDest.name);
            }


        }



        //Move the thing if it can move
        return canMove ? MoveNormal(dir) : false;



    }

    //This mtd will actually move the entity
    public virtual bool MoveNormal(GameMgrSingleton.MoveDirection dir) {


        moveToThisSpot.transform.position = calcDestPos(dir);

        return true;

    }

    public virtual Vector3 calcDestPos(GameMgrSingleton.MoveDirection dir) {
        //Get coors from moveToThisSpot instead of the GameObject itself in case the entity is still moving
        //to that spot but is not there yet
        Vector3 currPos = moveToThisSpot.transform.position;
        Vector3 destPos = GameMgrSingleton.calcNormalDestPos(currPos, dir);

        //ABSTRAcTED OUT TO GMS as this could be common operation
        // float currX = moveToThisSpot.transform.position.x;
        // float currY = moveToThisSpot.transform.position.y;
        // float currZ = moveToThisSpot.transform.position.z;

        // int[] coorDiff = null;
        // GameMgrSingleton.movePosCoorsDict.TryGetValue(dir, out coorDiff);

        // float newX = currX + coorDiff[0];
        // float newZ = currZ + coorDiff[1];

        // Vector3 destPos = new Vector3(newX, currY, newZ);
        return destPos;
    }

    protected virtual bool canMoveIntoWalls(Vector3 destPos) {
        //Normal entities cannot move into walls
        return false;
    }

    protected virtual bool canMoveIntoWater(Vector3 destPos) {
        //Normal entities cannot move into water
        return false;
    }

}
