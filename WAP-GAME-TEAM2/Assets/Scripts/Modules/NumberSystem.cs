using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    private AudioManager theAudio;
    [SerializeField] private string key_sound;
    [SerializeField] private string enter_sound;
    [SerializeField] private string cancel_sound;
    [SerializeField] private string correct_sound;

    private int count;
    private int selectedTextBox;
    private int result;
    private int correctNumber;

    private string tempNumber;

    [SerializeField] private GameObject superObject;  // 자릿수에 맞도록 가운데 정렬
    [SerializeField] private GameObject[] panel;
    [SerializeField] private Text[] Number_Text;

    [SerializeField] private Animator anim;

    public bool activeated;         // 플레이어가 움직이지 못하도록
    private bool keyInput;          // 키처리 활성화, 비활성화
    private bool correctFlag;       // 정답 여부 판별

    // Start is called before the first frame update
    void Start()
    {
        theAudio = AudioManager.instance;
    }

    public void ShowNumber(int _correctNumber)
    {
        correctNumber = _correctNumber;
        activeated = true;
        correctFlag = false;

        // 비밀번호 자릿수 만큼 패널 활성화
        string temp = correctNumber.ToString();
        for (int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }

        //패널 화면 가운데 배치
        superObject.transform.position = new Vector3(
            superObject.transform.position.x + 0.75f * count,
            superObject.transform.position.y,
            superObject.transform.position.z);

        selectedTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyInput = true;
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text); 
        if (_arrow == "DOWN")
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;
        }
        else if (_arrow =="UP")
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;
        }
        Number_Text[selectedTextBox].text = temp.ToString();
    }

    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for (int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;
        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.PlaySFX(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.PlaySFX(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.PlaySFX(key_sound);

                if (selectedTextBox < count)
                    selectedTextBox++;
                else
                    selectedTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.PlaySFX(key_sound);

                if (selectedTextBox > 0)
                    selectedTextBox--;
                else
                    selectedTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.PlaySFX(enter_sound);

                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.PlaySFX(cancel_sound);

                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;
        for (int i = count; i >= 0; i--)
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text;
        }

        yield return new WaitForSeconds(0.5f);

        result = int.Parse(tempNumber);
        if (result == correctNumber)
        {
            theAudio.PlaySFX(correct_sound);
            correctFlag = true;
            
        }
        else
        {
            theAudio.PlaySFX(cancel_sound);
            correctFlag = false;
        }

        StartCoroutine(ExitCoroutine());
    }
    IEnumerator ExitCoroutine()
    {
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i <= count; i++)
        {
            panel[i].SetActive(false);
        }
        superObject.transform.position = new Vector3(
            superObject.transform.position.x - 0.75f * count,
            superObject.transform.position.y,
            superObject.transform.position.z);

        activeated = false;
    }
}
