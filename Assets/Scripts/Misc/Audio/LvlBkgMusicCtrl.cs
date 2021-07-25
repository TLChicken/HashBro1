using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LvlBkgMusicCtrl : MonoBehaviour {
    public AudioSource audioSrc;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start() {
        if (GameMgrSingleton.GM != null) {
            audioMixer.SetFloat("volume", GameMgrSingleton.GM.currVol);
        } else {
            audioMixer.SetFloat("volume", -5f);
        }

    }

    // Update is called once per frame
    void Update() {

    }
}
