using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Slot class to model the behaviour commonly shared between Inventory slots and HT slots
public class UI_Slot : MonoBehaviour {
    //The GameObject that contains the graphic to show that this item is selected
    public GameObject selectedIndicatorObj;

    //The GameObject that contains the image of the item in the inventory
    public Image itemImg;

    //The GameObject that contains the wood panel holding the full name of the item
    public Image fullNameWoodPanelImg;

    //Reference to the logic event controller, filled in by the its Start() function
    [HideInInspector]
    public LogicEventController logicMgr;

    //The text that is on the item graphic
    public Text graphicTextObj;

    //The text that is below the item that should contain item full names
    public Text fullNameTextObj;

    //The item that this slot contains
    public HexItem currHexItem;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /**
        CODE RELATED to the Item's information in this slot
    */

    //Checks if slot is empty
    public bool isEmpty() {
        return currHexItem == null;
    }


    /**
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the inventory manager to select this item and deselect all other items
    public void onSlotClicked() {
        if (this.isEmpty()) {
            //If empty then don't allow selection
            return;
        }

        logicMgr.selectItem(this);
    }

    public void selectThisItemSlot() {
        this.selectedIndicatorObj.SetActive(true);
    }

    public void deselectThisItemSlot() {
        this.selectedIndicatorObj.SetActive(false);
    }

    /**
        CODE RELATED to the Item Manipulation (Adding and removing items etc)
    */

    // Add item to this slot
    //      Updates the 2 text components and turns on the item slot image
    public bool addItem(HexItem itemToAdd) {
        if (currHexItem != null) {
            Debug.Log("Trying to add a new item into a slot that is already filled. ERROR.");
            return false;
        }


        currHexItem = itemToAdd;

        this.graphicTextObj.text = itemToAdd.itemName;
        this.fullNameTextObj.text = itemToAdd.fullName;

        //Turns on the game object containing the hexagonal item sprite
        this.itemImg.gameObject.SetActive(true);

        if (itemToAdd.fullName != "") {
            //If the item has a full name, then display the wood panel with the full name text
            this.fullNameWoodPanelImg.gameObject.SetActive(true);
        }

        return true;
    }
}
