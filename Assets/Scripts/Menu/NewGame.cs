using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public void OnClick ()
    {
        PlayerPrefs.SetInt("Mission", -1);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
