using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMission : MonoBehaviour{
    public void OnClick()
    {
        string temp = this.gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        int missionNumber = int.Parse(temp);

        PlayerPrefs.SetInt("Mission", missionNumber);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

}
