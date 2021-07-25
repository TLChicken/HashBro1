using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardMsgTrigger : InvisEventTrigger {

    [TextArea]
    public string headerTxtIn;

    [TextArea]
    public string bodyTxtIn;

    public GameMgrSingleton.MoveDirection signboardDirection;

    private SignboardController signboardController;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        signboardController = LevelMasterSingleton.LM.getCurrOtherUIHFMgr().getSignboard();
        signboardController.hideSignboard();

    }

    public override void onHBEnter() {
        base.onHBEnter();
        signboardController.setMsgAndShowSignboard(headerTxtIn, bodyTxtIn);

    }

    public override void onHBExit() {
        base.onHBExit();
        signboardController.hideSignboard();

    }

    public override void onEntityStartToEnterTile(Entity currEntity) {

    }

    public override void onEntityEnterTileFully(Entity currEntity) {

    }

    public override void onEntityStartExitingTile(Entity currEntity) {

    }


}
