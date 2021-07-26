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
            SceneManager.LoadScene(EnumSceneName.levelName[SelectLevel]);
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
            return EnumSceneName.levelName[SelectLevel];
        } else {
            return EnumSceneName.nameEnumToStr(selLvlName);
        }
    }

    public void setBtnStats(int bonusCol, int bonusAvail, string bestTimeStr) {
        bonusCollectedTEXT.text = bonusCol.ToString();
        bonusAvailTEXT.text = bonusAvail.ToString();
        bestTimeTEXT.text = bestTimeStr;
    }
}
