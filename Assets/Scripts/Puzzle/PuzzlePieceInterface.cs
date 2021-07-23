using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface PuzzlePieceInterface {

    // Check if the puzzle piece is currently solved
    bool isCurrentlyCorrect();

    // To change the color of the obj (if applicable) to the color of the puzzle it is part of when the level starts
    void changeColor(Color changeToThisColor);


}
