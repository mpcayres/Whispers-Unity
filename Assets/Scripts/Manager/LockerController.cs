using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
/**
 * 
 * senha cofre corredor: 159
 * senha cadeado porão: 376
 * 
 falta:
    - considerações para abertura de porão
    - objeto a ser inserido quando abre cofre atras do quadro na parede
    - voltar o quadro para a parede
    - sair da abertura do cadeado caso desista (z de novo ou esc?)
     
     **/
public class LockerController : MonoBehaviour
{

    public Text row1;
    public Text row2;
    public Text row3;

    int selectedRow = 1;

    int selectedNumber1 = 1;
    int selectedNumber2 = 1;
    int selectedNumber3 = 1;

    public string password;

    public string boardName;

    bool basement = false;
    bool opened = false;
    bool tried = false;

    public AudioClip click;
    public AudioClip success;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public void Start()
    {
        source.clip = click;
    }
    public void Update()
    {
        
        if (gameObject.transform.GetChild(0).gameObject.activeSelf) {
            if (Input.GetButtonDown("Vertical"))
            { //up(positive) e down(negative)
                if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0) {
                    DownRow();
                }
                else if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0)
                {
                    UpRow();
                }
            }
            else if (Input.GetButtonDown("Horizontal"))
            {//right(positive) e left(negative)
                if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
                {
                    NumbersRight();
                }
                else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
                {
                    NumbersLeft();
                }
            }
            if (gameObject.transform.GetChild(0).gameObject.activeSelf) {
                if (row1.text[3] == password[0] && row2.text[3] == password[1] && row3.text[3] == password[2] && !opened)
                {
                    opened = true;
                    if (!basement /*&& Book.pageQuantity >= 6*/ ) //cofre atrás do quadro - descomentar na versão final. comentário apenas para testes
                    {
                        //adicionar algo do inventário
                        source.clip = success;
                        source.PlayOneShot(success);
                        Invoke("Show", 0.5f);
                    }
                    else if (Book.pageQuantity >= 4) { // porta do porão


                    }
                }
            }
        }
    }


    void DownRow()
    {
        GameObject selectorRow;
        if (selectedRow == 3)
            selectedRow = 1;
        else
            selectedRow++;
        switch (selectedRow)
        {
            case 1:
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow3").gameObject;
                selectorRow.SetActive(false);
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow1").gameObject;
                selectorRow.SetActive(true);
                break;
            case 2:
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow1").gameObject;
                selectorRow.SetActive(false);
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow2").gameObject;
                selectorRow.SetActive(true);
                break;
            case 3:
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow2").gameObject;
                selectorRow.SetActive(false);
                selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow3").gameObject;
                selectorRow.SetActive(true);
                break;
        }
        source.PlayOneShot(click);
    }

    void UpRow() {
            GameObject selectorRow;
            if (selectedRow == 1)
                selectedRow = 3;
            else
                selectedRow--;
            switch (selectedRow)
            {
                case 1:
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow2").gameObject;
                    selectorRow.SetActive(false);
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow1").gameObject;
                    selectorRow.SetActive(true);
                    break;
                case 2:
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow3").gameObject;
                    selectorRow.SetActive(false);
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow2").gameObject;
                    selectorRow.SetActive(true);
                    break;
                case 3:
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow1").gameObject;
                    selectorRow.SetActive(false);
                    selectorRow = this.transform.GetChild(0).gameObject.transform.Find("SelectorRow3").gameObject;
                    selectorRow.SetActive(true);
                    break;


            }
        source.PlayOneShot(click);

        }
    void NumbersLeft()
    {
        if (selectedRow == 1)
        {
            if (selectedNumber1 == 10)
                selectedNumber1 = 1;
            else
                selectedNumber1++;
        }
        else if (selectedRow == 2)
        {
            if (selectedNumber2 == 10)
                selectedNumber2 = 1;
            else
                selectedNumber2++;
        }
        else if (selectedRow == 3)
        {
            if (selectedNumber3 == 10)
                selectedNumber3 = 1;
            else
                selectedNumber3++;
        }
        ChangeText();
    }

    void NumbersRight() {
        if (selectedRow == 1)
        {
            if (selectedNumber1 == 1)
                selectedNumber1 = 10;
            else
                selectedNumber1--;
        }
        else if (selectedRow == 2)
        {
            if (selectedNumber2 == 1)
                selectedNumber2 = 10;
            else
                selectedNumber2--;
        }
        else if (selectedRow == 3)
        {
            if (selectedNumber3 == 1)
                selectedNumber3 = 10;
            else
                selectedNumber3--;
        }
        ChangeText();
    }

    void ChangeText() {

        int selectedNumber = 0;
        if (selectedRow == 1)
            selectedNumber = selectedNumber1;
        else if (selectedRow == 2)
            selectedNumber = selectedNumber2;
        else if (selectedRow == 3)
            selectedNumber = selectedNumber3;

        source.PlayOneShot(click);
            switch (selectedNumber)
            {
                case 1:
                    if (selectedRow == 1)
                        row1.text = "9  0  1";
                    else if (selectedRow == 2)
                        row2.text = "9  0  1";
                    else if (selectedRow == 3)
                        row3.text = "9  0  1";
                    break;
                case 2:
                    if (selectedRow == 1)
                        row1.text = "0  1  2";
                    else if (selectedRow == 2)
                        row2.text = "0  1  2";
                    else if (selectedRow == 3)
                        row3.text = "0  1  2";
                    break;
                case 3:
                    if (selectedRow == 1)
                        row1.text = "1  2  3";
                    else if (selectedRow == 2)
                        row2.text = "1  2  3";
                    else if (selectedRow == 3)
                        row3.text = "1  2  3";
                    break;
                case 4:
                    if (selectedRow == 1)
                        row1.text = "2  3  4";
                    else if (selectedRow == 2)
                        row2.text = "2  3  4";
                    else if (selectedRow == 3)
                        row3.text = "2  3  4";
                    break;
                case 5:
                    if (selectedRow == 1)
                        row1.text = "3  4  5";
                    else if (selectedRow == 2)
                        row2.text = "3  4  5";
                    else if (selectedRow == 3)
                        row3.text = "3  4  5";
                    break;
                case 6:
                    if (selectedRow == 1)
                        row1.text = "4  5  6";
                    else if (selectedRow == 2)
                        row2.text = "4  5  6";
                    else if (selectedRow == 3)
                        row3.text = "4  5  6";
                    break;
                case 7:
                    if (selectedRow == 1)
                        row1.text = "5  6  7";
                    else if (selectedRow == 2)
                        row2.text = "5  6  7";
                    else if (selectedRow == 3)
                        row3.text = "5  6  7";
                    break;
                case 8:
                    if (selectedRow == 1)
                        row1.text = "6  7  8";
                    else if (selectedRow == 2)
                        row2.text = "6  7  8";
                    else if (selectedRow == 3)
                        row3.text = "6  7  8";
                    break;
                case 9:
                    if (selectedRow == 1)
                        row1.text = "7  8  9";
                    else if (selectedRow == 2)
                        row2.text = "7  8  9";
                    else if (selectedRow == 3)
                        row3.text = "7  8  9";
                    break;
                case 10:
                    if (selectedRow == 1)
                        row1.text = "8  9  0";
                    else if (selectedRow == 2)
                        row2.text = "8  9  0";
                    else if (selectedRow == 3)
                        row3.text = "8  9  0";
                    break;

            }



    }

    void OnTriggerStay2D(Collider2D other)
    {
        GameObject board = GameObject.Find(boardName).gameObject;
        if (other.gameObject.tag.Equals("Player") && CrossPlatformInputManager.GetButton("keyInteract") && !gameObject.transform.GetChild(0).gameObject.activeSelf) {
            
            if (board)
            {
                board.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/cofre");
            }
            else {

                basement = true;
            }

            Invoke("Show", 1.5f);
            tried = true;

        }
        else if (tried && other.gameObject.tag.Equals("Player") && CrossPlatformInputManager.GetButton("keyInteract") && gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            if (board)
            {
                board.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/quadroInutil2");
            }
            else
            {

                basement = true;
            }
            Invoke("Show", 0.2f);
            tried = false;
        }


    }
    void Show() {
        GameObject locker = gameObject.transform.GetChild(0).gameObject;
        if (locker.activeSelf)
        {
            locker.SetActive(false);
           // MissionManager.instance.paused = false;
            MissionManager.instance.blocked = false;
        }
        else
        {
            locker.SetActive(true);
           // MissionManager.instance.paused = false;
            MissionManager.instance.blocked = false;

        }


            
    }
    
}
