using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LevelMasterSingleton : MonoBehaviour {
    // Start is called before the first frame update

    public static LevelMasterSingleton LM;

    private string[] fixedCollidableSpriteNames = GameMgrSingleton.fixedCollidableSpriteNames;
    public TileBase[] fixedColliderEventTiles;
    public int levelLength;
    public int levelWidth;

    //The Parent empty gameObject containing all the invisEventTriggers in the level
    public GameObject objsInLvlParent;

    //Do not modify in inspector anymore - Am leaving it inside for debug puurposes
    public InvisEventTrigger[] itemsInLevelList;

    //Entities found in objsInLvlParent
    public List<Entity> entitiesInLevelList;

    //The Parent empty gameObject containing all the other nonTrigger stuff in the level
    public GameObject allOtherMiscObjsInLvlParent;

    public GameObject HashBroPlayer;
    //Canvases
    public Canvas UI_levelComplete;

    public Canvas UI_pauseMenu;

    //Tracks whether game is paused
    public bool paused;

    public MapControllerScript mapController;
    public UI_InventoryManager invMgr;

    public UI_HashTableManager htMgr;

    public LogicEventController logicCtrl;

    [Tooltip("Open the Hash Function Canvas, Drag in the 'Hash Function Panel' GameObject.")]
    public UI_HashFunction hashFunctionMgr;

    [Tooltip("Open the Hash Function Canvas, Drag in the 'Other UI Things Panel' GameObject.")]
    public UI_OtherInHF otherUIHFMgr;

    [Tooltip("Put in the general audio controller.")]
    public LvlSoundEffectsMixer lvlMixer;

    [Tooltip("Drag in the picture (Sprite) containing the Hash Function for this level.")]
    public Sprite hashFunctionImgForThisLvl;

    [Tooltip("Put in the tile to use as the borders for this level.")]
    public TileBase borderTile;


    //Tracks whether HT is completed 
    public bool htCompleted = false;

    //------------------------------------------------------------------------
    [Header("Don't Modify in Inspector")]

    //List of all the level exits
    public List<LevelCompleter> lvlExitBlockList;

    [SerializeField]
    private int _totalAmtBonusInLvl = -1;
    private int totalAmtBonusInLvl {
        get {
            return _totalAmtBonusInLvl;
        }
        set {
            _totalAmtBonusInLvl = _totalAmtBonusInLvl == -1 ? value : _totalAmtBonusInLvl;
        }
    }
    [SerializeField]
    //private int _totalAmtBonusCoinsLeftInLvl = 0;
    private int totalAmtBonusCoinsLeftInLvl = 0;
    //     get {
    //         return _totalAmtBonusCoinsLeftInLvl;
    //     }
    //     set {
    //         _totalAmtBonusCoinsLeftInLvl = value;
    //         totalAmtBonusInLvl = value;
    //     }
    // }
    [SerializeField]
    private int totalBonusCollectedSoFar = 0;

    void Start() {

        updateEventTriggersList();

        //Set image if there is a hf for this level otherwise show plain msgBox
        if (hashFunctionImgForThisLvl != null) {
            hashFunctionMgr.setHFImg(hashFunctionImgForThisLvl);
        } else {
            hashFunctionMgr.changeMessage("", "Place all the items into the Hash Table in the correct positions. Then head to the exit to complete the level.");
            hashFunctionMgr.showMessage();
        }


    }

    // Update is called once per frame
    void Update() {
        int hbX = Mathf.RoundToInt(HashBroPlayer.transform.position.x);
        int hbY = Mathf.RoundToInt(HashBroPlayer.transform.position.z);


        //Check if hb came into contact with any of the InvisEventTrigger inside the level
        foreach (InvisEventTrigger currItem in itemsInLevelList) {
            //Debug.Log("HB POS: " + HashBroPlayer.transform.position + "Item pos being checked: " + currItem.transform.position);
            int itemX = Mathf.RoundToInt(currItem.transform.position.x);
            int itemY = Mathf.RoundToInt(currItem.transform.position.z);

            if (hbX == itemX && hbY == itemY) {
                Debug.Log("HB POS: " + HashBroPlayer.transform.position + "Item pos being checked: " + currItem.transform.position);

                if (currItem.gameObject.activeSelf) {
                    //HB walking into the item
                    currItem.onHBEnter();
                }

                //Stop checking
                return; //Remove if one tile can contain more than 1 item
            }

        }

        //Check for pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (UI_pauseMenu.gameObject.activeInHierarchy) {
                resumeGame();
            } else {
                pauseGame();
            }
        }

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

    /**
        Loads Scene at index 0 if u are at the last scene, otherwise loads the scene at the next build index.
    */
    public void nextLevel() {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalNoOfScene = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(currSceneIndex + 1 >= totalNoOfScene ? 0 : currSceneIndex + 1);
    }

    public void quitGame() {
        Debug.Log("Game Quitting...");
        Application.Quit();
    }

    public void pauseGame() {
        Debug.Log("Pausing Game...");
        paused = true;
        UI_pauseMenu.gameObject.SetActive(true);
    }
    public void resumeGame() {
        Debug.Log("Resuming Game...");
        paused = false;
        UI_pauseMenu.gameObject.SetActive(false);
    }

    public void backToMainMenu() {
        Debug.Log("Quitting to Main Menu...");
        SceneManager.LoadScene(0);
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

                Debug.Log("Fixed Collider Tile Event: " + currTileName);
                if (fixedColTileWEvent.name.Equals("exitTile")) {
                    //Check if HT completed
                    if (htCompleted) {
                        return false;
                    } else {

                        Debug.Log("Display hint in Hash Function box saying that player has to complete the HT first.");
                        hashFunctionMgr.changeAndShowMsgForSeconds("HINT:", "You have to fill up the entire Hash Table with the correct Hex Items in their respective positions before the exit unlocks.", 3.0f);
                        return true;
                    }

                }




            }
        }

        return false;
    }

    public bool nonHBSpecificFixedCollidableTileEvent(string currTileName) {
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
        float[] returnThis = { btmLeftX, btmLeftY };

        return returnThis;
    }


    public UI_InventoryManager getCurrInventory() {
        return invMgr;
    }

    public UI_HashTableManager getCurrHT() {
        return htMgr;
    }

    public LogicEventController getCurrLogicCtrl() {
        return logicCtrl;
    }

    public UI_OtherInHF getCurrOtherUIHFMgr() {
        return otherUIHFMgr;
    }

    //Gets the list of entities under objsInLvlParent
    public List<Entity> getLvlEntities() {
        return entitiesInLevelList;
    }

    public MapControllerScript GetMapController() {
        return mapController;
    }

    //Updates the Invis Event Trigger List and Entities List
    public void updateEventTriggersList() {
        //Rebuild list each time because it's faster and less intense than comparing the objs in objsInLvlParent
        //And seeing if each obj is alr in the lists
        List<InvisEventTrigger> tempList = new List<InvisEventTrigger>();
        entitiesInLevelList = new List<Entity>();

        totalAmtBonusCoinsLeftInLvl = 0;

        foreach (Transform itemInLevelTrans in objsInLvlParent.transform) {
            InvisEventTrigger itemInLevelTrigger = itemInLevelTrans.GetComponent<InvisEventTrigger>();
            if (itemInLevelTrigger != null) {
                //Add to the list
                tempList.Add(itemInLevelTrigger);

            }

            Entity entityOnObj = itemInLevelTrans.GetComponent<Entity>();
            if (entityOnObj != null) {

                //Check if it's bonus coin
                EnumCollection.EntityTypes currEntityType = entityOnObj.GetEntityType();

                if (currEntityType == EnumCollection.EntityTypes.BONUS_COIN) {
                    Debug.Log(currEntityType + " add bonus coin");
                    this.totalAmtBonusCoinsLeftInLvl = this.totalAmtBonusCoinsLeftInLvl + 1;
                }

                entitiesInLevelList.Add(entityOnObj);
            }
        }

        //Only actually sets the value the first time the function is run so if this fn is run again later
        // then the original starting amt of bonus coins wont change
        totalAmtBonusInLvl = totalAmtBonusCoinsLeftInLvl;

        //Transfer the gameObjects with InvisEventTriggers found under ObjsInLvlParent into the itemsInLevelList
        //This is so when HB walks into those coordinates it will trigger the event triggers.
        itemsInLevelList = new InvisEventTrigger[tempList.Count];
        int currIndex = 0;
        foreach (InvisEventTrigger invisEventTrigger in tempList) {
            itemsInLevelList[currIndex] = invisEventTrigger;


            currIndex = currIndex + 1;
        }

    }


    public void checkAnswersNow() {
        bool allCorrect = htMgr.checkCorrectnessOfHTSlots();

        //XOR - True if there is a change in the status of htCompleted
        if (htCompleted ^ allCorrect) {
            foreach (LevelCompleter currExit in lvlExitBlockList) {
                if (allCorrect) {
                    currExit.openExit();
                } else {
                    currExit.closeExit();
                }
            }
        }


        htCompleted = allCorrect;

    }

    /**
        Triggers an entity action on all InvisEventTriggers at a given coordinate.
        The given coordinate has to be the exact coordinate of the tile to trigger the triggers on
    */
    public void onEntitySomethingToInvisEventTrigger(Vector3 coorToTrigger, Entity currEntity, EnumCollection.EntityActionsOntoTile actionToPerform) {
        int posX = Mathf.RoundToInt(coorToTrigger.x);
        int posZ = Mathf.RoundToInt(coorToTrigger.z);

        foreach (InvisEventTrigger currTrigger in itemsInLevelList) {

            int itemX = Mathf.RoundToInt(currTrigger.transform.position.x);
            int itemZ = Mathf.RoundToInt(currTrigger.transform.position.z);

            if (posX.Equals(itemX) && posZ.Equals(itemZ)) {
                Debug.Log(this.name + ": Entity " + currEntity.name + ": Triggering entity action: " + actionToPerform + " for trigger: " + currTrigger.name + " at " + coorToTrigger);

                if (currTrigger.gameObject.activeSelf) {

                    switch (actionToPerform) {
                        case EnumCollection.EntityActionsOntoTile.ON_START_TO_ENTER:
                            currTrigger.onEntityStartToEnterTile(currEntity);
                            break;
                        case EnumCollection.EntityActionsOntoTile.ON_ENTER_FULLY:
                            currTrigger.onEntityEnterTileFully(currEntity);
                            break;
                        case EnumCollection.EntityActionsOntoTile.ON_START_EXITING:
                            currTrigger.onEntityStartExitingTile(currEntity);
                            break;
                    }


                }
            }

        }
    }


    /*
        Remove entiity from the list of entities to check
    */
    private void removeEntityFromActiveCheckingList(Entity toRemove) {
        entitiesInLevelList.Remove(toRemove);
    }

    /**
        Return the gameObject that contains all the entities that are still visible in the level but removed from the interaction checks
    */
    public void stopDetectingForThisEntity(Entity currEntity) {
        currEntity.transform.SetParent(this.allOtherMiscObjsInLvlParent.transform);
        this.removeEntityFromActiveCheckingList(currEntity);
    }

    /**
        Collecting a Bonus Coin
    */
    public void collectBonusCoin(BonusCoinCollidableEntity coin) {
        this.totalBonusCollectedSoFar = this.totalBonusCollectedSoFar + 1;
    }


}
