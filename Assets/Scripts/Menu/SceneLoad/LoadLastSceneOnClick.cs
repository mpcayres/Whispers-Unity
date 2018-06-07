using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class LoadLastSceneOnClick : MonoBehaviour
    {

        public Image black;
        public Animator anim;

        public void LoadLastScene()
        {
            GameManager.instance.ContinueGame(); //StartCoroutine(FadingLoad());
        }

        IEnumerator FadingLoad()
        {
            anim.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
            GameManager.instance.ContinueGame();
        }

    }
}