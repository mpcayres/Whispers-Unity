using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PickUpObject : MonoBehaviour {
    public bool colliding = false;
    public Inventory.InventoryItems item;
    public bool isUp = false;
    Player player;

    void Start ()
    {
        if (Inventory.HasItemType(item))
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	void Update ()
    {
        if ((!isUp && (player.playerAction == Player.Actions.DEFAULT)) || (isUp && (player.playerAction == Player.Actions.ON_OBJECT)))
        {
            if (colliding && CrossPlatformInputManager.GetButtonDown("keyInteract") &&
                !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
            {
                Inventory.NewItem(item);
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        colliding = true;
        if (colliding && CrossPlatformInputManager.GetButtonDown("keyInteract") &&
           !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            Inventory.NewItem(item);
            Destroy(this.gameObject);
        }
    }
}
