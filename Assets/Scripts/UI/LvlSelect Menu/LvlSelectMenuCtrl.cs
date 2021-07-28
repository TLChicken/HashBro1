using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSelectMenuCtrl : MonoBehaviour {

    public GameObject btnsParent;
    public List<LevelSelection> lvlSelBtns;
    // Start is called before the first frame update
    void Start() {
        foreach (Transform currChild in btnsParent.transform) {
            LevelSelection currLvlSel = currChild.gameObject.GetComponent<LevelSelection>();

            if (currLvlSel != null) {
                lvlSelBtns.Add(currLvlSel);
            } else {
                Debug.Log(this.name + ": UNKNOWN obj in lvl select scroll content.");
            }

        }

        reloadSaveData();
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.PageUp)) {
            //Cheat code
            this.unlockAllLvls();
        }

        if (Input.GetKey(KeyCode.PageDown)) {
            //Revert unlock status back to original
            this.updateLvlUnlockStatus();
        }

        if (Input.GetKey(KeyCode.End)) {
            this.resetProgress();
        }

        if (Input.GetKey(KeyCode.Home)) {
            this.reloadSaveData();
        }
    }

    public void reloadSaveData() {
        updateLvlUnlockStatus();
        updateBtnStats();
    }

    public void updateLvlUnlockStatus() {
        foreach (LevelSelection currLvlSel in lvlSelBtns) {
            string currLvlName = currLvlSel.getCurrLvlName();
            Debug.Log(currLvlName);

            string searchThis = currLvlName + "_unlocked";

            int unlocked = PlayerPrefs.GetInt(searchThis, 0);
            Debug.Log("Unlocked: " + unlocked);

            if (unlocked == 1 || currLvlName == "Tutorial") {
                currLvlSel.activateLvl();
            } else if (unlocked == 0) {
                currLvlSel.deactivateLvl();
            } else {
                Debug.Log("Unlocked state not 0 or 1 but is: " + unlocked);
            }


        }

    }

    public void unlockAllLvls() {
        foreach (LevelSelection currLvlSel in lvlSelBtns) {

            currLvlSel.activateLvl();

        }
    }

    public void resetProgress() {
        foreach (string currLvlName in EnumSceneName.levelName) {

            //Set the bonus coin and other settings also
            string searchUnlocked = currLvlName + "_unlocked";
            string searchColBonus = currLvlName + "_collectedBonus";
            string searchBestTime = currLvlName + "_shortestTimeTaken";
            // PlayerPrefs.SetInt(searchUnlocked, 0);
            // PlayerPrefs.SetInt(searchColBonus, 0);
            PlayerPrefs.DeleteKey(searchUnlocked);
            PlayerPrefs.DeleteKey(searchColBonus);
            PlayerPrefs.DeleteKey(searchBestTime);



        }
    }



    //Update the info underneath all buttons
    public void updateBtnStats() {
        foreach (LevelSelection currLvlSel in lvlSelBtns) {
            string currLvlName = currLvlSel.getCurrLvlName();

            string searchBonusCol = currLvlName + "_collectedBonus";
            string searchBonusAvail = currLvlName + "_totalBonus";
            string searchBestTime = currLvlName + "_shortestTimeTaken";

            int bonusCol = PlayerPrefs.GetInt(searchBonusCol, 0);
            int bonusAvail = PlayerPrefs.GetInt(searchBonusAvail, 0);
            int shortestTimeSecStr = PlayerPrefs.GetInt(searchBestTime, -1);

            if (shortestTimeSecStr == -1) {
                currLvlSel.setBtnStats(bonusCol, bonusAvail, "-");
            } else {
                LvlTimer.LvlTimeContainer shortestTimeCtn = new LvlTimer.LvlTimeContainer(shortestTimeSecStr);
                string bestTimeStr = shortestTimeCtn.getTimeStr();

                currLvlSel.setBtnStats(bonusCol, bonusAvail, bestTimeStr);
            }



        }

    }
}
