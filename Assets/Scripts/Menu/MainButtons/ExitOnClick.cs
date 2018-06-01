using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
            anim.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif

        }

    }
}