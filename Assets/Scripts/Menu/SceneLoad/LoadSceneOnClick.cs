using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class LoadSceneOnClick : MonoBehaviour
    {

        public Image black;
        public Animator anim;

        public void LoadByIndex(int sceneIndex)
        {
            GameManager.LoadScene(sceneIndex, true); //StartCoroutine(FadingLoad(sceneIndex));
        }

        IEnumerator FadingLoad(int sceneIndex)
        {
            anim.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
            GameManager.LoadScene(sceneIndex, true);
        }
    }
}