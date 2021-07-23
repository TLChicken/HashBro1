using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : CollidableEntity, PuzzleFinishInterface {

    public Renderer insideBlockOfGate;
    public Animator gateAnimator;

    [SerializeField]
    private bool gateOpen = false;

    // Start is called before the first frame update
    void Start() {

    }

    public void openGate() {
        gateOpen = true;
        gateAnimator.SetBool("isGateOpen", true);
    }

    public void closeGate() {
        gateOpen = false;
        gateAnimator.SetBool("isGateOpen", false);
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
