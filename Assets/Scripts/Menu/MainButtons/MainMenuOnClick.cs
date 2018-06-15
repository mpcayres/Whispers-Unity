using UnityEngine;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class MainMenuOnClick : MonoBehaviour
    {

        public void OnClick()
        {
            GameManager.LoadScene("MainMenu", true);
        }

    }
}