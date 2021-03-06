using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : CollidableEntity, PuzzleFinishInterface {


    public Renderer insideBlockOfGate;
    public Renderer outsideBlockOfGate;
    public Animator gateAnimator;

    [SerializeField]
    private bool gateOpen = false;

    [Header("Optional")]
    public float percentageDarkerAmt = 20f;

    // [SerializeField]
    // private Shader defaultShader;

    [SerializeField]
    private bool overrideDarkerColor = true;
    public Color darkerColorOverride = new Color(200, 170, 112, 255);

    public GateOptions gateType = GateOptions.NORMAL;

    private bool soundCoroutineRunning = false;

    public enum GateOptions {
        NORMAL, //Gate closes when puzzle incomplete
        BUGGY
    }

    public enum GateAction {
        OPENING,
        CLOSING
    }

    // Start is called before the first frame update
    void Start() {
        soundCoroutineRunning = false;
    }

    void Reset() {
        overrideDarkerColor = true;
        darkerColorOverride = new Color(200, 170, 112, 255);
        percentageDarkerAmt = 20f;
        gateType = GateOptions.NORMAL;
        gateOpen = false;

    }

    public void openGate() {
        gateOpen = true;
        gateAnimator.SetBool("isGateOpen", true);

        gateMoving();

        if (gateType == GateOptions.BUGGY) {
            //Do not move entities
        } else {
            LevelMasterSingleton.LM.GetMapController().gateActionAtPos(this.gameObject.transform.position + new Vector3(0f, 1f, 0f), GateAction.OPENING);
        }
    }

    public void closeGate() {
        gateOpen = false;
        gateAnimator.SetBool("isGateOpen", false);

        gateMoving();

        if (gateType == GateOptions.BUGGY) {
            //Do not move entities
        } else {
            LevelMasterSingleton.LM.GetMapController().gateActionAtPos(this.gameObject.transform.position, GateAction.CLOSING);
        }
    }

    public void gateMoving() {
        if (soundCoroutineRunning || LevelMasterSingleton.LM.lvlMixer.gateSoundPlaying) {
            return;
        }
        IEnumerator soundPlayingCoroutine = playGateMove();
        StartCoroutine(soundPlayingCoroutine);
    }

    public IEnumerator playGateMove() {
        soundCoroutineRunning = true;

        LevelMasterSingleton.LM.lvlMixer.playLvlSound(EnumCollection.LvlSounds.GATE_MOVE);
        LevelMasterSingleton.LM.lvlMixer.gateSoundPlaying = true;
        yield return new WaitForSeconds(0.5f);
        soundCoroutineRunning = false;
        LevelMasterSingleton.LM.lvlMixer.gateSoundPlaying = false;

    }

    // Only runs the first time that the puzzle is completed.
    public void onPuzzleFirstComplete() {
        openGate();
    }

    // Runs when the puzzle gets uncompleted (If Applicable)
    public void onPuzzleUncomplete() {
        closeGate();
    }

    // Runs when the puzzle gets fixed (2nd completion onwards)
    public void onPuzzleFurtherComplete() {
        openGate();
    }

    // Change color to the color of the puzzle
    public void changeColor(Material matWithColor) {
        insideBlockOfGate.material = matWithColor;

        Color darkerColor;

        if (overrideDarkerColor) {
            darkerColor = darkerColorOverride;
        } else {

            Color originalColor = matWithColor.color;
            darkerColor = new Color(makeDarkerColor(originalColor.r), makeDarkerColor(originalColor.g), makeDarkerColor(originalColor.b), originalColor.a);
        }

        Material darkerMaterial = new Material(Shader.Find("Standard"));
        darkerMaterial.color = darkerColor;

        outsideBlockOfGate.material = darkerMaterial;
    }

    private float makeDarkerColor(float colorValue) {
        return (100f - percentageDarkerAmt) * colorValue / 100f;
    }


    // If gate is open then can enter
    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {
        return gateOpen;
    }

    public override bool onEntityWantsToEnter(Entity theEntityThatIsEntering) {
        return gateOpen;
    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.GATE_NORMAL;
    }

}
