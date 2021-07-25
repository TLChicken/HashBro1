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

        updateLvlUnlockStatus();
    }

    // Update is called once per frame
    void Update() {

    }


    public void updateLvlUnlockStatus() {
        foreach (LevelSelection currLvlSel in lvlSelBtns) {
            string currLvlName = currLvlSel.getCurrLvlName();
            Debug.Log(currLvlName);

            string searchThis = currLvlName + "_unlocked";

            int unlocked = PlayerPrefs.GetInt(searchThis, 0);

            if (unlocked == 1 || currLvlName == "Tutorial") {
                currLvlSel.activateLvl();
            } else if (unlocked == 0) {
                currLvlSel.deactivateLvl();
            } else {
                Debug.Log("Unlocked state not 0 or 1 but is: " + unlocked);
            }


        }

    }
}
