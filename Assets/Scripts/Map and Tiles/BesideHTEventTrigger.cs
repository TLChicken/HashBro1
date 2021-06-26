using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BesideHTEventTrigger : InvisEventTrigger {
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


    }

    public override void onHBExit() {
        base.onHBExit();
        Debug.Log("EXITED from being Beside the Hash Table DETECTION!!!");
    }


}
