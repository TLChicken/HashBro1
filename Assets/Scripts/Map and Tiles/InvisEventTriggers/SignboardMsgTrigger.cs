using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardMsgTrigger : InvisEventTrigger {

    [TextArea(3, 5)]
    public string headerTxtIn;

    [TextArea(5, 10)]
    public string bodyTxtIn;



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
