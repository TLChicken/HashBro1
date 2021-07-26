using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlCompleteMenuController : MonoBehaviour {
    void Start() {
        //this.gameObject.SetActive(false);
    }

    public void restartLevel() {
        LevelMasterSingleton.LM.restartLevel();
    }

    public void nextLevel() {
        LevelMasterSingleton.LM.nextLevel();
    }

    public void backToMainMenu() {
        LevelMasterSingleton.LM.backToMainMenu();
    }
}
