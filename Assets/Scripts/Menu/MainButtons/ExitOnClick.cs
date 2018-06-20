using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class ExitOnClick : MonoBehaviour
    {

        public Image black;
        public Animator anim;

        public void Quit()
        {
            StartCoroutine(FadingExit());
        }

        IEnumerator FadingExit()
        {
            float fadeTime = FadingScene.instance.BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }

    }
}