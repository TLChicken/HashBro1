using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuCtrl : MonoBehaviour {
    public CanvasGroup gameOverCanvasGroup;
    public Animator gameOverCanvasAnimator;

    // Start is called before the first frame update
    void Start() {
        LevelMasterSingleton.LM.gameOverCanvasCtrl = this;
    }

    public void restartLevel() {
        LevelMasterSingleton.LM.restartLevel();
    }


    public void backToMainMenu() {
        LevelMasterSingleton.LM.backToMainMenu();
    }

    public void playShowGameOverScreen() {
        gameOverCanvasAnimator.Play("ShowGameOverCanvas");
    }
}
