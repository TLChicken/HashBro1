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

    private static float closeEnoughAllowance = 0.02f;

    [HideInInspector]
    public static string[] fixedCollidableSpriteNames = { "wallPlaceholder", "water1", "waterAnime", "hashTableTilePic", "Door" };


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

    //Vector3 creator. This only changes the original coordinate provided
    //to a new coordinate after moving in a certain direction and returns the new one
    public static Vector3 calcNormalDestPos(Vector3 startingPos, MoveDirection dir) {

        int[] coorDiff = null;
        movePosCoorsDict.TryGetValue(dir, out coorDiff);

        float newX = startingPos.x + coorDiff[0];
        float newZ = startingPos.z + coorDiff[1];

        Vector3 destPos = new Vector3(newX, startingPos.y, newZ);
        return destPos;
    }

    //Checks if something is close enough to something else by comparing their Vector3 (Coordinates)
    public static bool isCloseEnoughToXZ(Vector3 first, Vector3 second) {
        float firstX = first.x;
        float firstZ = first.z;
        float secondX = second.x;
        float secondZ = second.z;

        if (Mathf.Abs(secondX - firstX) < closeEnoughAllowance) {
            if (Mathf.Abs(secondZ - firstZ) < closeEnoughAllowance) {
                return true;
            }
        }

        return false;
    }



}
