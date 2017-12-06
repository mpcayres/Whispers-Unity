using UnityEngine;

public class Flashlight : MonoBehaviour {
    Player player;
    float rotationSpeed = 1f;
	static bool enable;
    void Start ()
    {
        player = GetComponentInParent<Player>();
        GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
    }
	
	void Update ()
    {
        //0 = east, 1 = west, 2 = north, 3 = south
        if (Input.GetKeyDown(KeyCode.X) && Inventory.GetCurrentItemType() == Inventory.InventoryItems.FLASHLIGHT &&
            !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
            enable = GetComponent<Light>().enabled;
            if (enable)
            {
                transform.rotation = Quaternion.Euler((float)0.0, (float)0.0, (float)0.0);
            }
        }
		
        if (enable)
        {
            if (Inventory.GetCurrentItemType() != Inventory.InventoryItems.FLASHLIGHT)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
                enable = GetComponent<Light>().enabled;
            }
            switch (player.direction)
            {
                case 0:
                    Quaternion targetRotationE = Quaternion.Euler((float)180.0, (float)230.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationE, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2((float)-2, 0);
                    break;
                case 1:
                    Quaternion targetRotationW = Quaternion.Euler((float)180.0, (float)130.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationW, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2((float)2, 0);
                    break;
                case 2:
                    Quaternion targetRotationN = Quaternion.Euler((float)-45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationN, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, (float)2);
                    break;
                case 3:
                    Quaternion targetRotationS = Quaternion.Euler((float)45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationS, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, (float)-2);
                    break;
                default:
                    break;
            }
        }

    }

	public static bool GetState(){
		return enable;
	}

    public void EnableFlashlight(bool e)
    {
        GetComponent<Light>().enabled = e;
        GetComponent<Collider2D>().enabled = e;
        enable = e;
    }
}
