using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerScript : MonoBehaviour
{

    public Grid mainGrid;
    public Tilemap TM_FixedCollider;
    public Tilemap TM_Walkable;
    public GameObject blocks3DContainer;
    public GameObject wallMainPrefab;
    public GameObject lvlExitPrefab;

    // A 2D array containing the 3D objects instantiated in the level so that we can find them easily with coordinates.
    public GameObject[,] lvlObjRef;
    
    

    // Start is called before the first frame update
    void Start()
    {
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
                }


                lvlObjRef[x, y] = currInstantiatedObj;

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
        This function is called by HashBro after the player supplies input telling it to go somewhere.
        The purpose is to check whether HashBro is able to go to that position.
    */
    public bool canGo(Vector3 position) {
        Debug.Log(lvlObjRef[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)]);
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
            Debug.Log("null!!!");
            return false;
        }

        //The name of the tile is the same as the name of the sprite.
        Debug.Log(currFixedTile.name);

        bool isFixedCollider = LevelMasterSingleton.LM.isFixedCollidable(currFixedTile.name);
        bool checkFixedTileEvent = LevelMasterSingleton.LM.fixedColliderTileEvent(currFixedTile.name);

        return isFixedCollider || checkFixedTileEvent;
    }


}
