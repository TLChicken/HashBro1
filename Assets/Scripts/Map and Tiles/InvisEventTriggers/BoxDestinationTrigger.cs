using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestinationTrigger : InvisEventTrigger {

    public BoxCollidableEntity theCorrectBox;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }


    public override void onEntityEnterTileFully(Entity currEntity) {
        BoxCollidableEntity currBox = currEntity.GetComponent<BoxCollidableEntity>();
        if (currBox != null) {
            if (currBox.Equals(theCorrectBox)) {
                theCorrectBox.arriveAtDest();
            }
        }
    }

    public override void onEntityStartExitingTile(Entity currEntity) {
        BoxCollidableEntity currBox = currEntity.GetComponent<BoxCollidableEntity>();
        if (currBox != null) {
            if (currBox.Equals(theCorrectBox)) {
                theCorrectBox.leaveDest();
            }
        }
    }

}
