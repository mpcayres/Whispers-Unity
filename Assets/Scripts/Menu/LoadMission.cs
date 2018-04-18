using UnityEngine;

public class LoadMission : MonoBehaviour{

    public void OnClick()
    {
        GameObject menu = GameObject.Find("MenuCanvas").gameObject;
        GameObject load = PreferencesManager.FindDeepChild(menu.transform, "LoadGamePanel").gameObject;
        GameObject loadSub = PreferencesManager.FindDeepChild(menu.transform, "LoadGameSubPanel").gameObject;

        load.SetActive(!load.activeSelf);
        loadSub.SetActive(!loadSub.activeSelf);

        string temp = gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        int missionNumber = 0;
        if (temp.Equals("End"))
        {
            missionNumber = 11;
        }
        else if (temp.Equals("?"))
        {
            missionNumber = 12;
        }
        else
        {
            missionNumber = int.Parse(temp);
        }
        menu.GetComponent<PreferencesManager>().SetSaveMenu(missionNumber);
    }

    public void OnSaveClick(int m, int save)
    {
        PlayerPrefs.SetInt("Mission", m);
        PlayerPrefs.SetInt("CurrentSaveNumber", save);
        MissionManager.LoadScene(6);
    }

}
