using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityStandardAssets.CrossPlatformInput;

public class MiniGameObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool activated = false;
    public bool achievedGoal = false;
    public float timeMax = 5;
    public int counterMax = 20;
    public bool refreshTimeMax = true;
    public float posFlareX = 0, posFlareY = 0;

    private bool otherItem = true;
    private bool playing = false;
    private float timeLeft;
    private int counter;

    GameObject anim, flare;

    void Start()
    {
        anim = GameObject.Find("HUDCanvas").gameObject.transform.Find("AnimMiniGame").gameObject;
        InitImage();
    }

    public void Update()
    {
        if (Inventory.GetCurrentItemType() == item && otherItem)
        {
            otherItem = false;
            InitImage();
        }
        else if(Inventory.GetCurrentItemType() != item && !otherItem)
        {
            otherItem = true;
            StopMiniGame();
        }

        if (activated && !achievedGoal && !otherItem && !GameManager.instance.paused && !GameManager.instance.blocked)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            if (CrossPlatformInputManager.GetButtonDown("keyUseObject") && !playing) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                //print("STARTMINIGAME" + item);
                GameManager.instance.pausedObject = true;
                timeLeft = timeMax;
                anim.SetActive(true);
                flare = GameManager.instance.AddObject("Effects/Flare", "", new Vector3(posFlareX, posFlareY, -0.5f), new Vector3(1, 1, 1));
                playing = true;
            }
            else if ((CrossPlatformInputManager.GetButtonDown("keyUseObject") || timeLeft <= 0) && playing)
            {
                StopMiniGame();
            }
            else if (CrossPlatformInputManager.GetButtonDown("keyMiniGame"))
            {
                counter++;
                if (item == Inventory.InventoryItems.FOSFORO || item == Inventory.InventoryItems.PAPEL || item == Inventory.InventoryItems.ISQUEIRO)
                {
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160/counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y);
                }
                else if (item == Inventory.InventoryItems.FACA)
                {
                    float rotZ = -135;
                    if ((counter % 2) == 0) rotZ = 135;
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160 / counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y);
                    anim.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(180, 0, rotZ));
                }
                else if (item == Inventory.InventoryItems.PEDRA)
                {
                    float somaY = -20;
                    if ((counter % 2) == 0) somaY = 20;
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160 / counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y + somaY);
                }
            }

            if(counter >= counterMax)
            {
                achievedGoal = true;
                StopMiniGame();
            }
        }

    }

    public void StopMiniGame()
    {
        //print("STOPMINIMAGE" + item);
        GameManager.instance.pausedObject = false;
        timeLeft = 0;
        if (refreshTimeMax)
        {
            counter = 0;
            InitImage();
        }
        if(anim != null) anim.SetActive(false);
        if(flare != null) Destroy(flare);
        playing = false;
    }

    private void InitImage()
    {
        //print("INITMINIGAME" + item);
        if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.FOSFORO || Inventory.GetCurrentItemType() == Inventory.InventoryItems.PAPEL)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/fosforo");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80, anim.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.ISQUEIRO)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/isqueiro_faisca");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 45f));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 100);
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80, anim.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.FACA)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/faca");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(180, 0, 180));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80 - counter * ( 160 / counterMax), anim.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.PEDRA)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/pedra");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -20));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 40);
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80 - counter * (160 / counterMax), anim.GetComponent<RectTransform>().anchoredPosition.y);
        }
        
    }
}
