using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LvlTimer : MonoBehaviour {

    private UI_StatusInfoController currStatusPanel;

    // [SerializeField]
    // private float lvlStartTime;

    // public float timeElapsed;

    public TimeSpan timeElapsedSpan;

    private TimeSpan oneSecond = TimeSpan.FromSeconds(1);

    // Start is called before the first frame update
    void Start() {
        // lvlStartTime = Time.time;
        // Debug.Log("CURRENT TIME:" + Time.time);

        UI_StatusInfoController status = this.gameObject.GetComponent<UI_StatusInfoController>();
        if (status == null) {
            Debug.LogWarning("StatusInfoCtrl Obj in curr lvl has no StatusInfoController attached!!");
        } else {
            currStatusPanel = status;
        }

        TimeSpan timeElapsedSpan = TimeSpan.FromSeconds(0);

        InvokeRepeating("updTimeElapsed", 1f, 1f);


    }



    public void updTimeElapsed() {
        if (LevelMasterSingleton.LM.paused) {
            //Do nothing
        } else {
            timeElapsedSpan = timeElapsedSpan + oneSecond;
            string minStr = Math.Floor(timeElapsedSpan.TotalMinutes).ToString();
            string secStr = Math.Floor(timeElapsedSpan.TotalSeconds).ToString();

            if (minStr.Length == 1) {
                minStr = "0" + minStr;
            }

            if (secStr.Length == 1) {
                secStr = "0" + secStr;
            }


            String timeStr = minStr + ":" + secStr;
            currStatusPanel.setCurrTime(timeStr);
        }
    }

    public LvlTimeContainer StopTimer() {
        CancelInvoke();
        return new LvlTimeContainer((int)Math.Floor(timeElapsedSpan.TotalMinutes), (int)Math.Floor(timeElapsedSpan.TotalSeconds));
    }

    public class LvlTimeContainer : IComparable<LvlTimeContainer> {
        public int min;
        public int sec;

        public LvlTimeContainer(int currMin, int currSec) {
            min = currMin;
            sec = currSec;
        }

        public LvlTimeContainer(int totalSec) : this(TimeSpan.FromSeconds(totalSec)) { }



        public LvlTimeContainer(TimeSpan fromTS) {
            min = (int)Math.Floor(fromTS.TotalMinutes);
            sec = (int)Math.Floor(fromTS.TotalSeconds);
        }

        public string getTimeStr() {
            string minStr = min.ToString();
            string secStr = sec.ToString();

            if (minStr.Length == 1) {
                minStr = "0" + minStr;
            }

            if (secStr.Length == 1) {
                secStr = "0" + secStr;
            }

            return minStr + ":" + secStr;
        }

        public int totalSeconds() {
            return min * 60 + sec;
        }

        public int CompareTo(LvlTimeContainer compareWith) {
            int thisTotalSec = this.totalSeconds();
            int thatTotalSec = compareWith.totalSeconds();

            if (thisTotalSec > thatTotalSec) {
                return 1;
            } else if (thisTotalSec < thatTotalSec) {
                return -1;
            } else {
                return 0;
            }
        }

    }
}
