using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour {

    // [Tooltip("Use this shader to create the material for the color of the puzzle.")]
    // public Shader shaderForMaterial;
    public Color puzzleColor;

    public PuzzleCondition conditionToComplete = PuzzleCondition.ALL_OF;

    [SerializeField]
    [Tooltip("Assign the puzzle pieces to be solved for this puzzle to complete")]
    //This list is only for attaching the puzzle pieces in the Inspector. The actual variable that contains the puzzle pieces is the next one.
    private List<GameObject> currPuzzlePiecesTEMP;
    public List<PuzzlePieceInterface> currPuzzlePieces = new List<PuzzlePieceInterface>();

    [SerializeField]
    [Tooltip("Assign the things that are affected when the puzzle is completed or uncompleted")]
    private List<GameObject> currPuzzleFinishAffectedTEMP;
    public List<PuzzleFinishInterface> currPuzzleFinishAffected = new List<PuzzleFinishInterface>();

    private bool solvedBefore = false;
    private bool currentlySolved = false;


    public enum PuzzleCondition {
        ALL_OF,
        ONE_OF
    }

    // Start is called before the first frame update
    void Start() {
        // Create a material to use for the selected color
        Material colorMaterial = new Material(Shader.Find("Standard"));
        colorMaterial.color = puzzleColor;

        // Process puzzle pieces
        foreach (GameObject currObj in currPuzzlePiecesTEMP) {
            PuzzlePieceInterface currPiece = currObj.GetComponent<PuzzlePieceInterface>();
            if (currPiece == null) {
                Debug.LogWarning(this.name + " ERROR: " + currObj.name + " has no PuzzlePieceInterface attached.");
                continue;
            }
            currPuzzlePieces.Add(currPiece);

            currPiece.changeColor(colorMaterial);
        }

        // Process puzzle finished affected blocks
        foreach (GameObject currObj in currPuzzleFinishAffectedTEMP) {
            PuzzleFinishInterface currFinishAffectObj = currObj.GetComponent<PuzzleFinishInterface>();
            if (currFinishAffectObj == null) {
                Debug.LogWarning(this.name + " ERROR: " + currObj.name + " has no PuzzleFinishInterface attached.");
                continue;
            }

            currPuzzleFinishAffected.Add(currFinishAffectObj);

            currFinishAffectObj.changeColor(colorMaterial);
        }

        updatePuzzleState();
    }

    public void updatePuzzleState() {
        bool solved = checkIfCurrPuzzleSolved();

        //Debug.Log(this.name + " AFTER CHECKING SOLVED Solved: " + solved + " | Unupdated currentlySolved: " + currentlySolved);

        if (solved && !currentlySolved) {
            //Puzzle is now solved
            currentlySolved = solved;
            if (solvedBefore) {
                //Not first time solving this puzzle
                foreach (PuzzleFinishInterface currFin in currPuzzleFinishAffected) {
                    currFin.onPuzzleFurtherComplete();
                }
            } else {
                //First time solving this puzzle
                foreach (PuzzleFinishInterface currFin in currPuzzleFinishAffected) {
                    currFin.onPuzzleFirstComplete();
                }
            }
        } else if (!solved && currentlySolved) {
            //Puzzle is now not solved
            currentlySolved = solved;
            foreach (PuzzleFinishInterface currFin in currPuzzleFinishAffected) {
                currFin.onPuzzleUncomplete();
            }


        } else {
            //Do nothing as no change in solved state

        }
    }


    public bool isPuzzleSolved() {
        return currentlySolved;
    }

    // Checks all the puzzle pieces to see if they are solved
    private bool checkIfCurrPuzzleSolved() {
        bool solved = true;

        if (conditionToComplete == PuzzleCondition.ONE_OF) {
            solved = false;
        }

        foreach (PuzzlePieceInterface currPiece in currPuzzlePieces) {
            //Debug.Log("Curr Piece Correct: " + currPiece.isCurrentlyCorrect());
            if (conditionToComplete == PuzzleCondition.ALL_OF) {
                solved = solved && currPiece.isCurrentlyCorrect();
            } else if (conditionToComplete == PuzzleCondition.ONE_OF) {
                solved = solved || currPiece.isCurrentlyCorrect();
            } else {
                Debug.Log("Unknown Condition");
            }
        }

        return solved;
    }


}
