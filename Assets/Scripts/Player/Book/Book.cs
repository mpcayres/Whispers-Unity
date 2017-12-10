using System.Collections;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class Book : MonoBehaviour {

    public static bool[] pages = { false, false, false, false, false };
    public static int pageQuantity;
    private int pageShowing; // 0 - 2
    private bool lastPageSeen = false, seenAll = false;

    public static bool show = false;
  
    GameObject book, page1, page2,pagebonus;
    MissionManager missionManager;

    Sprite pg1, pg2, pg3, pg4, pg5, pg6, pg78;

	void Start () {
        pageQuantity = 0;
        book = GameObject.Find("HUDCanvas").transform.Find("Book").gameObject;
        page1 = GameObject.Find("HUDCanvas").transform.Find("Book/Page 1").gameObject;
        page2 = GameObject.Find("HUDCanvas").transform.Find("Book/Page 2").gameObject;
        pagebonus = GameObject.Find("HUDCanvas").transform.Find("Book/Page Bonus").gameObject;
        pageShowing = 0;
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
        pg1 = Resources.Load<Sprite>("Sprites/UI/book/page1");
        pg2 = Resources.Load<Sprite>("Sprites/UI/book/page2");
        pg3 = Resources.Load<Sprite>("Sprites/UI/book/page3");
        pg4 = Resources.Load<Sprite>("Sprites/UI/book/page4");
        pg5 = Resources.Load<Sprite>("Sprites/UI/book/page5");
        pg6 = Resources.Load<Sprite>("Sprites/UI/book/page6");
        pg78 = Resources.Load<Sprite>("Sprites/UI/book/page7-8");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !MissionManager.instance.blocked
            && !MissionManager.instance.pausedObject)
        {

            show = false;
            ShowBook();
        }

        if (book.activeSelf && pageQuantity > 0)
        {
            if (pageShowing == 0)
            {
                if (pages[0] == true)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg1;
                }
                if (pages[1] == true)
                {
                    page2.SetActive(true);
                    page2.GetComponent<Image>().sprite = pg2;
                    //resource da pagina 2
                }
            }
            else if (pageShowing == 1) { 
                if (pages[2] == true)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg3;
                }
                if (pages[3] == true)
                {
                    page2.SetActive(true);
                    page2.GetComponent<Image>().sprite = pg4;
                }
            }
            else if (pageShowing == 2)
            {
                if (pages[4] == true)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg5;
                    page2.SetActive(true);
                    page2.GetComponent<Image>().sprite = pg6;
                }
            }
            else if (pageShowing == 3)
            {
                lastPageSeen = true;
                pagebonus.SetActive(true);
                pagebonus.GetComponent<Image>().sprite = pg78;
            }
        }

        if(book.activeSelf && (Input.GetKeyDown(KeyCode.RightArrow))){
            if(pageShowing == 0 && pageQuantity >= 2 && pageShowing != 2) {
                page1.SetActive(false); page2.SetActive(false);
                pageShowing++; 
            }
            else if(pageShowing == 1 && pages[4] == true)
            {
                page1.SetActive(false); page2.SetActive(false);
                pageShowing++;
            }
            else if(pageShowing == 2 && pageQuantity == 5)
            {
                page1.SetActive(false); page2.SetActive(false);
                pageShowing++;
            }
        }
        if (book.activeSelf && (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            if (pageShowing != 0)
            {
                page1.SetActive(false); page2.SetActive(false);
                pagebonus.SetActive(false);
                pageShowing--;
            }
        }
    }

    public void ShowBook()
    {
        if(book.activeSelf)
        {
            if (lastPageSeen) seenAll = true;
            book.SetActive(false);
            missionManager.paused = false;
        }
        else
        {
            book.SetActive(true);
            missionManager.paused = true;   
        }
    }

    public static void AddPage(int number)
    {
        pages[number] = true;
        pageQuantity++;
    }

    public bool SeenAll()
    {
        return seenAll;
    }

}
