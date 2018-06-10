using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class LoadLastSceneOnClick : MonoBehaviour
    {

        public void LoadLastScene()
        {
            GameManager.instance.ContinueGame();
        }

    }
}