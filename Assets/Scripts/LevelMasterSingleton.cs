using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LevelMasterSingleton : MonoBehaviour
{
    // Start is called before the first frame update

    public static LevelMasterSingleton LM;

    private string[] fixedCollidableSpriteNames = {"wallPlaceholder", "water1", "waterAnime"};
    public TileBase[] fixedColliderEventTiles;
    public int levelLength;
    public int levelWidth;

    public GameObject HashBroPlayer;
    //Canvases
    public Canvas UI_levelComplete;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Singleton Design
    void Awake() {
        if (LM != null) {
            GameObject.Destroy(LM);
        } else {
            LM = this;
        }
        //DontDestroyOnLoad(this);
    }

    public void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame() {
        Debug.Log("Game Quitting...");
        Application.Quit();
    }


    /**
        Checks if a tile is a fixed collidable in the level, using its sprite name.
        The sprite name is checked with a list of constants at the top of this script.
        Returns bool indicating whether it is collidable.
    */
    public bool isFixedCollidable(string name) {
        foreach (string colName in fixedCollidableSpriteNames) {
            if (colName.Equals(name)) {
                return true;
            }
        }

        return false;
    }

    /**
        Checks if there is any event to run if HB wants to move to a spot with this fixed collider, BEFORE HB moves there.
        Returns whether HB CANNOT walk into the tile.
    */
    public bool fixedColliderTileEvent(string currTileName) {
        foreach (TileBase fixedColTileWEvent in fixedColliderEventTiles) {
            if (fixedColTileWEvent.name.Equals(currTileName)) {
                //WIP
                return false;
            }
        }

        return false;
    }

    /**
        Retrieves the coordinates of the bottom left corner of the level.
        The LM gameObject should be placed at the bottom left corner of the level to mark the position.
        Returns list of floats containing the coordinates: x at index 0 and y at index 1.
    */
    public float[] getLvlCornerCoors() {
        float btmLeftX = LM.transform.position.x;
        float btmLeftY = LM.transform.position.y;
        float[] returnThis = {btmLeftX, btmLeftY};

        return returnThis;
    }


}
