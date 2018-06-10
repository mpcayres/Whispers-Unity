using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class NewGame : MonoBehaviour
    {

        public void OnClick()
        {
            PlayerPrefs.SetInt("Mission", -1);
            GameManager.LoadScene("QuartoKid", true);
        }

    }
}