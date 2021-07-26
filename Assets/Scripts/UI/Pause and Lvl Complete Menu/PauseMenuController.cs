using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used so that references to the LMS on the buttons in the pause menu do not break in every level when I do something to the button
public class PauseMenuController : MonoBehaviour {

    public void restartLevel() {
        LevelMasterSingleton.LM.restartLevel();
    }

    public void resumeGame() {
        LevelMasterSingleton.LM.resumeGame();
    }

    public void backToMainMenu() {
        LevelMasterSingleton.LM.backToMainMenu();
    }
}
