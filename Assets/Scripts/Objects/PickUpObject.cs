using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    public bool colliding = false;
    public Inventory.InventoryItems item;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (colliding && Input.GetKeyDown(KeyCode.Z))
        {
            Inventory.NewItem(item);
            Destroy(gameObject);
        }
	}
}
