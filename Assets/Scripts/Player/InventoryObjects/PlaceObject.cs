using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool inArea = false;
    
    void Start()
    {

    }
	
	void Update ()
    {
        if (Inventory.GetCurrentItemType() == item && inArea && Input.GetKeyDown(KeyCode.X))
        {
            Inventory.DeleteItem(item);
        }
    }
}
