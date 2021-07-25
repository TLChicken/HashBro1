using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls the status info panel below the HashFunction panel
//Displays number of hex items and bonus coins collected
public class UI_StatusInfoController : MonoBehaviour {


    [SerializeField]
    private Text hexCollectedTEXT;
    [SerializeField]
    private Text hexAvailTEXT;
    [SerializeField]
    private Text bonusCollectedTEXT;
    [SerializeField]
    private Text bonusAvailTEXT;

    void Start() {
        this.setBonusCollectedAmt(0);
        this.setHexCollectedAmt(0);
    }

    public void setHexCollectedAmt(int amt) {
        hexCollectedTEXT.text = amt.ToString();
    }

    public void setHexAvailAmt(int amt) {
        hexAvailTEXT.text = amt.ToString();
    }

    public void setBonusCollectedAmt(int amt) {
        bonusCollectedTEXT.text = amt.ToString();
    }

    public void setBonusAvailAmt(int amt) {
        bonusAvailTEXT.text = amt.ToString();
    }

    public void hexJustGotCollected() {
        int currAmt = int.Parse(hexCollectedTEXT.text);
        setHexCollectedAmt(currAmt + 1);
    }

    public void bonusJustGotCollected() {
        int currAmt = int.Parse(bonusCollectedTEXT.text);
        setBonusCollectedAmt(currAmt + 1);
    }



}
