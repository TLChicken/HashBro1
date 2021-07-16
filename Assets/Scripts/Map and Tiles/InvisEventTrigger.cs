using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is for invisible triggers placed throughout the level
public abstract class InvisEventTrigger : MonoBehaviour, TileBlockInterface {

    public bool onEnterTriggeredDuringLastFrame;
    public int updateFrameTracker;
    public int onEnterFrameTracker;

    // Start is called before the first frame update
    protected virtual void Start() {
        onEnterTriggeredDuringLastFrame = false;

        updateFrameTracker = 0;
        onEnterFrameTracker = 0;
    }

    // Update is called once per frame
    protected virtual void Update() {
        if (onEnterTriggeredDuringLastFrame) {

            updateFrameTracker = updateFrameTracker + 1;
        }

        if (Mathf.Abs(updateFrameTracker - onEnterFrameTracker) > 5) {
            this.onHBExit();
            Debug.Log("EXITING TRIGGER");
        }

    }

    public virtual void onHBEnter() {
        if (onEnterTriggeredDuringLastFrame) {
            onEnterFrameTracker = onEnterFrameTracker + 1;
        } else {
            onEnterTriggeredDuringLastFrame = true;
        }




    }

    public virtual void onHBExit() {
        onEnterFrameTracker = 0;
        updateFrameTracker = 0;
        onEnterTriggeredDuringLastFrame = false;

    }

    public virtual void onEntityEnterTileFully(Entity currEntity) {

    }

}
