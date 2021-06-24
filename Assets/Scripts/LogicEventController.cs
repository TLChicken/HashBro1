using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of game logic events such as which item is currently selected in the inventory, and contains the logic of
//what happens when another slot is clicked.
public class LogicEventController : MonoBehaviour {

    [HideInInspector]
    public UI_InventoryManager invMgr = LevelMasterSingleton.LM.invMgr;
    [HideInInspector]
    public UI_HashTableManager htMgr = LevelMasterSingleton.LM.htMgr;

    private UI_Slot selectedInvSlot;


    // Start is called before the first frame update
    void Start() {
        //Sets this logic controller game object as the controller of all the inventory and ht slots
        foreach (InvItemSlotController slot in invMgr.inventorySlotsList) {
            slot.logicMgr = this;
        }

        foreach (HTSlotController slot in htMgr.HTSlotsList) {
            slot.logicMgr = this;
        }

    }

    // Update is called once per frame
    void Update() {

    }

    // Called by item slots after they are clicked
    //      Selects an item and deselects the previously selected item
    //      Updates the current selectedInvSlot
    public void selectItem(UI_Slot selectThisSlot) {
        selectThisSlot.selectThisItemSlot();
        if (selectedInvSlot != null) {
            selectedInvSlot.deselectThisItemSlot();
        }
        selectedInvSlot = selectThisSlot;
    }


}
