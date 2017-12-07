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

       // MissionManager.instance.missionSelected = missionNumber;
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        //MissionManager.instance.missionSelected = missionNumber;
        //MissionManager.instance.LoadGame(missionNumber);
        //MissionManager.instance.SetMission(missionNumber);
        //SceneManager.LoadScene(3);
        //SceneManager.UnloadScene("MainMenu");
        //MissionManager.instance.LoadGame(missionNumber);
        //MissionManager.instance.SetMission(missionNumber);
        //MissionManager.instance.LoadGame(missionNumber);
        //switch (missionNumber)
        //{
        //    case 1:
        //        MissionManager.instance.ChangeMission(1);
        //        break;
        //    case 2:
        //        MissionManager.instance.ChangeMission(2);
        //        break;
        //    case 3:
        //        MissionManager.instance.ChangeMission(3);
        //        break;
        //    case 4:
        //        MissionManager.instance.ChangeMission(4);
        //        break;
        //    case 5:
        //        MissionManager.instance.ChangeMission(5);
        //        break;
        //    case 6:
        //        MissionManager.instance.ChangeMission(6);
        //        break;
        //    case 7:
        //        MissionManager.instance.ChangeMission(7);
        //        break;
        //    case 8:
        //        MissionManager.instance.ChangeMission(8);
        //        break;
        //    case 9:
        //        MissionManager.instance.ChangeMission(9);
        //        break;
        //}
        //MissionManager.instance.missionSelected = missionNumber;
    }

}
