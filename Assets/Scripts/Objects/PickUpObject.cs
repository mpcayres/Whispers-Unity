using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowObjects
{
    public class PickUpObject : MonoBehaviour
    {
        public bool colliding = false;
        public Inventory.InventoryItems item;
        public bool isUp = false;
        public int addTrowable = 1;

        Player player;

        void Start()
        {
            if (Inventory.HasItemType(item))
            {
                if (!((item == Inventory.InventoryItems.PEDRA && Inventory.pedraCount > 0) ||
                    (item == Inventory.InventoryItems.PAPEL && Inventory.papelCount > 0)))
                {
                    Destroy(gameObject);
                }
            }
            player = GameManager.instance.gameObject.GetComponent<Player>();
        }

        void Update()
        {
            if ((!isUp && (player.playerAction == Player.Actions.DEFAULT)) || (isUp && (player.playerAction == Player.Actions.ON_OBJECT)))
            {
                if (colliding && CrossPlatformInputManager.GetButtonDown("keyInteract") &&
                    !GameManager.instance.paused && !GameManager.instance.blocked && !GameManager.instance.pausedObject)
                {
                    for (int i = 0; i != addTrowable; i++)
                    {
                        Inventory.NewItem(item);
                    }
                    Destroy(gameObject);
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag.Equals("Player"))
                colliding = true;
        }
    }
}