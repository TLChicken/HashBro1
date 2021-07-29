using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS AN OR GATE
public class PuzzleIntermediateCtrl : MonoBehaviour, PuzzlePieceInterface, PuzzleFinishInterface {



    private int solvedCount = -1;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }



    // Only runs the first time that the puzzle is completed.
    public void onPuzzleFirstComplete() {
        solvedCount = solvedCount + 1;
    }

    // Runs when the puzzle gets uncompleted (If Applicable)
    public void onPuzzleUncomplete() {
        solvedCount = solvedCount - 1;
    }

    // Runs when the puzzle gets fixed (2nd completion onwards)
    public void onPuzzleFurtherComplete() {
        solvedCount = solvedCount + 1;
    }

    // Check if the puzzle piece is currently solved
    public bool isCurrentlyCorrect() {
        return solvedCount >= 0;
    }

    public void changeColor(Material colorMaterial) {

    }
}
