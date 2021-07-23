using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour {


    public Color puzzleColor;
    public List<GameObject> currPuzzlePieces;

    // Start is called before the first frame update
    void Start() {
        foreach (GameObject currObj in currPuzzlePieces) {
            PuzzlePieceInterface currPiece = currObj.GetComponent<PuzzlePieceInterface>();
            if (currPiece == null) {
                continue;
            }

            currPiece.changeColor(puzzleColor);
        }
    }




    public bool isCurrPuzzleSolved() {
        bool solved = true;
        foreach (GameObject currObj in currPuzzlePieces) {
            PuzzlePieceInterface currPiece = currObj.GetComponent<PuzzlePieceInterface>();
            if (currPiece == null) {
                continue;
            }

            solved = solved && currPiece.isCurrentlyCorrect();
        }

        return solved;
    }


}
