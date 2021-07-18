using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSoundEffectsMixer : MonoBehaviour {

    [Tooltip("Place the reference to the Audio Source - The one that plays the bkg music")]
    public AudioSource audioSource;

    [Tooltip("Sound for the UI trays sliding in and out")]
    public AudioClip openingClosingInv;

    public AudioClip putItemIntoHT;

    public AudioClip changeSceneWoosh;

    public AudioClip clickingButtonInMenu;

    public AudioClip boxPush;

    public AudioClip boxSplashIntoWater;

    public AudioClip boxUnmovable;

    public AudioClip collectingHexItems;

    public AudioClip collectingBonus;



    // Start is called before the first frame update
    void Start() {

    }


    public void playUISound(EnumCollection.UISounds uiSoundType) {
        AudioClip playThis;

        switch (uiSoundType) {

            case EnumCollection.UISounds.CHANGE_SCENE_WOOSH:
                playThis = changeSceneWoosh;
                break;
            case EnumCollection.UISounds.UI_TRAY_SLIDE:
                playThis = openingClosingInv;
                break;
            case EnumCollection.UISounds.PLACE_ITEM_INTO_HT:
                playThis = putItemIntoHT;
                break;
            case EnumCollection.UISounds.MENU_BUTTON_CLICK:
                playThis = clickingButtonInMenu;
                break;
            default:
                playThis = null;
                break;
        }

        audioSource.PlayOneShot(playThis, 0.9f);
    }

    public void playLvlSound(EnumCollection.LvlSounds lvlSoundType) {
        AudioClip playThis = null;

        switch (lvlSoundType) {
            case EnumCollection.LvlSounds.BOX_PUSH:
                playThis = boxPush;
                break;
            case EnumCollection.LvlSounds.BOX_SPLASH:
                playThis = boxSplashIntoWater;
                break;
            case EnumCollection.LvlSounds.BOX_UNMOVABLE:
                playThis = boxUnmovable;
                break;
            case EnumCollection.LvlSounds.COLLECT_HEXITEM:
                playThis = collectingHexItems;
                break;
            case EnumCollection.LvlSounds.COLLECT_BONUS:
                playThis = collectingBonus;
                break;

        }

        audioSource.PlayOneShot(playThis, 1f);
    }

}
