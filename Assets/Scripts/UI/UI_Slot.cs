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

    //Enum for identifying the type of the slot
    public enum slotType { General, Inventory, HashTable };

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

    //Checks slot type
    public virtual slotType whatType() {
        return slotType.General;
    }


    /**
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the logic manager to select this item
    public virtual void onSlotClicked() {
        if (this.isEmpty()) {
            //If empty then don't allow selection
            return;
        }

        logicMgr.selectItem(this);
    }

    public void onSelectedSlotClicked() {
        logicMgr.deselectItem();
    }

    //Called when selecting another slot with some slot already selected
    //Default behaviour is to swap the items
    public virtual void onSelectWhenSthElseAlrSelected(UI_Slot previousSel) {
        Debug.Log("Default UI_Slot onSelectWhenSthElseAlrSelected ran.");
        previousSel.swapItems(this);
        logicMgr.deselectItem();

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
    public virtual bool addItem(HexItem itemToAdd) {
        // if (currHexItem != null) {
        //     Debug.Log("Trying to add a new item into a slot that is already filled. ERROR.");
        //     //Used when swapping
        // } else if (!(itemToAdd is HexItem) || itemToAdd == null) {
        //     Debug.Log("Adding null item into slot. Empties the slot instead.");
        //     this.removeItem();
        //     return true;
        // }


        // currHexItem = itemToAdd;

        // this.graphicTextObj.text = itemToAdd.itemName;
        // this.fullNameTextObj.text = itemToAdd.fullName;

        // //Turns on the game object containing the hexagonal item sprite
        // this.itemImg.gameObject.SetActive(true);

        // if (itemToAdd.fullName != "") {
        //     //If the item has a full name, then display the wood panel with the full name text
        //     this.fullNameWoodPanelImg.gameObject.SetActive(true);
        // }

        // return true;
        Debug.Log("Default addItem function ran. Item not added.");
        return false;
    }

    //Removes an item from the slot
    //Returns true if the slot is empty in the end, false otherwise
    //(It won't return false currently because the item will always get removed when this is called.)
    public virtual bool removeItem() {
        // if (currHexItem == null) {
        //     Debug.Log("Trying to remove an item from a slot that is empty.");
        //     return true;
        // }

        // //Deselect item
        // currHexItem = null;
        // //Deactivate item image
        // this.itemImg.gameObject.SetActive(false);
        // //Deactivate full name panel if it was already activated
        // if (this.fullNameWoodPanelImg.gameObject.activeInHierarchy) {
        //     this.fullNameWoodPanelImg.gameObject.SetActive(false);
        // }

        // return true;

        Debug.Log("Default removeItem function ran. Item not removed.");
        return false;
    }

    public HexItem getItemInSlot() {
        return currHexItem;
    }

    // Pass in a HexItem into this function to replace the current item with it, then
    // returns the item that was previously already in the slot
    public HexItem replaceItemWithThis(HexItem replaceWithThis) {
        HexItem returnThis = currHexItem;

        //Even if the slot is empty u can still run removeItem on it
        this.removeItem();
        this.addItem(replaceWithThis);

        return returnThis;
    }

    //Swaps the items in the 2 slots
    public bool swapItems(UI_Slot secondSlot) {
        HexItem secondItem = secondSlot.replaceItemWithThis(this.currHexItem);
        bool successfulness = this.removeItem();
        successfulness = successfulness && this.addItem(secondItem);
        return successfulness;
    }

}
