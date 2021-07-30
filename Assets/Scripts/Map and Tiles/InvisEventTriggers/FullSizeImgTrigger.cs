using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSizeImgTrigger : InvisEventTrigger {
    public Sprite hbEnterImg;
    public bool displayMessageInHFPanel = false;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void onHBEnter() {
        base.onHBEnter();
        UI_OtherInHF otherUIMgr = LevelMasterSingleton.LM.getCurrOtherUIHFMgr();
        otherUIMgr.setFullScreenImg(hbEnterImg);
        otherUIMgr.showFullScreenImg();
        if (displayMessageInHFPanel) {
            LevelMasterSingleton.LM.hashFunctionMgr.changeAndShowMsgForSeconds("Congratulations!!!", "You have travelled the furthest to the right that is possible in the current game.", 20);

        }

    }

    public override void onHBExit() {
        base.onHBExit();
        LevelMasterSingleton.LM.getCurrOtherUIHFMgr().hideFullScreenImg();
        Debug.Log("EXITED from being in a funny place DETECTION!!!");
    }

}
