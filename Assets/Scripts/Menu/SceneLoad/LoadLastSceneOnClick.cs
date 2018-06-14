using UnityEngine;
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