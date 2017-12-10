using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class MiniGameObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool activated = false;
    public bool achievedGoal = false;
    public float timeMax = 5;
    public int counterMax = 20;
    public bool refreshTimeMax = true;
    public float posFlareX = 0, posFlareY = 0;
    private bool otherItem = true;
    float timeLeft;
    int counter;
    GameObject anim, flare;

    void Start()
    {
        anim = GameObject.Find("HUDCanvas").gameObject.transform.Find("AnimMiniGame").gameObject;
        InitImage();
    }

    void Update()
    {
        if (Inventory.GetCurrentItemType() == item && otherItem)
        {
            otherItem = false;
            InitImage();
        }
        else if(Inventory.GetCurrentItemType() != item)
        {
            otherItem = true;
            StopMiniGame();
        }

        if (activated && !achievedGoal && !otherItem && !MissionManager.instance.paused && !MissionManager.instance.blocked)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.X)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                MissionManager.instance.pausedObject = true;
                timeLeft = timeMax;
                anim.SetActive(true);
                flare = MissionManager.instance.AddObject("Flare", "", new Vector3(posFlareX, posFlareY, -0.5f), new Vector3(1, 1, 1));
            }
            else if (Input.GetKeyDown(KeyCode.X) || timeLeft <= 0)
            {
                StopMiniGame();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                counter++;
                if (item == Inventory.InventoryItems.FOSFORO || item == Inventory.InventoryItems.ISQUEIRO)
                {
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160/counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y);
                }
                else if (item == Inventory.InventoryItems.FACA)
                {
                    float rotZ = -90;
                    if ((counter % 2) == 0) rotZ = 90;
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160 / counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y);
                    anim.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0,0,rotZ));
                }
                else if (item == Inventory.InventoryItems.PEDRA)
                {
                    float somaY = -5;
                    if ((counter % 2) == 0) somaY = 5;
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

    private void StopMiniGame()
    {
        MissionManager.instance.pausedObject = false;
        timeLeft = 0;
        if (refreshTimeMax)
        {
            counter = 0;
            InitImage();
        }
        anim.SetActive(false);
        Destroy(flare);
    }

    private void InitImage()
    {
        if (item == Inventory.InventoryItems.FOSFORO)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/fosforo");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        }
        else if (item == Inventory.InventoryItems.ISQUEIRO)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/isqueiro_faisca");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 45f));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 100);
        }
        else if (item == Inventory.InventoryItems.FACA)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/faca");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 50);
        }
        else if (item == Inventory.InventoryItems.PEDRA)
        {
            anim.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/pedra");
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            anim.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 50);
        }

        anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80, anim.GetComponent<RectTransform>().anchoredPosition.y);
    }
}
