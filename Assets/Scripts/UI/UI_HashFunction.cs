using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HashFunction : MonoBehaviour {

    //Put in the image that is supposed to display the hash function
    [Tooltip("Open the HashFunction Canvas, go in all the way and drag in the Hash Function Img")]
    public Image hfImg;

    //These text boxes are for displaying messages. Switch off the Hash Function Image to display this.
    public Text hfHeaderText;

    public Text hfBodyText;

    private IEnumerator showingMsgCoroutine;
    private bool coroutineRunning = false;


    public void setHFImg(Sprite theHashFunctionPic) {
        this.hfImg.sprite = theHashFunctionPic;
    }

    public void changeMessage(string bodyText) {
        hfBodyText.text = bodyText;
    }

    public void changeMessage(string headerText, string bodyText) {
        hfHeaderText.text = headerText;
        hfBodyText.text = bodyText;
    }

    public void showMessage() {
        this.hfImg.gameObject.SetActive(false);
    }

    public void showHF() {
        if (this.hfImg.sprite != null) {
            this.hfImg.gameObject.SetActive(true);
        }

    }

    public void showCurrMsgForSeconds(float amtSecs) {
        if (coroutineRunning) {
            return;
        }

        showMessage();

        //Example of how to do something async
        showingMsgCoroutine = WaitThenShowHF(amtSecs);
        StartCoroutine(showingMsgCoroutine);

    }

    public void changeAndShowMsgForSeconds(string headerText, string bodyText, float amtSecs) {
        if (coroutineRunning) {
            return;
        }

        string previousHeader = hfHeaderText.text;
        string previousBody = hfBodyText.text;

        this.changeMessage(headerText, bodyText);
        showingMsgCoroutine = WaitThenShowHFAndChangeMsg(previousHeader, previousBody, amtSecs);
        StartCoroutine(showingMsgCoroutine); //OMG REMEMBER TO START THE COROUTINE
        Debug.Log("Started Routine");


    }

    private IEnumerator WaitThenShowHF(float amtSecs) {
        coroutineRunning = true;
        yield return new WaitForSeconds(amtSecs);
        showHF();
        coroutineRunning = false;
    }

    private IEnumerator WaitThenShowHFAndChangeMsg(string headerText, string bodyText, float amtSecs) {
        coroutineRunning = true;
        yield return new WaitForSeconds(amtSecs);
        Debug.Log("Coroutine ended");
        changeMessage(headerText, bodyText);
        showHF();
        coroutineRunning = false;
    }

}
