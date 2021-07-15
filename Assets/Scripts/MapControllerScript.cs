using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// A class to define the logic of what happens when HashBro interacts with physical tiles in the level
// Also does other things related to the physical tiles and tilemaps in the level
// Generates 3D objects based on what tiles are on the tilemap
// Also checks whenever HashBro walks into a tile from the fixed collider tilemap.
public class MapControllerScript : MonoBehaviour {

    public Grid mainGrid;
    public Tilemap TM_FixedCollider;
    public Tilemap TM_Walkable;
    public GameObject blocks3DContainer;
    public GameObject wallMainPrefab;
    public GameObject lvlExitPrefab;
    public GameObject hashTableBlockPrefab;

    // A 2D array containing the 3D objects instantiated in the level so that we can find them easily with coordinates.
    public GameObject[,] lvlObjRef;



    void Start() {
        //Initialise Array
        lvlObjRef = new GameObject[LevelMasterSingleton.LM.levelLength, LevelMasterSingleton.LM.levelWidth];

        /**
            This part of the script initialises the 3D gameObjects based on the tilemaps in the level.
        */

        //Create 3D Objects from tilemaps
        Tilemap currTilemap = TM_FixedCollider;

        // Vector3 tmOrigin = currTilemap.origin;
        // Vector3 tmSize = currTilemap.size;
        float[] lvlCornerCoors = LevelMasterSingleton.LM.getLvlCornerCoors();
        int btmLeftX = Mathf.RoundToInt(lvlCornerCoors[0]);
        int btmLeftY = Mathf.RoundToInt(lvlCornerCoors[1]);

        // for (int x = Mathf.RoundToInt(tmOrigin.x); x < tmOrigin.x + tmSize.x; x++) {
        //     for (int y = Mathf.RoundToInt(tmOrigin.y); y < tmOrigin.y + tmSize.y; y++) {
        for (int x = btmLeftX; x < btmLeftX + LevelMasterSingleton.LM.levelLength; x++) {
            for (int y = btmLeftY; y < btmLeftY + LevelMasterSingleton.LM.levelWidth; y++) {
                TileBase currTile = currTilemap.GetTile(mainGrid.WorldToCell(new Vector3(x, 0, y)));

                // Debug.Log(new Vector3(x, 0, y));

                if (currTile == null) {
                    continue;
                }

                GameObject currInstantiatedObj = null;

                //Wall Generator
                if (currTile.name == "wallPlaceholder") {
                    currInstantiatedObj = Instantiate(wallMainPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                }

                if (currTile.name == "exitTile") {
                    currInstantiatedObj = Instantiate(lvlExitPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                    LevelMasterSingleton.LM.lvlExitBlockList.Add(currInstantiatedObj.GetComponent<LevelCompleter>());
                    Debug.Log("Lvl Complete At: " + x + " " + y);
                }

                if (currTile.name == "hashTableTilePic") {
                    currInstantiatedObj = Instantiate(hashTableBlockPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                }


                lvlObjRef[x, y] = currInstantiatedObj;


            }
        }

    }

    // Update is called once per frame
    void Update() {

    }

    /** Runs when HB walks into the tile at position. */
    public void onHBEnterTile(Vector3 destPosition) {
        GameObject objAtDest = lvlObjRef[Mathf.RoundToInt(destPosition.x), Mathf.RoundToInt(destPosition.z)];
        if (objAtDest == null) {
            return;
        }


        TileBlockInterface tbiAtDest = objAtDest.GetComponent<TileBlockInterface>();
        if (tbiAtDest == null) {
            return;
        }
        tbiAtDest.onHBEnter();

        Debug.Log("entered:");
    }

    /**
        This function is called by HashBro after the player supplies input telling it to go somewhere.
        The purpose is to check whether HashBro is able to go to that position.
    */
    public bool canGo(Vector3 position) {
        Debug.Log(lvlObjRef[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)]);
        return !this.checkFixedCollider(position);
    }


    /**
        This function is for checking a fixed collider is present at the location "position",
        and if it is present, whether HashBro CANNOT walk into it. (true for cannot, false for can)
    */
    public bool checkFixedCollider(Vector3 position) {
        //Convert coordinates to Integer Values
        TileBase currFixedTile = TM_FixedCollider.GetTile(mainGrid.WorldToCell(position));
        if (currFixedTile == null) {
            return false;
        }

        //The name of the tile is the same as the name of the sprite.
        Debug.Log(currFixedTile.name);

        bool isFixedCollider = LevelMasterSingleton.LM.isFixedCollidable(currFixedTile.name);
        bool checkFixedTileEvent = LevelMasterSingleton.LM.fixedColliderTileEvent(currFixedTile.name);

        return isFixedCollider || checkFixedTileEvent;
    }

    public TileBase getFixedCollidableTileAt(Vector3 position) {
        return TM_FixedCollider.GetTile(mainGrid.WorldToCell(position));
    }

    // public bool checkObjectCollider(Vector3 position) {
    //     TileBase currentTile = TM_FixedCollider.GetTile(mainGrid.WorldToCell(position));
    //     if (currentTile == null) {
    //         Debug.Log("null");
    //         return false;
    //     }

    //     // if it is a box, must be pushable 
    //     if (currentTile.name == "box2") {
    //         Debug.Log("test");
    //         return true;
    //     }
    //     return false;
    // }

    /**
        This is a general function for checking if there are entities at the position that HB wants to enter
        If have then see if HB can enter
        If HB can enter then perform any actions needed to the entity and also return true
    */
    public bool checkEntityBeforeHBEnter(GameMgrSingleton.MoveDirection direction, Vector3 destPosition) {
        List<Entity> entitiesInLvl = LevelMasterSingleton.LM.getLvlEntities();
        bool HBCanEnter = true;

        foreach (Entity currEnt in entitiesInLvl) {

            int entityX = Mathf.RoundToInt(currEnt.transform.position.x);
            int entityZ = Mathf.RoundToInt(currEnt.transform.position.z);

            if (destPosition.x == entityX && destPosition.z == entityZ) {
                Debug.Log("Entity pos being checked: " + currEnt.transform.position);

                if (currEnt.gameObject.activeSelf) {
                    //Since there is an entity at the destination position that HB wants to go,
                    //we check if HB can enter
                    HBCanEnter = HBCanEnter && currEnt.onHBWantsToEnter(direction);
                    // Once there is a false somewhere then HB cannot enter
                }


            }


        }

        //True by default if there are no entities at the destination position
        return HBCanEnter;
    }

}
