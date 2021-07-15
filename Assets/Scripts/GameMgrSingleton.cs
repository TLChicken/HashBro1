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
