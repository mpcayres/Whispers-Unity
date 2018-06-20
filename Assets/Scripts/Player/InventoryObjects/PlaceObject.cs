using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CrowShadowPlayer
{
    public class PlaceObject : MonoBehaviour
    {
        public Inventory.InventoryItems item;
        public bool inArea = false;

        void Update()
        {
            if (Inventory.GetCurrentItemType() == item && inArea && CrossPlatformInputManager.GetButtonDown("keyUseObject"))
            {
                Inventory.DeleteItem(item);
            }
        }
    }
}