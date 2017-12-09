using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if ((!isUp && (player.playerState == Player.Actions.DEFAULT)) || (isUp && (player.playerState == Player.Actions.ON_OBJECT)))
        {
            if (colliding && Input.GetKeyDown(KeyCode.Z) &&
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
        if (colliding && Input.GetKeyDown(KeyCode.Z) &&
           !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            Inventory.NewItem(item);
            Destroy(gameObject);
        }
    }
}
