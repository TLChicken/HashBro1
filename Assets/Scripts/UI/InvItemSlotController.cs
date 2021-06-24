using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItemSlotController : UI_Slot {



    [HideInInspector]
    public UI_InventoryManager invMgr;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    /**
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the inventory manager to select this item and deselect all other items
    public new void onSlotClicked() {
        if (this.isEmpty()) {
            //If empty then don't allow selection
            return;
        }

        this.logicMgr.selectItem(this);
    }



    /**
        CODE RELATED to the Item Manipulation (Adding and removing items etc)
    */


}
