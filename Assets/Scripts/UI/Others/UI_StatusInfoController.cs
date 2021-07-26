using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls the status info panel below the HashFunction panel
//Displays number of hex items and bonus coins collected
public class UI_StatusInfoController : MonoBehaviour {

    private LvlTimer timerMgr;

    [SerializeField]
    private Text hexCollectedTEXT;
    [SerializeField]
    private Text hexAvailTEXT;
    [SerializeField]
    private Text bonusCollectedTEXT;
    [SerializeField]
    private Text bonusAvailTEXT;
    [SerializeField]
    private Text currTimeTEXT;



    void Start() {
        this.setBonusCollectedAmt(0);
        this.setHexCollectedAmt(0);

        LvlTimer currTimer = this.gameObject.GetComponent<LvlTimer>();
        if (currTimer == null) {
            Debug.LogWarning("StatusInfoCtrl Obj in curr lvl has no LvlTimer attached!!");
        } else {
            timerMgr = currTimer;
        }
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

    public void setCurrTime(string currTimeString) {
        currTimeTEXT.text = currTimeString;
    }

    public LvlTimer.LvlTimeContainer StopLvlTimer() {
        return timerMgr.StopTimer();
    }


}
