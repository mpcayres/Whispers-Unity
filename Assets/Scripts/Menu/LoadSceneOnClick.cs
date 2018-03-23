using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour {

    public Image black;
    public Animator anim;

    public void LoadByIndex(int sceneIndex)
	{
        StartCoroutine(FadingLoad(sceneIndex));
	}

    IEnumerator FadingLoad(int sceneIndex)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        MissionManager.LoadScene(sceneIndex);

    }
}