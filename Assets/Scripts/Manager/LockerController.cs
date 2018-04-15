using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
/**
 falta:
    - som de troca de número
    - som de acerto de número
    - considerações para abertura de porão
    - senha certa
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

    public void Start()
    {
        
    }
    public void Update()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            MissionManager.instance.paused = true;
            MissionManager.instance.blocked = true;

        }
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
            if (row1.text[3] == '0' & row2.text[3]=='1' & row3.text[3]=='2')
            {
                if (!basement)
                {
                    //adicionar algo do inventário
                    Invoke("Show", 0.5f); //tocar som de acerto
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
    }

        void UpRow()
        {
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
        if (other.gameObject.tag.Equals("Player") && CrossPlatformInputManager.GetButtonDown("keyInteract") && !gameObject.transform.GetChild(0).gameObject.activeSelf) {
            GameObject board = GameObject.Find(boardName).gameObject;
            if (board)
            {
                board.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/cofre");
            }
            else {

                basement = true;
            }
            Invoke("Show", 1.5f);

        }


    }
    void Show() {
        GameObject locker = gameObject.transform.GetChild(0).gameObject;
        locker.SetActive(true);
    }

        //checar se é igual

    //trocar imagem

    //abrir porta ou cofre
}
