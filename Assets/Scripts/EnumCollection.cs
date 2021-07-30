using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumCollection {

    public enum EntityActionsOntoTile {
        ON_START_TO_ENTER,
        ON_ENTER_FULLY,
        ON_START_EXITING,
        ON_EXIT_FULLY
    }

    public enum LvlSounds {
        BOX_PUSH,
        BOX_SPLASH,
        BOX_UNMOVABLE,
        COLLECT_HEXITEM,
        COLLECT_BONUS,
        GATE_MOVE,
        BUTTON_PRESS
    }

    public enum UISounds {
        UI_TRAY_SLIDE,
        PLACE_ITEM_INTO_HT,
        CHANGE_SCENE_WOOSH,
        MENU_BUTTON_CLICK
    }

    public enum EntityTypes {
        //Base Things
        ENTITY,
        COLLIDABLE,

        //Actual Entities
        BOX,
        HEXITEM,
        BONUS_COIN,
        KEY_NORMAL,

        //Blocks
        GATE_NORMAL


    }

}
