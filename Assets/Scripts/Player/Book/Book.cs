using Image = UnityEngine.UI.Image;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Book : MonoBehaviour {

    public static int pageQuantity = 0;
    public static bool bookBlocked = true;
    public static bool show = false;

    private int pageShowing; // 0 - 2
    private bool lastPageSeen = false, seenAll = false, invertWorldAux = false;

    GameObject book, page1, page2, pagebonus;
    Sprite pg1, pg2, pg3, pg4, pg5, pg6, pg78;

	void Start ()
    {
        book = GameObject.Find("HUDCanvas").transform.Find("Book").gameObject;
        page1 = GameObject.Find("HUDCanvas").transform.Find("Book/Page 1").gameObject;
        page2 = GameObject.Find("HUDCanvas").transform.Find("Book/Page 2").gameObject;
        pagebonus = GameObject.Find("HUDCanvas").transform.Find("Book/Page Bonus").gameObject;
        invertWorldAux = MissionManager.instance.invertWorld;
        pageShowing = 0;

        if (PlayerPrefs.HasKey("Language") && PlayerPrefs.GetString("Language").Equals("PR_BR"))
        {
            //portugês
            pg1 = Resources.Load<Sprite>("Sprites/UI/book/page1");
            pg2 = Resources.Load<Sprite>("Sprites/UI/book/page2");
            pg3 = Resources.Load<Sprite>("Sprites/UI/book/page3");
            pg4 = Resources.Load<Sprite>("Sprites/UI/book/page4");
            pg5 = Resources.Load<Sprite>("Sprites/UI/book/page5");
            pg6 = Resources.Load<Sprite>("Sprites/UI/book/page6");
            pg78 = Resources.Load<Sprite>("Sprites/UI/book/page7-8");
        }
        else
        {
            //inglês
            pg1 = Resources.Load<Sprite>("Sprites/UI/book/page1");
            pg2 = Resources.Load<Sprite>("Sprites/UI/book/page2");
            pg3 = Resources.Load<Sprite>("Sprites/UI/book/page3");
            pg4 = Resources.Load<Sprite>("Sprites/UI/book/page4");
            pg5 = Resources.Load<Sprite>("Sprites/UI/book/page5");
            pg6 = Resources.Load<Sprite>("Sprites/UI/book/page6");
            pg78 = Resources.Load<Sprite>("Sprites/UI/book/page7-8");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((CrossPlatformInputManager.GetButtonDown("keyJournal") || 
            (CrossPlatformInputManager.GetButtonDown("keyUseObject") && Inventory.GetCurrentItemType() == Inventory.InventoryItems.LIVRO))
            && !Inventory.open && !MissionManager.instance.showMissionStart && !MissionManager.instance.blocked &&
            !MissionManager.instance.pausedObject && !bookBlocked)
        {
            ShowBook();
        }

        if (invertWorldAux != MissionManager.instance.invertWorld)
        {
            invertWorldAux = MissionManager.instance.invertWorld;
            ShowBookPages();
        }

        if (book.activeSelf && CrossPlatformInputManager.GetButtonDown("Horizontal"))
        {
            // barulho de folha mexendo
            page1.SetActive(false); page2.SetActive(false);
            pagebonus.SetActive(false);
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
            {
                if (pageShowing < 2 || (pageShowing < 3 && pageQuantity > 5))
                {
                    pageShowing++;
                }
            }
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
            {
                if (pageShowing > 0)
                {
                    pageShowing--;
                }
            }
            ShowBookPages();
        }
    }

    public void ShowBook()
    {
        if(book.activeSelf)
        {
            MissionManager.instance.scenerySounds2.PlayPaper(3);
            if (lastPageSeen) seenAll = true;
            book.SetActive(false);
            MissionManager.instance.paused = false;
            show = false;
        }
        else
        {
            MissionManager.instance.scenerySounds2.PlayPaper(2);
            book.SetActive(true);
            MissionManager.instance.paused = true;
            show = true;
            ShowBookPages();
        }
    }

    private void ShowBookPages()
    {
        if (book.activeSelf && pageQuantity > 0 && MissionManager.instance.invertWorld)
        {
            if (pageShowing == 0)
            {
                if (pageQuantity > 0)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg1;
                }
                if (pageQuantity > 1)
                {
                    page2.SetActive(true);
                    page2.GetComponent<Image>().sprite = pg2;
                }
            }
            else if (pageShowing == 1)
            {
                if (pageQuantity > 2)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg3;
                }
                if (pageQuantity > 3)
                {
                    page2.SetActive(true);
                    page2.GetComponent<Image>().sprite = pg4;
                }
            }
            else if (pageShowing == 2)
            {
                if (pageQuantity > 4)
                {
                    page1.SetActive(true);
                    page1.GetComponent<Image>().sprite = pg5;
                }
                if (pageQuantity > 5)
                {
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
        else
        {
            page1.SetActive(false);
            page2.SetActive(false);
            pagebonus.SetActive(false);
        }
    }

    public static void AddPage()
    {
        pageQuantity++;
    }

    public bool SeenAll()
    {
        return seenAll;
    }

}
