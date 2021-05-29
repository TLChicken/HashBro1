using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapControllerScript : MonoBehaviour
{

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
        return true;
    }

    public bool checkFixedCollider(Vector3Int position) {
        TileBase currFixedTile = TM_FixedCollider.GetTile(position);
        return false;
    }


}
