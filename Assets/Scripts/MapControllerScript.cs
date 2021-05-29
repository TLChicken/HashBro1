using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerScript : MonoBehaviour
{

    public Grid mainGrid;
    public Tilemap TM_FixedCollider;
    public Tilemap TM_Walkable;
    

    // Start is called before the first frame update
    void Start()
    {
          
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
