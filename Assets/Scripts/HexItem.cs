using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexItem : InvisEventTrigger {
    //Extends InvisEventTrigger because it needs a transform, and this is supposed to be split into 2 classes,
    //One CollectHexItemTrigger which triggers the actual Hex Item

    //The short name of the item to display inside the item graphic itself
    public string itemName;

    //The full name of the item to display in text boxes below the item in the inventory/hash table
    public string fullName;

    //Description when u hover over an item
    public string desc;

    //Prompt in correct HT Slot - Can be a qn or match etc - Eg: item name is O(n^2) and the prompt is Selection Sort
    public string htQuestionStr;


    //Reference to the text gameObject in game to display the itemName on
    public Text itemTextInGame;

    private LevelMasterSingleton LM;
    private UI_InventoryManager invMgr;
    private Animator currHexItemAnimator;

    protected override void Start() {

        itemTextInGame.text = itemName;
        LM = LevelMasterSingleton.LM;
        invMgr = LM.invMgr;

        currHexItemAnimator = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    protected override void Update() {


    }


    //Runs when HashBro enters the tile with the item
    public override void onHBEnter() {
        base.onHBEnter();

        bool successfullyAdded = invMgr.addItemToInventory(this);

        Debug.Log("Successfully Added: " + successfullyAdded);

        //If added successfully then remove the item from the world (SetActive(false))
        if (successfullyAdded) {
            LevelMasterSingleton.LM.stopDetectingForThisInvisEventTrigger(this);
            LevelMasterSingleton.LM.stopDetectingForThisEntity(this.gameObject.GetComponent<Entity>());
            currHexItemAnimator.Play("CollectItem");
            //this.gameObject.SetActive(false);
        }

    }

    //Does nothing when u exit a tile with an item because u already collected it
    public override void onHBExit() {
        base.onHBExit();

    }

}
