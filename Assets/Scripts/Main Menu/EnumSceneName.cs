using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumSceneName {
    public static string[] levelName = { "Tutorial", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9", "SampleLevel2" };

    public enum lvlNameEnum {
        TUTORIAL,
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4,
        LEVEL5,
        LEVEL6,
        LEVEL7,
        LEVEL8,
        LEVEL9,
        SampleLevel2,

        //Not in levelname array
        MENU,
        NONE_SEL
    }

    public static string nameEnumToStr(lvlNameEnum currEnum) {
        if (currEnum == lvlNameEnum.NONE_SEL) {
            return null;
        } else if (currEnum == lvlNameEnum.MENU) {
            return "Menu";
        }
        return levelName[(int)currEnum];
    }
}
