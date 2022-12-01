using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class LibraryUI : MonoBehaviour
{
    public int sectorNum;
    public int actionNum;
    public Text[] UI_texts;
    public GameObject[] highlightedPanels;

    public LibraryEvent lbe;

    public string[] targetBookNames;
    public string[] playerOwnBooks;

    public string inputDial;
    public string outputDial;

    public string keySound;
    public string inputSound;

    private int highlightedNum;
    private int playerOwnCount;

    private int maxCount;

    private bool isWorking;
    private bool isEmpty;

    private List<string> sectorOwnBooks;
    private List<Item> inventoryItemList;
    void OnEnable()
    {
        highlightedNum = 0;
        playerOwnCount = 0;
        sectorOwnBooks = EventManager.instance.libraryLists[sectorNum];
        inventoryItemList = InventoryManager.instance.InventoryItemList;
        
        if (actionNum == 0)
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                string itemName = inventoryItemList[i].itemName;
                for (int j = 0; j < targetBookNames.Length; j++)
                {
                    if (itemName == targetBookNames[j])
                    {
                        playerOwnBooks[playerOwnCount++] = itemName;
                        break;
                    }
                }
            }

            if (playerOwnCount == 0)
            {
                isEmpty = true;
                return;
            }

            for (int i = 0; i < playerOwnCount; i++)
            {
                UI_texts[i].text = playerOwnBooks[i];
                UI_texts[i].gameObject.SetActive(true);
            }
            highlightedPanels[0].SetActive(true);
            maxCount = playerOwnCount;
        }
        
        else
        {
            if (sectorOwnBooks.Count == 0)
            {
                isEmpty = true;
                return;
            }

            for (int i = 0; i < sectorOwnBooks.Count; i++)
            {
                UI_texts[i].text = sectorOwnBooks[i];
                UI_texts[i].gameObject.SetActive(true);
            }
            highlightedPanels[0].SetActive(true);
            maxCount = sectorOwnBooks.Count;
        }
        
    }

  
    void Update()
    {
        if (isWorking) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.instance.PlaySFX(keySound);
            ExitUI();
        }

        if (isEmpty) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioManager.instance.PlaySFX(keySound);
          highlightedPanels[highlightedNum].SetActive(false);
          if (highlightedNum == 0)
              highlightedNum = maxCount - 1;
          else
              highlightedNum--;
          highlightedPanels[highlightedNum].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.instance.PlaySFX(keySound);
            highlightedPanels[highlightedNum].SetActive(false);
            if (highlightedNum == maxCount - 1)
                highlightedNum = 0;
            else
                highlightedNum++;
            highlightedPanels[highlightedNum].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (actionNum == 0)
            {
                AudioManager.instance.PlaySFX(keySound);
                string bookName = playerOwnBooks[highlightedNum];
                sectorOwnBooks.Add(bookName);
                InventoryManager.instance.DeleteItem(bookName);
                isWorking = true;
                StartCoroutine(InputCo());
            }
            else
            {
                AudioManager.instance.PlaySFX(keySound);
                string bookName = sectorOwnBooks[highlightedNum];
                InventoryManager.instance.GetItem(bookName);
                for (int i = 0; i < sectorOwnBooks.Count; i++)
                {
                    if (bookName == sectorOwnBooks[i])
                    {
                        sectorOwnBooks.RemoveAt(i);
                        break;
                    }
                }
                isWorking = true;
                StartCoroutine(OutputCo());
            }
        }
        
}

    private void ExitUI()
    {
        highlightedNum = 0;
        playerOwnCount = 0;
        isEmpty = false;
        isWorking = false;
        for (int i = 0; i < UI_texts.Length; i++)
        {
            UI_texts[i].text = "";
            UI_texts[i].gameObject.SetActive(false);
            highlightedPanels[i].SetActive(false);
            playerOwnBooks[i] = "";
        }
        EventManager.instance.SetEvent(false);
        gameObject.SetActive(false);
    }

    private IEnumerator InputCo()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(inputSound);
        yield return new WaitForSeconds(1f);
        DialogueManager.instance.ShowText(inputDial);
        yield return new WaitUntil(() => !DialogueManager.instance.talking);
        ExitUI();
        lbe.CheckResult();
    }
    
    private IEnumerator OutputCo()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(inputSound);
        yield return new WaitForSeconds(1f);
        DialogueManager.instance.ShowText(outputDial);
        yield return new WaitUntil(() => !DialogueManager.instance.talking);
        ExitUI();
    }
}
