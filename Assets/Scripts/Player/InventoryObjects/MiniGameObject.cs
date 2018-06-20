using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowPlayer
{
    public class MiniGameObject : MonoBehaviour
    {
        public Inventory.InventoryItems item;
        public bool activated = false;
        public bool achievedGoal = false;
        public float timeMax = 5;
        public int counterMax = 20;
        public bool refreshTimeMax = true;
        public float posFlareX = 0, posFlareY = 0;

        GameObject anim, flare;
        RectTransform animRect;
        Image animImage;

        private bool otherItem = true;
        private bool playing = false;
        private float timeLeft;
        private int counter;

        void Start()
        {
            anim = GameObject.Find("HUDCanvas").gameObject.transform.Find("AnimMiniGame").gameObject;
            animRect = anim.GetComponent<RectTransform>();
            animImage = anim.GetComponent<Image>();
            InitImage();
        }

        public void Update()
        {
            if (Inventory.GetCurrentItemType() == item && otherItem)
            {
                otherItem = false;
                InitImage();
            }
            else if (Inventory.GetCurrentItemType() != item && !otherItem)
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
                        animRect.anchoredPosition = new Vector3(animRect.anchoredPosition.x - 160 / counterMax, animRect.anchoredPosition.y);
                    }
                    else if (item == Inventory.InventoryItems.FACA)
                    {
                        float rotZ = -135;
                        if ((counter % 2) == 0) rotZ = 135;
                        animRect.anchoredPosition = new Vector3(animRect.anchoredPosition.x - 160 / counterMax, animRect.anchoredPosition.y);
                        animRect.localRotation = Quaternion.Euler(new Vector3(180, 0, rotZ));
                    }
                    else if (item == Inventory.InventoryItems.PEDRA)
                    {
                        float somaY = -20;
                        if ((counter % 2) == 0) somaY = 20;
                        animRect.anchoredPosition = new Vector3(animRect.anchoredPosition.x - 160 / counterMax, animRect.anchoredPosition.y + somaY);
                    }
                }

                if (counter >= counterMax)
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
            if (anim != null) anim.SetActive(false);
            if (flare != null) Destroy(flare);
            playing = false;
        }

        private void InitImage()
        {
            //print("INITMINIGAME" + item);
            if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.FOSFORO || Inventory.GetCurrentItemType() == Inventory.InventoryItems.PAPEL)
            {
                animImage.sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/fosforo");
                animRect.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                animRect.sizeDelta = new Vector2(50, 50);
                animRect.anchoredPosition = new Vector3(80, animRect.anchoredPosition.y);
            }
            else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.ISQUEIRO)
            {
                animImage.sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/isqueiro_faisca");
                animRect.rotation = Quaternion.Euler(new Vector3(0, 0, 45f));
                animRect.sizeDelta = new Vector2(50, 100);
                animRect.anchoredPosition = new Vector3(80, animRect.anchoredPosition.y);
            }
            else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.FACA)
            {
                animImage.sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/faca");
                animRect.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
                animRect.sizeDelta = new Vector2(100, 20);
                animRect.anchoredPosition = new Vector3(80 - counter * (160 / counterMax), animRect.anchoredPosition.y);
            }
            else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.PEDRA)
            {
                animImage.sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/pedra");
                animRect.rotation = Quaternion.Euler(new Vector3(0, 0, -20));
                animRect.sizeDelta = new Vector2(60, 40);
                animRect.anchoredPosition = new Vector3(80 - counter * (160 / counterMax), animRect.anchoredPosition.y);
            }

        }
    }
}