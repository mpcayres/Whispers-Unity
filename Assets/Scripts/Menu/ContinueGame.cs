using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour {

    public void OnClick()
    {
        PlayerPrefs.SetInt("Mission", 0);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
