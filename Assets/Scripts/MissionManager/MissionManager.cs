using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    public Mission mission;
    public string previousSceneName, currentSceneName;
    public bool paused = false;

    //float startMissionDelay = 3f;

    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            currentSceneName = SceneManager.GetActiveScene().name;
            previousSceneName = currentSceneName;
            //print("NEWMM: " + previousSceneName);
            mission = new Mission1();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        previousSceneName = currentSceneName;
        currentSceneName = scene.name;
        print("OLDSCENE" + previousSceneName);
        print("NEWSCENE" + currentSceneName);
        GetComponent<Player>().ChangePosition();
        mission.LoadMissionScene();
    }

    public void AddObject(string name, Vector3 position, Vector3 scale)
    {
        GameObject moveInstance =
            Instantiate(Resources.Load("Prefab/" + name),
            position, Quaternion.identity) as GameObject;
        moveInstance.transform.localScale = scale;
    }
}
