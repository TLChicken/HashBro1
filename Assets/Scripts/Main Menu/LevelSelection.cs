using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {
    public int SelectLevel = -1;

    [Header("The dropdown has Higher Priority")]
    public EnumSceneName.lvlNameEnum selLvlName = EnumSceneName.lvlNameEnum.NONE_SEL;

    [Space(10f)]
    [Header("Do not modify:")]
    public Button lvlSelBtn;

    public Text bonusCollectedTEXT;
    public Text bonusAvailTEXT;
    public Text bestTimeTEXT;

    public void Select() {
        if (SelectLevel == -1 && selLvlName == EnumSceneName.lvlNameEnum.NONE_SEL) {
            SceneManager.LoadScene("Menu");
        } else if (selLvlName == EnumSceneName.lvlNameEnum.NONE_SEL) {
            SceneManager.LoadScene(SelectLevel);
        } else {
            SceneManager.LoadScene(EnumSceneName.nameEnumToStr(selLvlName));
        }
    }

    public void activateLvl() {
        lvlSelBtn.interactable = true;
    }

    public void deactivateLvl() {
        lvlSelBtn.interactable = false;
    }

    public string getCurrLvlName() {
        if (SelectLevel == -1 && selLvlName == EnumSceneName.lvlNameEnum.NONE_SEL) {
            return null;
        } else if (selLvlName == EnumSceneName.lvlNameEnum.NONE_SEL) {
            int buildIndex = SelectLevel;
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            int slashPos = pathToScene.LastIndexOf('/');
            string sceneFilename = pathToScene.Substring(slashPos + 1);
            int dotPos = sceneFilename.LastIndexOf('.');
            string nameOfScene = sceneFilename.Substring(0, dotPos);
            return nameOfScene;
        } else {
            return EnumSceneName.nameEnumToStr(selLvlName);
        }
    }

    public void setBtnStats(int bonusCol, int bonusAvail, string bestTimeStr) {
        if (bonusAvail == 0) {
            bonusCollectedTEXT.text = bonusCol == 0 ? "-" : bonusCol.ToString();
            bonusAvailTEXT.text = "-";
            bestTimeTEXT.text = bestTimeStr;
        } else {
            bonusCollectedTEXT.text = bonusCol.ToString();
            bonusAvailTEXT.text = bonusAvail.ToString();
            bestTimeTEXT.text = bestTimeStr;
        }
    }
}
