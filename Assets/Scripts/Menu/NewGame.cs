using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    public Image black;
    public Animator anim;

    public void OnClick()
    {
        StartCoroutine(FadingNewGame());
    }

    IEnumerator FadingNewGame()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        PlayerPrefs.SetInt("Mission", -1);
        MissionManager.LoadScene(6);

    }

}