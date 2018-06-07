using UnityEngine;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class Credits : MonoBehaviour
    {
        public void ChangeScene(int number = 0)
        {
            GameManager.LoadScene(0, true); // animation finished: load main menu
        }
    }
}
