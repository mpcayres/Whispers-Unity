using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowPlayer
{
    public class ProtectionObject : MonoBehaviour
    {
        public Inventory.InventoryItems item;
        public int life = 60;

        bool enterProtection = false;
        Player player;

        void Start()
        {
            player = GetComponentInParent<Player>();
        }

        void Update()
        {
            if (life <= 0)
            {
                GameManager.instance.playerProtected = false;
                Inventory.DeleteItem(item);
            }
            else
            {
                if (Inventory.GetCurrentItemType() == item && !GameManager.instance.paused &&
                    !GameManager.instance.blocked && !GameManager.instance.pausedObject &&
                    CrossPlatformInputManager.GetButtonDown("keyUseObject"))
                {
                    EnterProtection(!enterProtection);
                }
                else if (Inventory.GetCurrentItemType() != item && enterProtection)
                {
                    EnterProtection(false);
                }
            }
        }

        private void EnterProtection(bool e = true)
        {
            enterProtection = e;
            GameManager.instance.playerProtected = e;
            // mudar imagem do player
            if (enterProtection)
            {
                if (item == Inventory.InventoryItems.TAMPA)
                {
                    player.ChangeState((int)Player.States.PROTECTED_TAMPA);
                }
                else
                {
                    player.ChangeState((int)Player.States.PROTECTED_ESCUDO);
                }
            }
            else
            {
                player.ChangeState((int)Player.States.DEFAULT);
            }
        }

        public void DecreaseLife()
        {
            life--;
        }
    }
}