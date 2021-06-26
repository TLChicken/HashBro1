using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashTableBlock : MonoBehaviour {
    public BesideHTEventTrigger hTEventTriggerPrefab;

    //To spawn the htEventTrigger Objects
    private GameObject objsInLvlParent;

    //Position of the HT
    private float currX;
    private float currY;
    private float currZ;

    private int[,] positions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

    // Start is called before the first frame update
    void Start() {
        objsInLvlParent = LevelMasterSingleton.LM.objsInLvlParent;

        currX = this.transform.position.x;
        currY = this.transform.position.y;
        currZ = this.transform.position.z;

        Debug.Log("start HT Block");

        for (int posNo = 0; posNo < positions.Length; posNo = posNo + 1) {
            int diffX = positions[posNo, 0];
            int diffZ = positions[posNo, 1];


            BesideHTEventTrigger currTrigger = Instantiate(hTEventTriggerPrefab);
            currTrigger.transform.parent = objsInLvlParent.transform;
            Vector3 theNewPosition = new Vector3(currX + diffX, currY, currZ + diffZ);
            currTrigger.transform.position = theNewPosition;
            //Debug.Log(theNewPosition);
        }




    }

    // Update is called once per frame
    void Update() {

    }
}
