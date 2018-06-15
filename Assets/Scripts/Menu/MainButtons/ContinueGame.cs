using UnityEngine;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class ContinueGame : MonoBehaviour
    {

        public void OnClick(int save)
        {
            PlayerPrefs.SetInt("Mission", 0);
            PlayerPrefs.SetInt("CurrentSaveNumber", save);
            GameManager.LoadScene("QuartoKid", true);
        }

    }
}