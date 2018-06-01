using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadLastSceneOnClick : MonoBehaviour
 {

    public Image black;
    public Animator anim;

    public void LoadLastScene()
    {
        StartCoroutine(FadingLoad());
    }

    IEnumerator FadingLoad()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        GameManager.instance.ContinueGame();
    }

} 
