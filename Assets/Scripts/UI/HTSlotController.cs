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
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the logic manager to select this item
    public new void onSlotClicked() {

        this.logicMgr.selectItem(this);
    }



}
