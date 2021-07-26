using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumSceneName {
    public static string[] levelName = { "Tutorial", "TLC4", "TLC2", "TLC3", "SampleLevel2", "Ben1", "Ben2", "Ben3" };

    public enum lvlNameEnum {
        TUTORIAL,
        TLC4,
        TLC2,
        TLC3,
        SampleLevel2,
        BEN1,
        BEN2,

        BEN3,

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
