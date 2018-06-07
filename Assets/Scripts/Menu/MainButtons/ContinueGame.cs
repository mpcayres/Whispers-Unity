using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class ContinueGame : MonoBehaviour
    {

        public Image black;
        public Animator anim;

        public void OnClick(int save)
        {
            PlayerPrefs.SetInt("Mission", 0);
            PlayerPrefs.SetInt("CurrentSaveNumber", save);
            GameManager.LoadScene(6, true); //StartCoroutine(FadingContinue());
        }

        IEnumerator FadingContinue()
        {
            anim.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
            GameManager.LoadScene(6, true);
        }
    }
}