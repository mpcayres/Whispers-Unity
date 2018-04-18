using UnityEngine;

public class EnableMissions : MonoBehaviour{

    void Start()
    {

        // C:\Users\Admin\AppData\LocalLow\DefaultCompany\AlGhaib
        for (int i = 1; i <= 12; i++)
        {
            if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + i + "_" + "*.save"))
            {
                gameObject.transform.Find("Mission" + i + "Button").gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.Find("Mission" + i + "Button").gameObject.SetActive(true);
            }
        }

    }
    
}