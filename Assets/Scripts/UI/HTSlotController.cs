using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTSlotController : UI_Slot {

    [HideInInspector]
    public UI_HashTableManager htMgr;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /**
        Code Related to item's information.
    */

    public override slotType whatType() {
        return slotType.HashTable;
    }


    /**
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the logic manager to select this item
    public new void onSlotClicked() {

        this.logicMgr.selectItem(this);
    }

    public override void onSelectWhenSthElseAlrSelected(UI_Slot previousSel) {
        if (previousSel.whatType() == slotType.Inventory) {
            //If clicked inventory slot then a Hash Table slot, then swap them
            previousSel.swapItems(this);
            logicMgr.deselectItem();

        } else if (previousSel.whatType() == slotType.HashTable) {
            //If clicked Hash Table slot then another Hash Table slot, then swap them too
            previousSel.swapItems(this);
            logicMgr.deselectItem();

        } else {
            Debug.Log("Unknown previous slot type.");
        }


    }



    /**
        CODE RELATED to the Item Manipulation (Adding and removing items etc)
    */

    // Add item to this slot
    //      Updates the 2 text components and turns on the item slot image
    public override bool addItem(HexItem itemToAdd) {
        if (currHexItem != null) {
            Debug.Log("Trying to add a new item into a slot that is already filled. ERROR.");
            return false;
            //Used when swapping
        } else if (!(itemToAdd is HexItem) || itemToAdd == null) {
            Debug.Log("Adding null item into slot. Empties the slot instead.");
            this.removeItem();
            return true;
        }


        currHexItem = itemToAdd;

        this.graphicTextObj.text = itemToAdd.itemName;

        //Turns on the game object containing the hexagonal item sprite
        this.itemImg.gameObject.SetActive(true);

        //Something to set whether item is in the correct HT Slot

        return true;
    }

    //Removes an item from the slot
    //Returns true if the slot is empty in the end, false otherwise
    //(It won't return false currently because the item will always get removed when this is called.)
    public override bool removeItem() {
        if (currHexItem == null) {
            Debug.Log("Trying to remove an item from a slot that is empty.");
            return true;
        }

        //Deselect item
        currHexItem = null;
        //Deactivate item image
        this.itemImg.gameObject.SetActive(false);


        return true;
    }


}
