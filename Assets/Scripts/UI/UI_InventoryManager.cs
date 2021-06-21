using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryManager : MonoBehaviour
{
    public InvItemSlotController[] inventorySlotsList;

    private InvItemSlotController selectedInvSlot;

    // Start is called before the first frame update
    void Start()
    {
        foreach (InvItemSlotController slot in inventorySlotsList) {
            slot.invMgr = this;
            
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void selectItem(InvItemSlotController selectThisSlot) {
        selectThisSlot.selectThisItemSlot();
        if (selectedInvSlot != null) {
            selectedInvSlot.deselectThisItemSlot();
        }
        selectedInvSlot = selectThisSlot;
    }

}
