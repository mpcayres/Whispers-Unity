using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    Player player;
    float rotationSpeed = 1f;
	static public bool enable;
    void Start ()
    {
        player = GetComponentInParent<Player>();
        GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
    }
	
	void Update ()
    {
        //0 = east, 1 = west, 2 = north, 3 = south
        if (Input.GetKeyDown(KeyCode.F) && Inventory.GetCurrentItemType() == Inventory.InventoryItems.FLASHLIGHT && !MissionManager.instance.paused)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
            if (GetComponent<Light>().enabled)
            {
                transform.rotation = Quaternion.Euler((float)0.0, (float)0.0, (float)0.0);
            }
        }
		enable = GetComponent<Light> ().enabled;
        if (GetComponent<Light>().enabled)
        {
            if (Inventory.GetCurrentItemType() != Inventory.InventoryItems.FLASHLIGHT)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
            }
            switch (player.direction)
            {
                case 0:
                    Quaternion targetRotationE = Quaternion.Euler((float)180.0, (float)230.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationE, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(-1, 0);
                    break;
                case 1:
                    Quaternion targetRotationW = Quaternion.Euler((float)180.0, (float)130.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationW, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(1, 0);
                    break;
                case 2:
                    Quaternion targetRotationN = Quaternion.Euler((float)-45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationN, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, 1);
                    break;
                case 3:
                    Quaternion targetRotationS = Quaternion.Euler((float)45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationS, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, -1);
                    break;
                default:
                    break;
            }
        }

    }
	public bool GetState(){


		return enable;
	}
}
