using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorScript : MonoBehaviour
{
    public void OpenDoor() {
        gameObject.SetActive(false);
    }

    public void CloseDoor() {
        gameObject.SetActive(true);
    }
}
