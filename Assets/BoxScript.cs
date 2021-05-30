using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Transform HashBromoveToThisSpot;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;        
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(this.movePoint.position, HashBromoveToThisSpot.position) <= 0.05f) {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1) {
                movePoint.position += new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f);
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1) {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}
