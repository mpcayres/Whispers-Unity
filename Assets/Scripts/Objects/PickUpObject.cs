using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    public bool colliding = false;
    public Inventory.InventoryItems item;

    void Start ()
    {
        if (Inventory.HasItemType(item))
        {
            Destroy(gameObject);
        }
    }
	
	void Update ()
    {
        if (colliding && Input.GetKeyDown(KeyCode.Z) && !MissionManager.instance.paused)
        {
            Inventory.NewItem(item);
            Destroy(gameObject);
        }
	}
}
