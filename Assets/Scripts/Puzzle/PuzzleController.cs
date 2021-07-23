using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour {


    public Color puzzleColor;
    [SerializeField]
    [Tooltip("Assign the puzzle pieces to be solved for this puzzle to complete")]
    //This list is only for attaching the puzzle pieces in the Inspector. The actual variable that contains the puzzle pieces is the next one.
    private List<GameObject> currPuzzlePiecesTEMP;
    public List<PuzzlePieceInterface> currPuzzlePieces = new List<PuzzlePieceInterface>();

    // Start is called before the first frame update
    void Start() {
        foreach (GameObject currObj in currPuzzlePiecesTEMP) {
            PuzzlePieceInterface currPiece = currObj.GetComponent<PuzzlePieceInterface>();
            if (currPiece == null) {
                continue;
            }
            currPuzzlePieces.Add(currPiece);

            currPiece.changeColor(puzzleColor);
        }
    }




    public bool isCurrPuzzleSolved() {
        bool solved = true;
        foreach (PuzzlePieceInterface currPiece in currPuzzlePieces) {

            solved = solved && currPiece.isCurrentlyCorrect();
        }

        return solved;
    }


}
