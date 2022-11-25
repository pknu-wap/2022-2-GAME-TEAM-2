using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private AudioManager theAudio;

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
        itemDictionary.Add("조건 체크", new Item("조건 체크", "")); // 문 조건 체크를 위한 디폴트 아이템
        itemDictionary.Add("과산화수소", new Item("과산화수소", "어떤 얼룩도 깨끗하게 지워주는 아이템이다.")); 
        itemDictionary.Add("방송실 열쇠", new Item("방송실 열쇠", "방송실의 열쇠이다."));
        itemDictionary.Add("배전함 열쇠(3F)", new Item("배전함 열쇠(3F)", "3층 배전함의 열쇠이다."));
        itemDictionary.Add("2-1반 열쇠", new Item("2-1반 열쇠", "2-1반의 열쇠"));
        itemDictionary.Add("2-2반 열쇠", new Item("2-2반 열쇠", "2-2반의 열쇠"));
        itemDictionary.Add("컴퓨터실 열쇠", new Item("컴퓨터실 열쇠", "컴퓨터실 열쇠"));
        itemDictionary.Add("음악실 열쇠", new Item("음악실 열쇠", "음악실 열쇠"));
    }

    public void GetItem(string _itemName)
    {
        // 현재 인벤토리에 얻으려는 아이템이 없으면 itemList에서 아이템을 찾아서 추가해줌.
        if (!inventoryItemList.Contains(itemDictionary[_itemName]))
        {
            inventoryItemList.Add(itemDictionary[_itemName]);
        }
    }

    public bool SearchItem(string _itemName)
    {
        // 아이템을 소유하고 있으면 true 리턴.
        if (inventoryItemList.Contains(itemDictionary[_itemName]))
            return true;
        return false;
    }
    
    
}
