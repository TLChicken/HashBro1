using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMasterSingleton : MonoBehaviour
{
    // Start is called before the first frame update

    public static LevelMasterSingleton LM;

    private string[] fixedCollidableSpriteNames = {"wallPlaceholder", "water1"};
    public int levelLength;
    public int levelWidth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Singleton Design
    void Awake() {
        if (LM != null) {
            GameObject.Destroy(LM);
        } else {
            LM = this;
        }
        DontDestroyOnLoad(this);
    }


    public bool isFixedCollidable(string name) {
        foreach (string colName in fixedCollidableSpriteNames) {
            if (colName.Equals(name)) {
                return true;
            }
        }

        return false;
    }

    public float[] getLvlCornerCoors() {
        float btmLeftX = LM.transform.position.x;
        float btmLeftY = LM.transform.position.y;
        float[] returnThis = {btmLeftX, btmLeftY};

        return returnThis;
    }


}
