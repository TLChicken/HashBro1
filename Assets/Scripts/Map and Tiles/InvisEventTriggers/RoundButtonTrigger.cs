using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundButtonTrigger : ButtonEventTrigger {
    public override void onHBEnter() {
        base.onHBEnter();
        this.PressButton();

    }

    public override void onHBExit() {
        base.onHBExit();
        this.UnPressButton();

    }

    public override void onEntityStartToEnterTile(Entity currEntity) {

    }

    public override void onEntityEnterTileFully(Entity currEntity) {
        this.PressButton();
    }

    public override void onEntityStartExitingTile(Entity currEntity) {
        this.UnPressButton();
    }


}
