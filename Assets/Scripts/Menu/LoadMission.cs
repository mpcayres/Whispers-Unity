using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class LoadMission : MonoBehaviour{
    public int missionNumber;

    public void GetMissionNumber()
    {
        string temp = this.gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        missionNumber = int.Parse(temp);
        MissionManager.instance.LoadGame(missionNumber);
    }

}
