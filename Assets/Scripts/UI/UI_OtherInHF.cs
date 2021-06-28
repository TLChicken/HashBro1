using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OtherInHF : MonoBehaviour {

    public Image fullScreenImg;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void setFullScreenImg(Sprite theImg) {
        fullScreenImg.sprite = theImg;
    }

    public void showFullScreenImg() {
        fullScreenImg.gameObject.SetActive(true);
    }

    public void hideFullScreenImg() {
        fullScreenImg.gameObject.SetActive(false);
    }



}
