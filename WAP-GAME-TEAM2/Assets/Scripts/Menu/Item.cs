using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemDescription;
    public Item(string _itemName, string _itemDescription)
    {
        itemName = _itemName;
        itemDescription = _itemDescription;
    }

}
