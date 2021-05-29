using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerScript : MonoBehaviour
{

    public Grid mainGrid;
    public Tilemap TM_FixedCollider;
    public Tilemap TM_Walkable;
    public GameObject wallMainPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        //Create 3D Objects from tilemaps
        Tilemap currTilemap = TM_FixedCollider;

        Vector3 tmOrigin = currTilemap.origin;
        Vector3 tmSize = currTilemap.size;

        for (int x = Mathf.RoundToInt(tmOrigin.x); x < tmOrigin.x + tmSize.x; x++) {
            for (int y = Mathf.RoundToInt(tmOrigin.y); y < tmOrigin.y + tmSize.y; y++) {
                TileBase currTile = currTilemap.GetTile(mainGrid.WorldToCell(new Vector3(x, 0, -y)));

                if (currTile == null) {
                    continue;
                }

                if (currTile.name == "wallPlaceholder") {
                    Instantiate(wallMainPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool canGo(Vector3 position) {
        
        return !this.checkFixedCollider(position);
    }

    public bool checkFixedCollider(Vector3 position) {
        //Convert coordinates to Integer Values
        TileBase currFixedTile = TM_FixedCollider.GetTile(mainGrid.WorldToCell(position));
        if (currFixedTile == null) {
            Debug.Log("null!!!");
            return false;
        }

        //The name of the tile is the same as the name of the sprite.
        Debug.Log(currFixedTile.name);

        return LevelMasterSingleton.LM.isFixedCollidable(currFixedTile.name);
    }


}
