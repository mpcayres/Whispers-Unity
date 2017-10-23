using UnityEngine.SceneManagement;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    public Mission mission;
    public Scene currentScene;
    public string previousSceneName;
    public bool paused = false;

    float startMissionDelay = 3f;

    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            print("NEWMM");
            currentScene = SceneManager.GetActiveScene();
            previousSceneName = currentScene.name;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int index)
    {
        previousSceneName = currentScene.name;
        currentScene = SceneManager.GetSceneByBuildIndex(index);
        //print("OLDSCENE" + previousSceneName);
        //print("NEWSCENE" + currentScene.name);
        GetComponent<Player>().ChangePosition();
    }
}
