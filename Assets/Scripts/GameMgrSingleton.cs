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

    //CONSTANTS
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

    private static float closeEnoughAllowance = 0.05f;
    private static float closeEnoughToTileCentreYAllowance = 0.2f;

    [HideInInspector]
    public static string[] fixedCollidableSpriteNames = { "wallPlaceholder", "water1", "waterAnime", "hashTableTilePic", "Door", "InvisCollidableTilePlayMode" };

    [HideInInspector]
    public static List<string> waterTypesSpriteNames = new List<string>() { "water1", "waterAnime" };

    //Need to prepend scene name for each level in front of these strings to find the key for each level
    private static List<string> playerPrefGameProgressKeysBackPart = new List<string>() { "_unlocked", "_collectedBonus", "_shortestTimeTaken" };
    private static List<string> playerPrefSettingsKeys = new List<string>() { "setVol_v1" };



    //INSTANCE VARIABLES
    [Header("Changes Depending on Playthrough:")]
    [SerializeField]
    private float _currVol = -5f;

    public float currVol {
        get {
            return _currVol;
        }
        set {
            _currVol = value;
            PlayerPrefs.SetFloat("setVol_v1", value);
        }
    }


    //Singleton Design - This one real singleton
    void Awake() {
        if (_GM != null && _GM != this) {
            GameObject.Destroy(this.gameObject);
        } else {
            _GM = this;
        }
        DontDestroyOnLoad(this);

        this.currVol = PlayerPrefs.GetFloat("setVol_v1", -5f);

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

    //Checks if sth is close enough to something else on all XYZ
    public static bool isCloseEnoughToXYZ(Vector3 first, Vector3 second, float yAllowance) {
        float firstX = first.x;
        float firstY = first.y;
        float firstZ = first.z;
        float secondX = second.x;
        float secondY = second.y;
        float secondZ = second.z;

        if (Mathf.Abs(secondX - firstX) < closeEnoughAllowance) {
            if (Mathf.Abs(secondZ - firstZ) < closeEnoughAllowance) {
                if (Mathf.Abs(secondY - firstY) < yAllowance) {
                    return true;
                }

            }
        }

        return false;
    }


    //Return the nearest Vector3 coordinate XZ with Y = 0 if the obj is near the centre of a tile (for onTileEnterFully)
    //Returns null if the currPos is not close enough to an integer coor
    public static bool nearEnoughToIntTileCoordinate(Vector3 currPos) {
        float currX = currPos.x;
        float currY = currPos.y;
        float currZ = currPos.z;

        if (currY <= closeEnoughToTileCentreYAllowance && currY >= -closeEnoughToTileCentreYAllowance) {
            float closestX = Mathf.Round(currX);
            float closestZ = Mathf.Round(currZ);

            if (Mathf.Abs(closestX - currX) < closeEnoughAllowance && Mathf.Abs(closestZ - currZ) < closeEnoughAllowance) {
                //The coor is close enough so return that coor
                return true;
            }

        }

        return false;
    }

    //Return nearest int coordinate at ground level unless ground level = false
    public static Vector3 nearestYZeroIntCoordinate(Vector3 currPos, bool groundLevel = true) {
        float currX = currPos.x;
        float currY = currPos.y;
        float currZ = currPos.z;

        float closestX = Mathf.Round(currX);
        float closestY = Mathf.Round(currY);
        float closestZ = Mathf.Round(currZ);

        return new Vector3(closestX, groundLevel ? 0.0f : closestY, closestZ);

    }




    /**
        PLAYER PREFS
    */
    public static void resetProgress() {
        foreach (string currLvlName in EnumSceneName.levelName) {
            foreach (string backPart in playerPrefGameProgressKeysBackPart) {
                string searchThis = currLvlName + backPart;
                PlayerPrefs.DeleteKey(searchThis);
            }

            // //Set the bonus coin and other settings also
            // string searchUnlocked = currLvlName + "_unlocked";
            // string searchColBonus = currLvlName + "_collectedBonus";
            // string searchBestTime = currLvlName + "_shortestTimeTaken";
            // // PlayerPrefs.SetInt(searchUnlocked, 0);
            // // PlayerPrefs.SetInt(searchColBonus, 0);
            // PlayerPrefs.DeleteKey(searchUnlocked);
            // PlayerPrefs.DeleteKey(searchColBonus);
            // PlayerPrefs.DeleteKey(searchBestTime);
        }
    }

    public static void resetPlayerSettings() {
        foreach (string searchInPP in playerPrefSettingsKeys) {
            PlayerPrefs.DeleteKey(searchInPP);
        }
    }


}
