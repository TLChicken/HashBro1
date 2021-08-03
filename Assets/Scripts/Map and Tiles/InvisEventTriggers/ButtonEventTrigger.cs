using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventTrigger : InvisEventTrigger, PuzzlePieceInterface {

    public Renderer buttonKnob;

    [SerializeField]
    private bool isActivated;
    public Animator buttonAnimation;

    [SerializeField]
    private float timeTakenForButtonToDeactivate = 0.1f;
    private IEnumerator buttonDeactivateDelayCoroutine;
    private bool coroutineRunning = false;
    private bool soundCoroutineRunning = false;


    public void PressButton() {
        if (coroutineRunning) {
            StopCoroutine(buttonDeactivateDelayCoroutine);
            coroutineRunning = false;
        }

        if (isActivated) {
            return;
        }

        isActivated = true;
        buttonAnimation.SetBool("buttonDepressed", true);

        playButtonPress();

        //Just in case the coroutine runs while the above 2 lines are running?
        if (coroutineRunning) {
            StopCoroutine(buttonDeactivateDelayCoroutine);
            coroutineRunning = false;
        }
    }

    public void UnPressButton() {
        if (coroutineRunning) {
            return;
        } else {
            coroutineRunning = true;
            buttonDeactivateDelayCoroutine = delayThenUnactivateButton(timeTakenForButtonToDeactivate);
            StartCoroutine(buttonDeactivateDelayCoroutine);
        }

    }

    public void playButtonPress() {
        if (soundCoroutineRunning || updateFrameTracker > 20) {
            return;
        }
        IEnumerator soundPlayingCoroutine = buttonPressCoroutine();
        StartCoroutine(soundPlayingCoroutine);
    }

    public IEnumerator buttonPressCoroutine() {
        soundCoroutineRunning = true;
        LevelMasterSingleton.LM.lvlMixer.playLvlSound(EnumCollection.LvlSounds.BUTTON_PRESS);
        yield return new WaitForSeconds(0.5f);
        soundCoroutineRunning = false;

    }

    public IEnumerator delayThenUnactivateButton(float amtSecs) {
        Debug.Log("Delay unpress button coroutine started.");
        yield return new WaitForSeconds(amtSecs);
        isActivated = false;
        buttonAnimation.SetBool("buttonDepressed", false);
        coroutineRunning = false;
    }



    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    public override void onHBEnter() {
        base.onHBEnter();


    }

    public override void onHBExit() {
        base.onHBExit();


    }

    public override void onEntityStartToEnterTile(Entity currEntity) {

    }

    public override void onEntityEnterTileFully(Entity currEntity) {

    }

    public override void onEntityStartExitingTile(Entity currEntity) {

    }

    // Check if the puzzle piece is currently solved
    public bool isCurrentlyCorrect() {
        return isActivated;
    }

    // To change the color of the obj (if applicable) to the color of the puzzle it is part of when the level starts
    public void changeColor(Material colorMaterial) {
        buttonKnob.material.color = colorMaterial.color;
    }

}
