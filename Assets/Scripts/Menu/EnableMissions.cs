using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class EnableMissions : MonoBehaviour{
    //public int missionNumber;
    void Start()
    {
 
        if(!File.Exists(Application.persistentDataPath + "/gamesave" + 10 + ".save")) //last scene = 10
            this.gameObject.transform.Find("Mission10Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 9 + ".save")) //last scene = 9
            this.gameObject.transform.Find("Mission9Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 8 + ".save")) //last scene = 8
            this.gameObject.transform.Find("Mission8Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 7 + ".save")) //last scene = 7
            this.gameObject.transform.Find("Mission7Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 6 + ".save")) //last scene = 6
            this.gameObject.transform.Find("Mission6Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 5 + ".save")) //last scene = 5
            this.gameObject.transform.Find("Mission5Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 4 + ".save")) //last scene = 4
            this.gameObject.transform.Find("Mission4Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 3 + ".save")) //last scene = 3
            this.gameObject.transform.Find("Mission3Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 2 + ".save")) //last scene = 2
            this.gameObject.transform.Find("Mission2Button").gameObject.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/gamesave" + 1 + ".save")) //last scene = 1
            this.gameObject.transform.Find("Mission1Button").gameObject.SetActive(false);


    }

    //void LoadMission()
    //{
    //    string temp = this.gameObject.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text;
    //    missionNumber = int.Parse(temp);
    //    MissionManager.instance.LoadGame(missionNumber);
    //}


}