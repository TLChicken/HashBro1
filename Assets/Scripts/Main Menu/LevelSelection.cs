using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public int SelectLevel;
    public void Select () {
        SceneManager.LoadScene(EnumSceneName.levelName[SelectLevel]);
    }

}
