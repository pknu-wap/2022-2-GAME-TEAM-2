using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private Dictionary<string ,Item> itemDictionary; // 게임 내에 존재하는 모든 아이템 리스트
    private List<Item> inventoryItemList; // 플레이어가 가지고 있는 아이템 리스트
    public List<Item> InventoryItemList => inventoryItemList;
    public Dictionary<string, Item> ItemDictionary => itemDictionary;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        itemDictionary = new Dictionary<string, Item>();
        itemDictionaryInit();
        inventoryItemList = new List<Item>();
    }

    private void itemDictionaryInit()
    {
        
    }

    public void GetItem(string _itemName)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            // 현재 인벤토리에 얻으려는 아이템이 없으면 itemList에서 아이템을 찾아서 추가해줌.
            inventoryItemList.Add(itemDictionary[_itemName]);
        }
    }
    
    
}
