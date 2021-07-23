using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEntity : CollidableEntity, PuzzlePieceInterface {

    [SerializeField]
    private Renderer keyModel;
    [SerializeField]
    private bool keyCollected = false;
    public Animator keyAnimator;

    public float rotatingSpeed = 90f;


    void Reset() {
        keyCollected = false;
        solidToHashBro = false;
        rotatingSpeed = 90f;
    }

    void Update() {
        keyModel.gameObject.transform.Rotate(new Vector3(0, 1, 0), rotatingSpeed * Time.deltaTime, Space.Self);
    }

    public override bool onHBWantsToEnter(GameMgrSingleton.MoveDirection direction) {
        keyCollected = true;
        LevelMasterSingleton.LM.stopDetectingForThisEntity(this);

        keyAnimator.Play("CollectItem");

        return true;
    }

    // Check if the puzzle piece is currently solved
    public bool isCurrentlyCorrect() {
        return keyCollected;
    }

    // To change the color of the obj (if applicable) to the color of the puzzle it is part of when the level starts
    public void changeColor(Material colorMaterial) {
        keyModel.material = colorMaterial;
    }

    public override EnumCollection.EntityTypes GetEntityType() {
        return EnumCollection.EntityTypes.KEY_NORMAL;
    }

}
