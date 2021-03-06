using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryManager : MonoBehaviour {
    public InvItemSlotController[] inventorySlotsList;



    // Start is called before the first frame update
    void Start() {
        //Tells all the inventory slots that this script is its inventory manager
        foreach (InvItemSlotController slot in inventorySlotsList) {
            slot.invMgr = this;
        }


    }

    // Update is called once per frame
    void Update() {

    }




    // Returns the next inventory slot that is empty
    //      Returns null if all slots are full
    public InvItemSlotController findNextEmptySlot() {
        foreach (InvItemSlotController currSlot in inventorySlotsList) {
            if (currSlot.isEmpty()) {
                return currSlot;
            }
        }

        return null;
    }

    // Adds this new item to the inventory
    //  Inventory finds the next empty slot then adds the item to that slot
    public bool addItemToInventory(HexItem currItem) {
        InvItemSlotController nextEmptySlot = this.findNextEmptySlot();

        Debug.Log("Next Empty Slot: " + nextEmptySlot);

        if (nextEmptySlot == null) {
            return false;
        } else {
            bool addToSlotSuccess = nextEmptySlot.addItem(currItem);

            return addToSlotSuccess;
        }

    }

}
