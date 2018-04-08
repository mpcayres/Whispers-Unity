using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Inventory : MonoBehaviour {

    [System.Serializable]
    public enum InventoryItems { DEFAULT, FLASHLIGHT, VELA, FOSFORO, FACA, TAMPA, PEDRA, RACAO, ISQUEIRO, LIVRO };
    public class DataItems
    {
        public InventoryItems type;
        public string file;

        public DataItems(InventoryItems type, string file)
        {
            this.type = type;
            this.file = file;
        }
    }
    private static List<DataItems> listItems;
    private static int currentItem = -1;
    private int previousItem = -1;
    public static bool open = false;

    GameObject menu, slotsPanel, imagesPanel;
    Sprite box, selectedBox;
    static GameObject menuItem;
    MissionManager missionManager;

    public AudioClip sound;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void Awake()
    {
        menu = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu").gameObject;
        menu.SetActive(false);
        menuItem = GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject;
        slotsPanel = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu/SlotsPanel").gameObject;
        imagesPanel = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu/ImagesPanel").gameObject;
        box = Resources.Load<Sprite>("Sprites/UI/box");
        selectedBox = Resources.Load<Sprite>("Sprites/UI/box-select");
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
        if (listItems == null) listItems = new List<DataItems>();
    }

    void Start ()
    {
        // Adiciona todos os objetos, para testar
        // DELETAR PARA A VERSÃO FINAL
        /*NewItem(InventoryItems.RACAO);
        NewItem(InventoryItems.TAMPA);
        NewItem(InventoryItems.FACA);
        NewItem(InventoryItems.PEDRA);
        NewItem(InventoryItems.FOSFORO);
        NewItem(InventoryItems.ISQUEIRO);
        NewItem(InventoryItems.FLASHLIGHT);*/

        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }
	
	void Update ()
    {
        if (CrossPlatformInputManager.GetButtonDown("keyInventory") && !MissionManager.instance.showMissionStart &&
            !MissionManager.instance.blocked && !MissionManager.instance.pausedObject && !Book.show)
        {
            if(!source.isPlaying)
                source.PlayOneShot(sound);
            ShowInventoryMenu();
        }

        if (menu.activeSelf && currentItem != -1)
        {
            if(Input.GetButtonDown("Horizontal"))
            {
                if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0 && currentItem < listItems.Count - 1)
                {
                    currentItem++;
                }
                else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0 && currentItem > 0)
                {
                    currentItem--;
                }
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0 && currentItem < listItems.Count - 4)
                {
                    currentItem += 4;
                }
                else if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0 && currentItem > 3)
                {
                    currentItem -= 4;
                }
            }

            if (previousItem != currentItem)
            {
                GameObject previousSlot = slotsPanel.transform.Find("Slot (" + previousItem + ")").gameObject;
                previousSlot.GetComponent<Image>().sprite = box;
                GameObject currentSlot = slotsPanel.transform.Find("Slot (" + currentItem + ")").gameObject;
                currentSlot.GetComponent<Image>().sprite = selectedBox;
                previousItem = currentItem;
            }
        }
    }

    void ShowInventoryMenu()
    {
        if (menu.activeSelf)
        {
            open = false;
            menu.SetActive(false);
            missionManager.paused = false;
            if (currentItem != -1)
            {
                GameObject currentSlot = slotsPanel.transform.Find("Slot (" + currentItem + ")").gameObject;
                currentSlot.GetComponent<Image>().sprite = box;
                previousItem = currentItem;
                SetCurrentItem(currentItem);
            }
        }
        else
        {
            open = true;
            menu.SetActive(true);
            missionManager.paused = true;
            int count = 0;
            foreach (DataItems i in listItems)
            {
                GameObject slot = imagesPanel.transform.Find("Slot (" + count + ")").gameObject;
                slot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/" + i.file);
                slot.GetComponent<Image>().preserveAspect = true;
                slot.SetActive(true);
                count++;
            }
            for ( ; count < 16; count++)
            {
                GameObject slot = imagesPanel.transform.Find("Slot (" + count + ")").gameObject;
                slot.SetActive(false);
            }
            if (currentItem != -1) {
                GameObject currentSlot = slotsPanel.transform.Find("Slot (" + currentItem + ")").gameObject;
                currentSlot.GetComponent<Image>().sprite = selectedBox;
                previousItem = currentItem;
            }
        }
        
    }

    public static int GetCurrentItem()
    {
        return currentItem;
    }

    public static InventoryItems GetCurrentItemType()
    {
        if (currentItem == -1) return InventoryItems.DEFAULT;
        return listItems[currentItem].type;
    }

    public static List<DataItems> GetInventory()
    {
        return listItems;
    }

    public static List<InventoryItems> GetInventoryItems()
    {
        List<InventoryItems> list = new List<InventoryItems>();

        if (listItems != null) {
            for (int i = 0; i < listItems.Count; i++)
            {
                list.Add(listItems[i].type);
            }
        }

        return list;
    }

    public static void SetCurrentItem(int pos)
    {
        currentItem = pos;
        if (pos != -1)
        {
            if(menuItem == null) menuItem = GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject;
            if (!menuItem.activeSelf && !(MissionManager.instance.mission is Mission9)) menuItem.SetActive(true);
            menuItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/" + listItems[pos].file);
            menuItem.GetComponent<Image>().preserveAspect = true;
        }
    }

    public static void SetInventory(List<InventoryItems> invItems)
    {
        listItems = new List<DataItems>();
        if (invItems != null) {
            for (int i = 0; i < invItems.Count; i++)
            {
                NewItem(invItems[i]);
            }
        }
        else
        {
            currentItem = -1;
        }
    }

    public static void NewItem(InventoryItems selectItem)
    {

        // Não permite ter mais de um mesmo objeto no inventário
        if (listItems == null) listItems = new List<DataItems>();
        if (listItems.Count > 0) {
            foreach (DataItems i in listItems)
            {
                if (selectItem == i.type)
                {
                    return;
                }
            }
        }

        string file = "";
        if (selectItem == InventoryItems.FLASHLIGHT)
        {
            file = "lanterna";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Flashlight").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.VELA)
        {
            file = "vela";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Vela").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.FOSFORO)
        {
            file = "caixa_fosforo_maior";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Fosforo").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.FACA)
        {
            file = "faca";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Faca").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.TAMPA)
        {
            file = "tampa";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Tampa").gameObject.SetActive(true);
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Tampa").gameObject.GetComponent<ProtectionObject>().life = 80;
        }
        else if (selectItem == InventoryItems.PEDRA)
        {
            file = "pedra";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Pedra").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.RACAO)
        {
            file = "saco_racao";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Racao").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.ISQUEIRO)
        {
            file = "isqueiro";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Isqueiro").gameObject.SetActive(true);
        }
        else if (selectItem == InventoryItems.LIVRO)
        {
            file = "book";
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Livro").gameObject.SetActive(true);
        }

        DataItems novoItem = new DataItems(selectItem, file);
        listItems.Add(novoItem);
        SetCurrentItem(listItems.Count - 1);
    }

    public static void DeleteItem(InventoryItems selectItem)
    {
        int count = 0;
        foreach (DataItems i in listItems)
        {
            if (i.type == selectItem)
            {
                if (selectItem == listItems[currentItem].type)
                {
                    listItems.RemoveAt(count);

                    if (listItems.Count > 0)
                    {
                        SetCurrentItem(0);
                    }
                    else
                    {
                        menuItem.SetActive(false);
                        currentItem = -1;
                    }
                }
                else
                {
                    listItems.RemoveAt(count);
                }
                break;
            }
            count++;
        }

        if (selectItem == InventoryItems.FLASHLIGHT)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Flashlight").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.VELA)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Vela").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.FOSFORO)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Fosforo").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.FACA)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Faca").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.TAMPA)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Tampa").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.PEDRA)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Pedra").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.RACAO)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Racao").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.ISQUEIRO)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Isqueiro").gameObject.SetActive(false);
        }
        else if (selectItem == InventoryItems.ISQUEIRO)
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Livro").gameObject.SetActive(false);
        }

    }

    public static bool HasItemType(InventoryItems item)
    {
       if (listItems != null && listItems.Count > 0)
       {
            foreach (DataItems i in listItems)
            {
                if (item == i.type)
                {
                    return true;
                }
            }
       }
       
        return false;
    }

}
