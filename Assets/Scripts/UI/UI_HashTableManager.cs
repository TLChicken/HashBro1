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

    //The exclaimation mark picture to tell the player that the Hash Table is open,
    // and the audio source to play the corresponding sound
    public Image HTExclaimationMark;
    public AudioSource HTExclaimationMarkAudioSource;
    private bool currHBEnterPlayed = false;

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

        int totalHexItemsInLvl = 0;

        //Add slots to HT if there's the relevant item in the level
        foreach (Transform itemInLevelTrans in LevelMasterSingleton.LM.objsInLvlParent.transform) {
            HexItem currItem = itemInLevelTrans.GetComponent<HexItem>();

            //If it is a HexItem then add it to the Hash Table
            if (currItem != null) {
                totalHexItemsInLvl++;

                HTSlotController currHTSlot = Instantiate(htSlotPrefab);
                //currHTSlot.transform.localScale = new Vector3(1, 1, 1);
                currHTSlot.htMgr = this;
                currHTSlot.logicMgr = LevelMasterSingleton.LM.logicCtrl;
                currHTSlot.qnText.text = currItem.htQuestionStr;

                //Debug.Log(currHTSlot.qnText.text);
                //Make newlines display correctly (Not Working)
                currHTSlot.qnText.text.Replace("\\n", "\n");
                currHTSlot.qnText.text.Replace("\\r", "\n");
                Debug.Log(currHTSlot.qnText.text);

                currHTSlot.correctItem = currItem;
                currHTSlot.transform.SetParent(gridLayoutObj.transform, false);

                activeHTSlotsList.Add(currHTSlot);

            } else {
                //Check if the GO is an unused HT Slot
                HTSlotUnusedCtrl unusedSlotCmp = itemInLevelTrans.GetComponent<HTSlotUnusedCtrl>();

                if (unusedSlotCmp != null) {
                    //Add unused slot to HT
                    HTSlotController currHTSlot = Instantiate(htSlotPrefab);

                    currHTSlot.htMgr = this;
                    currHTSlot.logicMgr = LevelMasterSingleton.LM.logicCtrl;
                    currHTSlot.qnText.text = unusedSlotCmp.htQuestionStr;

                    //Make newlines display correctly (Not Working)
                    currHTSlot.qnText.text.Replace("\\n", "\n");
                    currHTSlot.qnText.text.Replace("\\r", "\n");
                    Debug.Log(currHTSlot.qnText.text);

                    currHTSlot.correctItem = null;
                    currHTSlot.transform.SetParent(gridLayoutObj.transform, false);

                    activeHTSlotsList.Add(currHTSlot);
                }
            }

        }

        HTScrollExtenderPanel.transform.SetParent(gridLayoutObj.transform, false);

        LevelMasterSingleton.LM.GetStatusInfoController().setHexAvailAmt(totalHexItemsInLvl);
    }

    // Update is called once per frame
    void Update() {

    }

    public void blockSlots() {
        HTSlotsBlocker.SetActive(true);
        HTExclaimationMark.gameObject.SetActive(false);
        currHBEnterPlayed = false;
    }

    public void unblockSlots() {
        HTSlotsBlocker.SetActive(false);
        HTExclaimationMark.gameObject.SetActive(true);
        if (!HTExclaimationMarkAudioSource.isPlaying && currHBEnterPlayed == false) {
            HTExclaimationMarkAudioSource.Play();
            currHBEnterPlayed = true;
        }
    }


    //Run when you want to check whether ythe player filled in all the answers correctly
    public bool checkCorrectnessOfHTSlots() {
        foreach (HTSlotController currSlot in activeHTSlotsList) {
            //CorrectItem could now be null because unused HT slots were added
            bool correctness;

            if (currSlot.correctItem == null) {
                correctness = currSlot.currHexItem == null;
            } else {

                correctness = currSlot.correctItem.Equals(currSlot.currHexItem);
            }

            if (!correctness) {
                return false;
            }
        }

        return true;
    }

}
