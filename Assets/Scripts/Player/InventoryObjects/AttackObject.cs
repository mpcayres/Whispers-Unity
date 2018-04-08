using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AttackObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public static float timeAttack = 1f;
    public bool attacking = false;

    Player player;
    float timeLeftAttack = 0;

    void Start()
    {
        player = GetComponentInParent<Player>();
        attacking = false;
    }

    void Update()
    {
        if (timeLeftAttack > 0)
        {
            timeLeftAttack -= Time.deltaTime;
        }
        else if (CrossPlatformInputManager.GetButtonDown("keyUseObject") && Inventory.GetCurrentItemType() == item &&
            !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            timeLeftAttack = timeAttack;
            attacking = true;
            // animação + som
        }
        else if (timeLeftAttack <= 0)
        {
            attacking = false;
        }

        switch (player.direction)
        {
            case 0:
                GetComponent<Collider2D>().offset = new Vector2(1, 0);
                break;
            case 1:
                GetComponent<Collider2D>().offset = new Vector2(-1, 0);
                break;
            case 2:
                GetComponent<Collider2D>().offset = new Vector2(0, 1);
                break;
            case 3:
                GetComponent<Collider2D>().offset = new Vector2(0, -1);
                break;
            default:
                break;
        }

    }

}
