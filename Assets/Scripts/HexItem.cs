using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexItem : MonoBehaviour
{
    // Start is called before the first frame update

    //The short name of the item to display inside the item graphic itself
    public string itemName;

    //The full name of the item to display in text boxes below the item in the inventory/hash table
    public string fullName;

    //Description when u hover over an item
    public string desc;

    //Reference to the text gameObject in game to display the itemName on
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
