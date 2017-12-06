using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public int life = 15;
    bool enterProtection = false;

    void Start()
    {
        
    }
    
	void Update ()
    {
        if (life <= 0)
        {
            MissionManager.instance.playerProtected = false;
            Inventory.DeleteItem(item);
        }
        else
        {
            if (Inventory.GetCurrentItemType() == item)
            {
                if (!enterProtection)
                {
                    enterProtection = true;
                    MissionManager.instance.playerProtected = true;
                }
            }
            else if (enterProtection)
            {
                enterProtection = false;
                MissionManager.instance.playerProtected = false;
            }
        }
    }

    public void DecreaseLife()
    {
        life--;
    }
}
