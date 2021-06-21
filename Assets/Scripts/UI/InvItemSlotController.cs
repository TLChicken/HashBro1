using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvItemSlotController : MonoBehaviour
{
    //The GameObject that contains the graphic to show that this item is selected
    public GameObject selectedIndicatorObj;

    //Reference to the inventory manager, filled in by the inventory mangaer's Start() function
    [HideInInspector]
    public UI_InventoryManager invMgr;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /**
        CODE FOR Selecting and Deselecting Items
    */

    //Clicking on the item will trigger this function
    //This will call the inventory manager to select this item and deselect all other items
    public void onSlotClicked() {
        invMgr.selectItem(this);
    }

    public void selectThisItemSlot() {
        this.selectedIndicatorObj.SetActive(true);
    }

    public void deselectThisItemSlot() {
        this.selectedIndicatorObj.SetActive(false);
    }

}
