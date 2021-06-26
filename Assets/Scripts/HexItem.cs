using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexItem : MonoBehaviour, TileBlockInterface {
    // Start is called before the first frame update

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

    void Start() {
        itemTextInGame.text = itemName;
        LM = LevelMasterSingleton.LM;
        invMgr = LM.invMgr;

    }

    // Update is called once per frame
    void Update() {

    }


    //Runs when HashBro enters the tile with the item
    public void onHBEnter() {
        bool successfullyAdded = invMgr.addItemToInventory(this);

        Debug.Log("Successfully Added: " + successfullyAdded);

        //If added successfully then remove the item from the world (SetActive(false))
        if (successfullyAdded) {
            this.gameObject.SetActive(false);
        }

    }


}
