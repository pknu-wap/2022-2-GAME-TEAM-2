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
        // GetItem("난장이가 쏘아올린 작은 공");
        // GetItem("소시민");
        // GetItem("윤동주 시집");
        // GetItem("이육사 시집");
        // GetItem("낙원구 행복동");
        // GetItem("초식");
        //GetItem("나무 판자");
        GetItem("칼");
    }

    private void itemDictionaryInit()
    {
        itemDictionary.Add("조건 체크", new Item("조건 체크", "")); // 문 조건 체크를 위한 디폴트 아이템
        itemDictionary.Add("과산화수소", new Item("과산화수소", "어떤 얼룩도 깨끗하게 지워주는 아이템이다."));
        itemDictionary.Add("알코올램프", new Item("알코올램프", "불을 붙힐 수 있는 알코올램프이다."));
        itemDictionary.Add("3-2반 열쇠", new Item("3-2반 열쇠", "3-2반 열쇠이다."));
        itemDictionary.Add("미술실 열쇠", new Item("미술실 열쇠", "미술실의 열쇠이다."));
        itemDictionary.Add("방송실 열쇠", new Item("방송실 열쇠", "방송실의 열쇠이다."));
        itemDictionary.Add("배전함 열쇠(3F)", new Item("배전함 열쇠(3F)", "3층 배전함의 열쇠이다."));
        itemDictionary.Add("2-1반 열쇠", new Item("2-1반 열쇠", "2-1반의 열쇠"));
        itemDictionary.Add("1-1반 열쇠", new Item("1-1반 열쇠", "1-1반의 열쇠"));
        itemDictionary.Add("2-2반 열쇠", new Item("2-2반 열쇠", "2-2반의 열쇠"));
        ItemDictionary.Add("도서관 열쇠", new Item("도서관 열쇠", "도서관의 열쇠이다."));
        itemDictionary.Add("컴퓨터실 열쇠", new Item("컴퓨터실 열쇠", "컴퓨터실 열쇠"));
        itemDictionary.Add("음악실 열쇠", new Item("음악실 열쇠", "음악실 열쇠이다."));
        itemDictionary.Add("시청각실 열쇠", new Item("시청각실 열쇠", "시청각실 열쇠이다."));
        itemDictionary.Add("출입문 열쇠", new Item("출입문 열쇠", "밖에 나갈 수 있다!."));
        itemDictionary.Add("나무 판자", new Item("나무 판자", "끊어진 곳에 덧대어서 지나갈수 있게 할\n수 있을 것 같다."));
        ItemDictionary.Add("칼", new Item("칼", "날카롭게 날이 선 칼이다."));
        ItemDictionary.Add("난장이가 쏘아올린 작은 공", new Item("난장이가 쏘아올린 작은 공", "군부 독재시절 소시민들의 암울함을\n그려낸 소설(1-A)"));
        ItemDictionary.Add("소시민", new Item("소시민", "1960년대 소시민들의 사회상을\n그려낸 소설(2-A)"));
        ItemDictionary.Add("윤동주 시집", new Item("윤동주 시집", "강점기 시대의 대표 저항 문학가인\n윤동주의 시집이다.(3-A)"));
        ItemDictionary.Add("이육사 시집", new Item("이육사 시집", "강점기 시대의 대표 저항 문학가인\n이육사의 시집이다.(2-C)"));
        ItemDictionary.Add("낙원구 행복동", new Item("낙원구 행복동", "1970년대 소시민들의 사회상을\n.그려낸 소설(2-B)"));
        ItemDictionary.Add("초식", new Item("초식", "1970년대 소시민들의 사회상을\n.그려낸 소설(3-C)"));
    }

    public void GetItem(string _itemName)
    {
        // 현재 인벤토리에 얻으려는 아이템이 없으면 itemList에서 아이템을 찾아서 추가해줌.
        if (!inventoryItemList.Contains(itemDictionary[_itemName]))
        {
            inventoryItemList.Add(itemDictionary[_itemName]);
        }
    }
    
    public void DeleteItem(string _itemName)
    {
        Item forDeleteItem = ItemDictionary[_itemName];
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i] == forDeleteItem)
            {
                inventoryItemList.Remove(forDeleteItem);
                return;
            }
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
