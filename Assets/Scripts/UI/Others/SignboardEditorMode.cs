using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class SignboardEditorMode : MonoBehaviour {

    public GameMgrSingleton.MoveDirection signboardDirection;
    [SerializeField]
    private Grid mainGrid;
    [SerializeField]
    private Tilemap TM_Collidable;

    [Space(10f)]
    [Header("Do not modify:")]
    public GameObject signboardContainer; // The pivot to rotate the signboard around
    public GameObject actualSignboard;

    [SerializeField]
    private TileBase invisCollidableInEditor;
    [SerializeField]
    private TileBase invisCollidableInPlayMode;

    private float[] rotationAmt = { 0f, 180f, 270f, 90f };

    [SerializeField]
    private GameMgrSingleton.MoveDirection prevSetDirection = GameMgrSingleton.MoveDirection.UP;


    // Update is called once per frame
    void Update() {
        if (signboardDirection != prevSetDirection) {
            signboardContainer.transform.eulerAngles = new Vector3(0f, rotationAmt[(int)signboardDirection], 0f);
            changeTileUnderSignboard();
            prevSetDirection = signboardDirection;
        }
    }

    public void changeTileUnderSignboard() {
        Vector3 signboardPos = actualSignboard.transform.position;

        TM_Collidable.SetTile(mainGrid.WorldToCell(Vector3Int.RoundToInt(GameMgrSingleton.nearestYZeroIntCoordinate(signboardPos))), invisCollidableInEditor);
        Debug.Log("Changed tile under signboard to InvisCollider.");
    }
}
