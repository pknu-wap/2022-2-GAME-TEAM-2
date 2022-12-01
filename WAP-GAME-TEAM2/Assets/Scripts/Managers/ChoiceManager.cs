using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;
    #region SingleTon
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
    #endregion
    public GameObject go;   // 활성화 게임오브젝트

    public Text dialText;
    public Text[] choiceTexts;
    public GameObject[] highlighted_Panels;

    private int highlightedNum;
    public int SelectedNum { get; private set; }

    public bool choiceIng;

    public string keySound;
    void Start()
    {
        SelectedNum = -1;
        highlightedNum = 0;
        choiceIng = false;
    }

    void Update()
    {
        if (!choiceIng) return;
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SelectedNum = highlightedNum;
            ExitChoice();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (highlightedNum > 0)
                highlightedNum--;
            else
                highlightedNum = 1;
            ChangeHighlight();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (highlightedNum < 1)
                highlightedNum++;
            else
                highlightedNum = 0;
            ChangeHighlight();
        }
    }

    public void ShowChoice(string[] _choiceText, string _dial)
    {
        choiceIng = true;
        go.SetActive(true);
        for (int i = 0; i < _choiceText.Length; i++)
            choiceTexts[i].text = _choiceText[i];
        dialText.text = _dial;
        highlighted_Panels[0].SetActive(true);
    }

    private void ExitChoice()
    {
        AudioManager.instance.PlaySFX(keySound);
        highlightedNum = 0;
        choiceIng = false;
        dialText.text = "";
        for (int i = 0; i < highlighted_Panels.Length; i++)
            highlighted_Panels[i].SetActive(false);
        go.SetActive(false);
    }

    private void ChangeHighlight()
    {
        AudioManager.instance.PlaySFX(keySound);
        for (int i = 0; i < highlighted_Panels.Length; i++)
            highlighted_Panels[i].SetActive(false);
        highlighted_Panels[highlightedNum].SetActive(true);
    }

}
