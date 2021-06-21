using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HashFunctionPanel_Mover : MonoBehaviour
{

    public GameObject openedMarker;
    public GameObject closedMarker;
    private RectTransform rt;
    private Vector2 openPosition;

    private Vector2 closedPosition;
    public bool currOpen;
    void Start () {
        rt = GetComponent<RectTransform>();
        if (openedMarker == null) {
            openPosition = rt.transform.localPosition;
        } else {
            openPosition = openedMarker.GetComponent<Transform>().localPosition;
        }
        
        closedPosition = closedMarker.GetComponent<Transform>().localPosition;
        Debug.Log(closedMarker.GetComponent<Transform>().localPosition);
        Debug.Log(closedMarker.GetComponent<Transform>().position);
        
    }
    public void Toggle () {
        if (currOpen)
        {
            rt.anchoredPosition = closedPosition;
            currOpen = false;
        }
        else
        {
            rt.anchoredPosition = openPosition;
            currOpen = true;
        }
        
    }
}
