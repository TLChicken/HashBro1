using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Keeps track of game logic events such as which item is currently selected in the inventory, and contains the logic of
//what happens when another slot is clicked.
public class LogicEventController : MonoBehaviour {

    [HideInInspector]
    public UI_InventoryManager invMgr;

    [HideInInspector]
    public UI_HashTableManager htMgr;

    private UI_Slot selectedInvSlot;

    private bool inHTRange;


    // Start is called before the first frame update
    void Start() {
        invMgr = LevelMasterSingleton.LM.invMgr;
        htMgr = LevelMasterSingleton.LM.htMgr;

        //Sets this logic controller game object as the controller of all the inventory and ht slots
        foreach (InvItemSlotController slot in invMgr.inventorySlotsList) {
            slot.logicMgr = this;
        }

        foreach (HTSlotController slot in htMgr.preHTSlotsList) {
            slot.logicMgr = this;
        }

        inHTRange = false;
    }

    // Update is called once per frame
    void Update() {
        if (inHTRange) {
            //In HT Range Effects
        }
    }

    // Called by item slots after they are clicked
    //      Selects an item and deselects the previously selected item
    //      Updates the current selectedInvSlot
    public void selectItem(UI_Slot selectThisSlot) {
        //If nothing was selected and the slot being clicked is not empty, then select it
        if (selectedInvSlot == null && !selectThisSlot.isEmpty()) {
            selectThisSlot.selectThisItemSlot();
            selectedInvSlot = selectThisSlot;
        } else if (selectedInvSlot != null) {

            selectThisSlot.onSelectWhenSthElseAlrSelected(selectedInvSlot);


        }


        //Every time after the action caused by selecting an item finishes, check if HT is correct
        LevelMasterSingleton.LM.checkAnswersNow();

    }

    public void forceSelectItem(UI_Slot selectThisSlot) {
        selectThisSlot.selectThisItemSlot();
        if (selectedInvSlot != null) {
            selectedInvSlot.deselectThisItemSlot();
        }
        selectedInvSlot = selectThisSlot;
    }

    public void deselectItem() {
        if (selectedInvSlot != null) {
            selectedInvSlot.deselectThisItemSlot();
            selectedInvSlot = null;
        }

    }


    //Called when HB walks into range of HT
    public void HBGoInHTRange() {
        inHTRange = true;
        htMgr.unblockSlots();
        //TODO: Play sound and activate graphic that shows can interact with HT
    }

    //Called when HB walks out of range of HT
    public void HBOutOfHTRange() {
        inHTRange = false;
        htMgr.blockSlots();
        deselectItem();
    }

    //Checks if inventory is full
    // Return true if inventory is full and false if it isn't
    public bool isInventoryFull() {
        InvItemSlotController nextEmptySlot = invMgr.findNextEmptySlot();

        return nextEmptySlot == null;
    }

}
