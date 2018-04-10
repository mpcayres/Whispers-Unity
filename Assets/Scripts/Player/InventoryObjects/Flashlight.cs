using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Flashlight : MonoBehaviour {
    public Inventory.InventoryItems item;

    Player player;
    float rotationSpeed = 1f, timePressed = 0f;
	static bool enable;
    bool changeDirectionTime = false;

    void Start ()
    {
        player = GetComponentInParent<Player>();
        GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
    }
	
	void Update ()
    {
        //0 = east, 1 = west, 2 = north, 3 = south
        if (Inventory.GetCurrentItemType() == item && !MissionManager.instance.paused &&
            !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            if (CrossPlatformInputManager.GetButtonDown("keyUseObject"))
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
                enable = GetComponent<Light>().enabled;
                if (enable)
                {
                    transform.rotation = Quaternion.Euler((float)0.0, (float)0.0, (float)0.0);
                }
                timePressed = 0f;
            }
            else if (CrossPlatformInputManager.GetButton("keyUseObject"))
            {
                if (changeDirectionTime)
                {
                    timePressed -= 4 * Time.deltaTime;
                }
                else
                {
                    timePressed += 4 * Time.deltaTime;
                }

                if (timePressed >= 20f)
                {
                    changeDirectionTime = true;
                }
                else if (timePressed <= 0f)
                {
                    changeDirectionTime = false;
                }

            }
        }
		
        if (enable)
        {
            if (Inventory.GetCurrentItemType() != item)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                GetComponent<Collider2D>().enabled = GetComponent<Light>().enabled;
                enable = GetComponent<Light>().enabled;
            }

            GetComponent<Light>().spotAngle = 60 - timePressed;
            GetComponent<CircleCollider2D>().radius = 1f - (timePressed / 100f);

            switch (player.direction)
            {
                case 0:
                    Quaternion targetRotationE = Quaternion.Euler((float)180.0, (float)230.0 - timePressed, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationE, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(-2f + (timePressed / 25f), 0);
                    break;
                case 1:
                    Quaternion targetRotationW = Quaternion.Euler((float)180.0, (float)130.0 + timePressed, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationW, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(2f - (timePressed / 25f), 0);
                    break;
                case 2:
                    Quaternion targetRotationN = Quaternion.Euler((float)-45.0 + (timePressed / 2f), (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationN, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, 2f - (timePressed / 25f));
                    break;
                case 3:
                    Quaternion targetRotationS = Quaternion.Euler((float)45.0 - (timePressed / 2f), (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationS, rotationSpeed * Time.deltaTime);
                    GetComponent<Collider2D>().offset = new Vector2(0, -2f + (timePressed / 25f));
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
