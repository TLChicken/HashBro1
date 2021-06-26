using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HashTableManager : MonoBehaviour {
    //HT Slots Parent Object that contains the Grid Layout Group AND contains the HT Slots
    public GameObject gridLayoutObj;

    public HTSlotController[] HTSlotsList;



    // Start is called before the first frame update
    void Start() {
        //Sets the scroll area to not start from the centre for some reason
        gridLayoutObj.transform.position = new Vector3(gridLayoutObj.transform.position.x, 193, gridLayoutObj.transform.position.z);

        //Tells all the HT slots that this script is its HT manager
        foreach (HTSlotController slot in HTSlotsList) {
            slot.htMgr = this;
        }

    }

    // Update is called once per frame
    void Update() {

    }
}
