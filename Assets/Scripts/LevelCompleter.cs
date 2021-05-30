using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    // Start is called before the first frame update

    //The gameObject to change the color of after completing the Hash Table. (The top cube)
    public GameObject indicatorGameObj;
    //The 2 available colors of this indicator
    public Material exitClosedMaterial;
    public Material exitOpenMaterial;

    public bool hashTableCompleted;

    

    void Start()
    {
        if (hashTableCompleted) {
            indicatorGameObj.GetComponent<Renderer>().material = exitOpenMaterial;
        } else {
            indicatorGameObj.GetComponent<Renderer>().material = exitClosedMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
