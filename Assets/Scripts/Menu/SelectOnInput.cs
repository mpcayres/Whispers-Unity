using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace CrowShadowMenu
{
    public class SelectOnInput : MonoBehaviour
    {

        public EventSystem eventSystem;
        public GameObject selectedObject;

        private bool buttonSelected;
        
        void Update()
        {
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
            {
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
            }
        }

        private void OnDisable()
        {
            buttonSelected = false;
        }
    }
}