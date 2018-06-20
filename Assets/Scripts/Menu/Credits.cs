using UnityEngine;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class Credits : MonoBehaviour
    {
        public void ChangeScene(int number = 0)
        {
            GameManager.LoadScene("MainMenu", true); // animation finished: load main menu
        }
    }
}
