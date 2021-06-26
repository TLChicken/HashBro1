using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HashTableManager : MonoBehaviour {
    //HT Slots Parent Object that contains the Grid Layout Group AND contains the HT Slots
    public GameObject gridLayoutObj;

    //The panel that prevents interaction with the HT Slots when HB is not beside the HT
    public GameObject HTSlotsBlocker;

    //The HT Slot Prefab
    public HTSlotController htSlotPrefab;


    public HTSlotController[] HTSlotsList;



    // Start is called before the first frame update
    void Start() {
        //Sets the scroll area to not start from the centre for some reason
        gridLayoutObj.transform.position = new Vector3(gridLayoutObj.transform.position.x, 193, gridLayoutObj.transform.position.z);

        //Tells all the HT slots that this script is its HT manager
        foreach (HTSlotController slot in HTSlotsList) {
            slot.htMgr = this;
        }

        //Add slots to HT if there's the relevant item in the level
        foreach (Transform itemInLevelTrans in LevelMasterSingleton.LM.objsInLvlParent.transform) {
            HexItem currItem = itemInLevelTrans.GetComponent<HexItem>();

            //If it is a HexItem then add it to the Hash Table
            if (currItem != null) {
                HTSlotController currHTSlot = Instantiate(htSlotPrefab);
                //currHTSlot.transform.localScale = new Vector3(1, 1, 1);
                currHTSlot.htMgr = this;
                currHTSlot.logicMgr = LevelMasterSingleton.LM.logicCtrl;
                currHTSlot.qnText.text = currItem.htQuestionStr;
                currHTSlot.correctItem = currItem;
                currHTSlot.transform.SetParent(gridLayoutObj.transform, false);

            }
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public void blockSlots() {
        HTSlotsBlocker.SetActive(true);
    }

    public void unblockSlots() {
        HTSlotsBlocker.SetActive(false);
    }
}
