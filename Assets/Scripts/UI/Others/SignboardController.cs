using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignboardController : MonoBehaviour {
    public GameObject signBoardParentGO;

    public Text headerText;
    public Text bodyText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void setMsgAndShowSignboard(string headerTxtIn, string bodyTxtIn) {
        setMessage(headerTxtIn, bodyTxtIn);
        showSignBoard();
    }

    public void setMsgAndShowSignboard(string bodyTxtIn) {
        setMessage(bodyTxtIn);
        showSignBoard();
    }

    public void setMessage(string headerTxtIn, string bodyTxtIn) {
        headerText.text = headerTxtIn;
        bodyText.text = bodyTxtIn;
    }

    public void setMessage(string bodyTxtIn) {
        bodyText.text = bodyTxtIn;

    }

    public void showSignBoard() {
        signBoardParentGO.SetActive(true);
    }

    public void hideSignboard() {
        signBoardParentGO.SetActive(false);
    }




}
