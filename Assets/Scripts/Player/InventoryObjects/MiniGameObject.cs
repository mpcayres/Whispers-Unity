using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool activated = false;
    public bool achievedGoal = false;
    public float timeMax = 5;
    public int counterMax = 20;
    public bool refreshTimeMax = true;
    public float posFlareX = 0, posFlareY = 0;
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
        if (activated && !achievedGoal && Inventory.GetCurrentItemType() == item && !MissionManager.instance.paused && !MissionManager.instance.blocked)
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
                if (item == Inventory.InventoryItems.FOSFORO)
                {
                    flare = MissionManager.instance.AddObject("Flare", "", new Vector3(posFlareX, posFlareY, -0.5f), new Vector3(1, 1, 1));
                }
            }
            else if (Input.GetKeyDown(KeyCode.X) || timeLeft <= 0)
            {
                MissionManager.instance.pausedObject = false;
                timeLeft = 0;
                if (refreshTimeMax)
                {
                    counter = 0;
                    InitImage();
                }
                anim.SetActive(false);
                if (item == Inventory.InventoryItems.FOSFORO)
                {
                    Destroy(flare);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                counter++;
                if (item == Inventory.InventoryItems.FOSFORO)
                {
                    anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(anim.GetComponent<RectTransform>().anchoredPosition.x - 160/counterMax, anim.GetComponent<RectTransform>().anchoredPosition.y);
                }
            }

            if(counter >= counterMax)
            {
                achievedGoal = true;
                timeLeft = counter = 0;
                MissionManager.instance.pausedObject = false;
                anim.SetActive(false);
                if (item == Inventory.InventoryItems.FOSFORO)
                {
                    Destroy(flare);
                }
            }
        }

    }

    private void InitImage()
    {
        if (item == Inventory.InventoryItems.FOSFORO)
        {
            anim.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            anim.GetComponent<RectTransform>().anchoredPosition = new Vector3(80, anim.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }
}
