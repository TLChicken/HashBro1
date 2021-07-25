using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // loads the next scene in the queue

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
