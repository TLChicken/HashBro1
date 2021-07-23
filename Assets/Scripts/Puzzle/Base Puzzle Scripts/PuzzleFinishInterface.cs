using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PuzzleFinishInterface {


    // Only runs the first time that the puzzle is completed.
    void onPuzzleFirstComplete();

    // Runs when the puzzle gets uncompleted (If Applicable)
    void onPuzzleUncomplete();

    // Runs when the puzzle gets fixed (2nd completion onwards)
    void onPuzzleFurtherComplete();

    // Change color to the color of the puzzle
    void changeColor(Material matWithColor);

}
