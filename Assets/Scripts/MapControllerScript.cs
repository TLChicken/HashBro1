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
    public Tilemap TM_NonInteractable;
    public GameObject blocks3DContainer;
    public GameObject wallMainPrefab;
    public GameObject lvlExitPrefab;
    public GameObject hashTableBlockPrefab;

    public GameObject generalWaterPrefab;

    // A 2D array containing the 3D objects instantiated in the level so that we can find them easily with coordinates.
    public GameObject[,] lvlObjRef;


    [SerializeField]
    private TileBase invisCollidableInEditor;
    [SerializeField]
    private TileBase invisCollidableInPlayMode;

    void Start() {
        //Replace all invis collidable tiles with the ones for Play Mode
        TM_FixedCollider.SwapTile(invisCollidableInEditor, invisCollidableInPlayMode);


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
                Vector3 currentCoordinateInFocus = new Vector3(x, 0, y);

                // TileBase currTile = currTilemap.GetTile(mainGrid.WorldToCell(currentCoordinateInFocus));

                // // Debug.Log(new Vector3(x, 0, y));

                // if (currTile == null) {
                //     continue;
                // }

                GameObject currInstantiatedObj = getGameObjFromCollidableTile(currentCoordinateInFocus);


                //Wall Generator
                // if (currTile.name == "wallPlaceholder") {
                //     currInstantiatedObj = Instantiate(wallMainPrefab, currentCoordinateInFocus, Quaternion.identity);
                //     currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                // }

                // if (currTile.name == "exitTile") {
                //     currInstantiatedObj = Instantiate(lvlExitPrefab, currentCoordinateInFocus, Quaternion.identity);
                //     currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                //     LevelMasterSingleton.LM.lvlExitBlockList.Add(currInstantiatedObj.GetComponent<LevelCompleter>());
                //     Debug.Log("Lvl Complete At: " + x + " " + y);
                // }

                // if (currTile.name == "hashTableTilePic") {
                //     currInstantiatedObj = Instantiate(hashTableBlockPrefab, currentCoordinateInFocus, Quaternion.identity);
                //     currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                // }

                // if (GameMgrSingleton.waterTypesSpriteNames.Contains(currTile.name)) {
                //     //Check if the tile is of a water type
                //     currInstantiatedObj = Instantiate(generalWaterPrefab, currentCoordinateInFocus, Quaternion.identity);
                //     currInstantiatedObj.transform.parent = blocks3DContainer.transform;
                //     currInstantiatedObj.GetComponent<GeneralWaterTile>().setTilePosition(currentCoordinateInFocus);
                // }

                if (currInstantiatedObj == null) {
                    continue;
                }

                //ONLY WORKS IF LEVEL BTMLEFT X AND Y ARE AT 0, 0 - PLS FIX
                lvlObjRef[x, y] = currInstantiatedObj;


            }
        }

        fillBordersWithTile();


    }

    // Update is called once per frame
    void Update() {

    }

    private GameObject getGameObjFromCollidableTile(Vector3 currentCoordinateInFocus) {

        TileBase currTile = TM_FixedCollider.GetTile(mainGrid.WorldToCell(currentCoordinateInFocus));

        if (currTile == null) {
            return null;
        }

        GameObject currInstantiatedObj = null;


        //Wall Generator
        if (currTile.name == "wallPlaceholder") {
            currInstantiatedObj = Instantiate(wallMainPrefab, currentCoordinateInFocus, Quaternion.identity);
            currInstantiatedObj.transform.parent = blocks3DContainer.transform;
        }

        if (currTile.name == "exitTile") {
            currInstantiatedObj = Instantiate(lvlExitPrefab, currentCoordinateInFocus, Quaternion.identity);
            currInstantiatedObj.transform.parent = blocks3DContainer.transform;
            LevelMasterSingleton.LM.lvlExitBlockList.Add(currInstantiatedObj.GetComponent<LevelCompleter>());
            Debug.Log("Lvl Complete At: " + currentCoordinateInFocus.x + " " + currentCoordinateInFocus.z);
        }

        if (currTile.name == "hashTableTilePic") {
            currInstantiatedObj = Instantiate(hashTableBlockPrefab, currentCoordinateInFocus, Quaternion.identity);
            currInstantiatedObj.transform.parent = blocks3DContainer.transform;
        }

        if (GameMgrSingleton.waterTypesSpriteNames.Contains(currTile.name)) {
            //Check if the tile is of a water type
            currInstantiatedObj = Instantiate(generalWaterPrefab, currentCoordinateInFocus, Quaternion.identity);
            currInstantiatedObj.transform.parent = blocks3DContainer.transform;
            currInstantiatedObj.GetComponent<GeneralWaterTile>().setTilePosition(currentCoordinateInFocus);
        }

        return currInstantiatedObj;
    }

    /** Fill in the Borders (Non Interactable Tilemap) with the tile - Run at the start of the level. */
    private void fillBordersWithTile() {
        TileBase useToFill = LevelMasterSingleton.LM.borderTile;

        float[] lvlBtmLeft = LevelMasterSingleton.LM.getLvlCornerCoors();
        float left = lvlBtmLeft[0];
        float btm = lvlBtmLeft[1];
        float top = btm + LevelMasterSingleton.LM.levelWidth;
        float right = left + LevelMasterSingleton.LM.levelLength;

        int padding = 15;
        int btmPadding = 3;

        for (float i = left - padding; i <= right + padding; i++) {
            for (float j = btm - btmPadding; j <= top + padding; j++) {

                if (i <= left || i >= right || j <= btm || j >= top) {
                    //Debug.Log(new Vector3(i, 0, j));
                    TM_NonInteractable.SetTile(mainGrid.WorldToCell(new Vector3(i, 0, j)), useToFill);
                }

            }
        }


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

    /** Runs when a movable entity enters a tile fully the first time. */
    public void onEntityEnterTileFully(Vector3 destPosition, Entity currEntity) {
        Debug.Log(this.name + ": onEntityEnterTileFully at position: " + destPosition);

        GameObject objAtDest = lvlObjRef[Mathf.RoundToInt(destPosition.x), Mathf.RoundToInt(destPosition.z)];
        if (objAtDest == null) {
            return;
        }

        TileBlockInterface tbiAtDest = objAtDest.GetComponent<TileBlockInterface>();
        if (tbiAtDest == null) {
            return;
        }

        Debug.Log(this.name + ": onEntityEnterTileFully entity: " + currEntity.name + " entering tile of type: " + tbiAtDest.GetType());

        tbiAtDest.onEntityEnterTileFully(currEntity);

    }

    /** Runs when a movable entity starts to exit a tile. */
    public void onEntityStartExitingTile(Vector3 posOfTileBeingExited, Entity currEntity) {
        Debug.Log(this.name + ": onEntityStartExitingTile at position: " + posOfTileBeingExited);

        GameObject objAtDest = lvlObjRef[Mathf.RoundToInt(posOfTileBeingExited.x), Mathf.RoundToInt(posOfTileBeingExited.z)];
        if (objAtDest == null) {
            return;
        }

        TileBlockInterface tbiAtDest = objAtDest.GetComponent<TileBlockInterface>();
        if (tbiAtDest == null) {
            return;
        }

        Debug.Log(this.name + ": onEntityStartExitingTile entity: " + currEntity.name + " exiting tile of type: " + tbiAtDest.GetType());

        tbiAtDest.onEntityStartExitingTile(currEntity);

    }

    /**
        This function is called by HashBro after the player supplies input telling it to go somewhere.
        The purpose is to check whether HashBro is able to go to that position.
    */
    public bool canGo(Vector3 position) {
        Debug.Log(lvlObjRef[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)]);

        if (position.x == 0 || position.z == 0) {
            return false;
        }

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

    //Change tile in Collidable TileMap
    public void setTileInTMCollidable(Vector3 coorsOfTile, TileBase theNewTile) {
        TM_FixedCollider.SetTile(mainGrid.WorldToCell(coorsOfTile), theNewTile);
        updateLvlObjRefByCoorsWithNewObj(coorsOfTile);

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
        List<Entity> entitiesThatHBWantsToEnter = new List<Entity>();

        foreach (Entity currEnt in entitiesInLvl) {

            int entityX = Mathf.RoundToInt(currEnt.transform.position.x);
            int entityY = Mathf.RoundToInt(currEnt.transform.position.y);
            int entityZ = Mathf.RoundToInt(currEnt.transform.position.z);

            if (destPosition.x == entityX && destPosition.z == entityZ && Mathf.Abs(entityY - destPosition.y) < 0.5) {

                entitiesThatHBWantsToEnter.Add(currEnt);
            }
        }

        //Entity List may be modified by onHBWantsToEnter while going through the list so should only call the fn after checking which entities to call it on
        foreach (Entity currEnt in entitiesThatHBWantsToEnter) {
            Debug.Log("Entity pos being checked: " + currEnt.transform.position);

            if (currEnt.gameObject.activeSelf) {
                //Since there is an entity at the destination position that HB wants to go,
                //we check if HB can enter
                HBCanEnter = HBCanEnter && currEnt.onHBWantsToEnter(direction);
                // Once there is a false somewhere then HB cannot enter
            }
        }

        //True by default if there are no entities at the destination position
        return HBCanEnter;
    }

    /**
        General function for checking if there are entities at a certain position.
        Used to determine if entities can move to that position BEFORE checking if this position has fixed collidable

        IMPORTANT: ONLY WORKS IF THERE IS ONLY 1 ENTITY AT THAT POSITION
    */
    public Entity checkEntityAtPos(Vector3 position) {
        List<Entity> entitiesInLvl = LevelMasterSingleton.LM.getLvlEntities();

        foreach (Entity currEnt in entitiesInLvl) {

            int entityX = Mathf.RoundToInt(currEnt.transform.position.x);
            int entityZ = Mathf.RoundToInt(currEnt.transform.position.z);

            if (position.x == entityX && position.z == entityZ) {
                Debug.Log(this.name + ":  Entity pos being checked: " + currEnt.transform.position);

                if (currEnt.gameObject.activeSelf) {
                    return currEnt;
                }
            }
        }

        return null;
    }


    public void gateActionAtPos(Vector3 position, GateController.GateAction action) {
        // Can use get getEntitiesAtXYZPos in the future
        List<Entity> entitiesInLvl = LevelMasterSingleton.LM.getLvlEntities();

        List<Entity> entitiesToPerformActionOn = new List<Entity>();

        foreach (Entity currEnt in entitiesInLvl) {

            int entityX = Mathf.RoundToInt(currEnt.transform.position.x);
            int entityY = Mathf.RoundToInt(currEnt.transform.position.y);
            int entityZ = Mathf.RoundToInt(currEnt.transform.position.z);

            if (position.x == entityX && position.z == entityZ && Mathf.Abs(entityY - position.y) < 0.5) {

                entitiesToPerformActionOn.Add(currEnt);
            }
        }

        foreach (Entity currEntity in entitiesToPerformActionOn) {
            switch (action) {
                case GateController.GateAction.OPENING:
                    currEntity.onGateBelowMeOpen();
                    break;
                case GateController.GateAction.CLOSING:
                    currEntity.onGateBelowMeClose();
                    break;
            }

        }

        // If HashBro is also there then play HB dead animation
        Vector3 hbAt = LevelMasterSingleton.LM.HashBroPlayer.transform.position;
        bool hbNearGate = GameMgrSingleton.isCloseEnoughToXYZ(position, hbAt, 0.5f);
        if (hbNearGate) {
            LevelMasterSingleton.LM.becomeGameOver();
        }


    }


    //Change the game object in the array that tracks the game objects instantiated by TMCollidable
    public void setGameObjInLvlObjRefByCoors(Vector3 coorsOfTile, GameObject setToThis) {
        int i = Mathf.RoundToInt(coorsOfTile.x);
        int j = Mathf.RoundToInt(coorsOfTile.z);


        lvlObjRef[i, j] = setToThis;
    }

    public void removeGameObjInLvlObjRefByCoors(Vector3 coorsOfTile) {
        setGameObjInLvlObjRefByCoors(coorsOfTile, null);
    }

    public void updateLvlObjRefByCoorsWithNewObj(Vector3 coorsOfTile) {
        GameObject newInstantiatedObj = getGameObjFromCollidableTile(coorsOfTile);
        setGameObjInLvlObjRefByCoors(coorsOfTile, newInstantiatedObj);
    }

}
