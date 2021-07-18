using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumCollection {

    public enum EntityActionsOntoTile {
        ON_START_TO_ENTER,
        ON_ENTER_FULLY,
        ON_START_EXITING
    }

    public enum LvlSounds {
        BOX_PUSH,
        BOX_SPLASH,
        BOX_UNMOVABLE,
        COLLECT_HEXITEM,
        COLLECT_BONUS
    }

    public enum UISounds {
        UI_TRAY_SLIDE,
        PLACE_ITEM_INTO_HT,
        CHANGE_SCENE_WOOSH,
        MENU_BUTTON_CLICK
    }

}
