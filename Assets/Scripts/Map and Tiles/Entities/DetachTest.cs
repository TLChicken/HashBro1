using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachTest : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        this.gameObject.transform.SetParent(LevelMasterSingleton.LM.allOtherMiscObjsInLvlParent.transform);
    }

    // Update is called once per frame
    void Update() {

    }
}
