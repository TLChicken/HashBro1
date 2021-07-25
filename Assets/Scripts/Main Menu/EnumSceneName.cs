using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumSceneName {
    public static string[] levelName = { "Tutorial", "TLC4", "TLC2", "TLC3" };

    public enum lvlNameEnum {
        TUTORIAL,
        TLC4,
        TLC2,
        TLC3,
        NONE_SEL
    }

    public static string nameEnumToStr(lvlNameEnum currEnum) {
        if (currEnum == lvlNameEnum.NONE_SEL) {
            return null;
        }
        return levelName[(int)currEnum];
    }
}
