using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface PuzzlePieceInterface : PuzzleCommon {

    // Check if the puzzle piece is currently solved
    bool isCurrentlyCorrect();

}
