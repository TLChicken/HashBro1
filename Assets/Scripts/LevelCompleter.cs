using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleter : MonoBehaviour, TileBlockInterface {
    // Start is called before the first frame update

    //The gameObject to change the color of after completing the Hash Table. (The top cube)
    public GameObject indicatorGameObj;
    //The 2 available colors of this indicator
    public Material exitClosedMaterial;
    public Material exitOpenMaterial;

    public bool hashTableCompleted;




    void Start() {
        if (hashTableCompleted) {
            indicatorGameObj.GetComponent<Renderer>().material = exitOpenMaterial;
        } else {
            indicatorGameObj.GetComponent<Renderer>().material = exitClosedMaterial;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    /** True = Collidable (When HT not completed yet). */
    public bool isCollidable() {
        return !hashTableCompleted;
    }

    public void openExit() {
        hashTableCompleted = true;
        indicatorGameObj.GetComponent<Renderer>().material = exitOpenMaterial;
    }

    public void closeExit() {
        hashTableCompleted = false;
        indicatorGameObj.GetComponent<Renderer>().material = exitClosedMaterial;
    }

    public void onHBEnter() {

        LevelMasterSingleton.LM.UI_levelComplete.gameObject.SetActive(true);
        Destroy(LevelMasterSingleton.LM.HashBroPlayer.GetComponent<HashBroMover>());
    }

    //Does nothing as HB cant exit this tile
    public void onHBExit() {

    }

}
