using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

public class PickUpObject : MonoBehaviour {
    public bool colliding = false;
    public Inventory.InventoryItems item;
    public bool isUp = false;
    Player player;

    void Start ()
    {
        if (Inventory.HasItemType(item))
        {
            if (!((item == Inventory.InventoryItems.PEDRA && Inventory.pedraCount > 0) ||
                (item == Inventory.InventoryItems.PAPEL && Inventory.papelCount > 0)))
            {
                Destroy(gameObject);
            }
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	void Update ()
    {
        if ((!isUp && (player.playerAction == Player.Actions.DEFAULT)) || (isUp && (player.playerAction == Player.Actions.ON_OBJECT)))
        {
            if (colliding && CrossPlatformInputManager.GetButtonDown("keyInteract") &&
                !GameManager.instance.paused && !GameManager.instance.blocked && !GameManager.instance.pausedObject)
            {
                Inventory.NewItem(item);
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        colliding = true;
    }
}
