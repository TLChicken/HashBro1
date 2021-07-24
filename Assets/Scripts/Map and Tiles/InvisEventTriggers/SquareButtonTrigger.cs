using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareButtonTrigger : ButtonEventTrigger {

    public override void onHBEnter() {
        base.onHBEnter();
        this.PressButton();

    }

    public override void onHBExit() {
        base.onHBExit();
        //Do nothing as square buttons remain permanently activated

    }




    public override void onEntityStartToEnterTile(Entity currEntity) {

    }

    public override void onEntityEnterTileFully(Entity currEntity) {
        this.PressButton();
    }

    public override void onEntityStartExitingTile(Entity currEntity) {
        //Do nothing as square buttons remain permanently activated
    }


}
