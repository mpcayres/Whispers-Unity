using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlaceObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool inArea = false;
    
    void Start()
    {

    }
	
	void Update ()
    {
        if (Inventory.GetCurrentItemType() == item && inArea && CrossPlatformInputManager.GetButtonDown("keyUseObject"))
        {
            Inventory.DeleteItem(item);
        }
    }
}
