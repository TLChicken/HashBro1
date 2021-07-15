using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgrSingleton : MonoBehaviour {

    private static GameMgrSingleton _GM;
    //The reference to the GameMgrSingleton that is in use. U can only get it not set it
    public static GameMgrSingleton GM {
        get {
            return _GM;
        }
    }

    public enum MoveDirection {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private static int[][] movePositions = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };

    //To easily get X and Z coordinates to increment or decrement by if something wants to move in a certian direction
    public static Dictionary<MoveDirection, int[]> movePosCoorsDict = new Dictionary<MoveDirection, int[]>()
    {
        { MoveDirection.UP, movePositions[0] },
        { MoveDirection.RIGHT, movePositions[1] },
        { MoveDirection.DOWN, movePositions[2] },
        { MoveDirection.LEFT, movePositions[3] }

    };

    public int testNo = 2;


    //Singleton Design - This one real singleton
    void Awake() {
        if (_GM != null && _GM != this) {
            GameObject.Destroy(this.gameObject);
        } else {
            _GM = this;
        }
        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
