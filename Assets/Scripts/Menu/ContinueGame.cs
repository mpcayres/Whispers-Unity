using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour {

    public Image black;
    public Animator anim;

    public void OnClick(int save)
    {
        PlayerPrefs.SetInt("Mission", 0);
        PlayerPrefs.SetInt("CurrentSaveNumber", save);
        StartCoroutine(FadingContinue());
    }

    IEnumerator FadingContinue()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        MissionManager.LoadScene(6);
    }
}
