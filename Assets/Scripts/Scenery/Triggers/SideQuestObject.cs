using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowScenery
{
    public class SideQuestObject : MonoBehaviour
    {
        public int numSideQuest = 0;
        private bool triggered = false;

        private void Update()
        {
            if (triggered && CrossPlatformInputManager.GetButtonDown("keyInteract"))
            {
                ExtrasManager.InitSideQuest(numSideQuest);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                triggered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                triggered = false;
            }
        }
    }

}