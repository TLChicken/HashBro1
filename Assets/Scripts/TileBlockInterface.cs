using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileBlockInterface
{

    /**
        When HB enters some tile, this method will run while HB is entering
    */
    void onHBEnter();
}
