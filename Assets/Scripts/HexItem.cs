using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexItem : MonoBehaviour
{
    // Start is called before the first frame update

    public string itemName;
    public string fullName;
    public string desc;

    public Text itemTextInGame;

    void Start()
    {
        itemTextInGame.text = itemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
