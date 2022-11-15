using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Text itemName_Text;
    public GameObject FocusPanel; // 아이템이 선택되면 활성화될 패널 오브젝트
    
    public void AddItem(Item _item)
    {
        itemName_Text.text = _item.itemName;
    }

    public void RemoveItem()
    {
        itemName_Text.text = "";
    }
}
