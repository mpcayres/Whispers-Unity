using System.Collections;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [System.Serializable]
    public enum InventoryItems { DEFAULT, FLASHLIGHT, VELA, FOSFORO, FACA, TAMPA, PEDRA, RACAO, ISQUEIRO, LIVRO };
    [System.Serializable]
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

    GameObject menu, slotsPanel, imagesPanel;
    Sprite box, selectedBox;
    static GameObject menuItem;
    MissionManager missionManager;

    void Start ()
    {
        listItems = new List<DataItems>();
        menu = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu").gameObject;
        menu.SetActive(false);
        menuItem = GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject;
        slotsPanel = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu/SlotsPanel").gameObject;
        imagesPanel = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu/ImagesPanel").gameObject;
        box = Resources.Load<Sprite>("Sprites/UI/box");
        selectedBox = Resources.Load<Sprite>("Sprites/UI/box-select");
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
        // Adiciona todos os objetos, para testar
        NewItem(InventoryItems.RACAO);
        NewItem(InventoryItems.TAMPA);
        NewItem(InventoryItems.FACA);
        NewItem(InventoryItems.PEDRA);
        NewItem(InventoryItems.FOSFORO);
        NewItem(InventoryItems.ISQUEIRO);
        NewItem(InventoryItems.FLASHLIGHT);
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I) && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            ShowInventoryMenu();
        }

        if (menu.activeSelf && currentItem != -1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && currentItem < listItems.Count - 1)
            {
                currentItem++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentItem > 0)
            {
                currentItem--;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentItem < listItems.Count - 4)
            {
                currentItem += 4;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && currentItem > 3)
            {
                currentItem -= 4;
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

    public static void SetCurrentItem(int pos)
    {
        currentItem = pos;
        if (!menuItem.activeSelf) menuItem.SetActive(true);
        menuItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/" + listItems[pos].file);
        menuItem.GetComponent<Image>().preserveAspect = true;
    }

    public static void SetInventory(List<DataItems> inv)
    {
        listItems = inv;
        if (HasItemType(InventoryItems.FLASHLIGHT))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Flashlight").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.VELA))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Vela").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.FOSFORO))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Fosforo").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.FACA))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Faca").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.TAMPA))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Tampa").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.PEDRA))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Pedra").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.RACAO))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Racao").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.ISQUEIRO))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Isqueiro").gameObject.SetActive(true);
        }
        else if (HasItemType(InventoryItems.LIVRO))
        {
            MissionManager.instance.GetComponent<Player>().gameObject.transform.Find("Livro").gameObject.SetActive(true);
        }
    }

    public static void NewItem(InventoryItems selectItem)
    {
        // Não permite ter mais de um mesmo objeto no inventário
        foreach (DataItems i in listItems)
        {
            if (selectItem == i.type)
            {
                return;
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
            file = "livro";
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
