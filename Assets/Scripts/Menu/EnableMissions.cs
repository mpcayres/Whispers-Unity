using UnityEngine;

public class EnableMissions : MonoBehaviour{

    void Start()
    {

        // C:\Users\Admin\AppData\LocalLow\DefaultCompany\AlGhaib
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 9 + "*.save")) //last scene = 9
            gameObject.transform.Find("Mission9Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 8 + "*.save")) //last scene = 8
            gameObject.transform.Find("Mission8Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 7 + "*.save")) //last scene = 7
            gameObject.transform.Find("Mission7Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 6 + "*.save")) //last scene = 6
            gameObject.transform.Find("Mission6Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 5 + "*.save")) //last scene = 5
            gameObject.transform.Find("Mission5Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 4 + "*.save")) //last scene = 4
            gameObject.transform.Find("Mission4Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 3 + "*.save")) //last scene = 3
            gameObject.transform.Find("Mission3Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 2 + "*.save")) //last scene = 2
            gameObject.transform.Find("Mission2Button").gameObject.SetActive(false);
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 1 + "*.save")) //last scene = 1
            gameObject.transform.Find("Mission1Button").gameObject.SetActive(false);

    }
    
}