using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class NewGame : MonoBehaviour
    {

        public Image black;
        public Animator anim;

        public void OnClick()
        {
            GameManager.LoadScene(6, true); //StartCoroutine(FadingNewGame());
        }

        IEnumerator FadingNewGame()
        {
            anim.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
            PlayerPrefs.SetInt("Mission", -1);
            GameManager.LoadScene(6, true);
        }

    }
}