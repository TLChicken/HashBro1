using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HashTableManager : MonoBehaviour {
    //HT Slots Parent Object that contains the Grid Layout Group AND contains the HT Slots
    public GameObject gridLayoutObj;

    //The panel that prevents interaction with the HT Slots when HB is not beside the HT
    public GameObject HTSlotsBlocker;

    //A panel with the size of 1 HT slot to extend the scroll area if necessary
    //So that the HT slots will not be blocked by the inventory
    public GameObject HTScrollExtenderPanel;

    //The HT Slot Prefab
    public HTSlotController htSlotPrefab;

    //Add pre-existing slots in the HT in to this list if its not
    //supposed to be autoadded.
    public HTSlotController[] preHTSlotsList;

    //List containing all the HTSlots that are active
    public List<HTSlotController> activeHTSlotsList = new List<HTSlotController>();



    // Start is called before the first frame update
    void Start() {
        //Sets the scroll area to not start from the centre for some reason
        gridLayoutObj.transform.position = new Vector3(gridLayoutObj.transform.position.x, -10000, gridLayoutObj.transform.position.z);

        //Tells all the HT slots that this script is its HT manager
        foreach (HTSlotController slot in preHTSlotsList) {
            slot.htMgr = this;
            activeHTSlotsList.Add(slot);
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

                //Debug.Log(currHTSlot.qnText.text);
                //Make newlines display correctly
                currHTSlot.qnText.text.Replace("\\n", "\n");
                currHTSlot.qnText.text.Replace("\\r", "\n");
                Debug.Log(currHTSlot.qnText.text);

                currHTSlot.correctItem = currItem;
                currHTSlot.transform.SetParent(gridLayoutObj.transform, false);

                activeHTSlotsList.Add(currHTSlot);

            }
        }

        HTScrollExtenderPanel.transform.SetParent(gridLayoutObj.transform, false);

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


    //Run when you want to check whether ythe player filled in all the answers correctly
    public bool checkCorrectnessOfHTSlots() {
        foreach (HTSlotController currSlot in activeHTSlotsList) {
            bool correctness = currSlot.correctItem.Equals(currSlot.currHexItem);

            if (!correctness) {
                return false;
            }
        }

        return true;
    }

}
