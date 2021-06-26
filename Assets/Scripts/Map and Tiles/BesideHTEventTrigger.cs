using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BesideHTEventTrigger : InvisEventTrigger {

    private LogicEventController logicCtrl;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        logicCtrl = LevelMasterSingleton.LM.getCurrLogicCtrl();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void onHBEnter() {
        base.onHBEnter();
        logicCtrl.HBGoInHTRange();

    }

    public override void onHBExit() {
        base.onHBExit();
        logicCtrl.HBOutOfHTRange();
        Debug.Log("EXITED from being Beside the Hash Table DETECTION!!!");
    }


}
