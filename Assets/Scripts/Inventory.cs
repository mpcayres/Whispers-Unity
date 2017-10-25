using System.Collections;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public enum InventoryItems { DEFAULT, FLASHLIGHT };
    public class DataItems
    {
        public InventoryItems item;
        public string file;

        public DataItems(InventoryItems item, string file)
        {
            this.item = item;
            this.file = file;
        }
    }
    public static DataItems selectedItem;
    private static List<DataItems> listItems;
    GameObject menu;
    static GameObject menuItem;
    MissionManager missionManager;

    void Start ()
    {
        listItems = new List<DataItems>();
        selectedItem = new DataItems(InventoryItems.DEFAULT, "");
        menu = GameObject.Find("HUDCanvas").transform.Find("InventoryMenu").gameObject;
        menu.SetActive(false);
        menuItem = GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject;
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventoryMenu();
        }
	}

    void ShowInventoryMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            missionManager.paused = false;
        }
        else
        {
            menu.SetActive(true);
            missionManager.paused = true;
        }
        //Resources.Load("Sprites/Objects/Inventory" + name)
    }

    static void SetItem(DataItems selectItem)
    {
        selectedItem = selectItem;
        if (!menuItem.activeSelf) menuItem.SetActive(true);
        menuItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/" + selectedItem.file);
        menuItem.GetComponent<Image>().preserveAspect = true;
    }

    public static void NewItem(InventoryItems selectItem)
    {
        string file = "";
        if (selectItem == InventoryItems.FLASHLIGHT)
        {
            file = "carretel";
        }
        DataItems novoItem = new DataItems(selectItem, file);
        listItems.Add(novoItem);
        SetItem(novoItem);
    }
}
