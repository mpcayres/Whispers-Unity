using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    public Mission mission;
    public string previousSceneName, currentSceneName;
    public bool paused = false;
    public bool blocked = false;
    public float pathBird, pathCat;
    private GameObject hud, menu, text1, text2;
    private int optionSelected;

    //float startMissionDelay = 3f;

    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            currentSceneName = SceneManager.GetActiveScene().name;
            previousSceneName = currentSceneName;
            hud = GameObject.Find("HUDCanvas").gameObject;
            menu = hud.transform.Find("DecisionMenu").gameObject;
            text1 = menu.transform.Find("Option1").gameObject;
            text2 = menu.transform.Find("Option2").gameObject;
            mission = new Mission1();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        mission.UpdateMission();
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

    public void SetDecision(string opt1, string opt2)
    {
        blocked = true;
        menu.SetActive(true);
        text1.GetComponent<Text>().text = opt1;
        text2.GetComponent<Text>().text = opt2;
        SelectOption(text1, text2);
        optionSelected = 1;
    }

    public int MakeDecision()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (optionSelected == 1)
            {
                SelectOption(text2, text1);
                optionSelected = 2;
            }
            else if (optionSelected == 2)
            {
                SelectOption(text1, text2);
                optionSelected = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            blocked = false;
            menu.SetActive(false);
            return optionSelected;
        }

        return -1;
    }

    private void SelectOption(GameObject textSel, GameObject textNon)
    {
        textSel.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
        textSel.GetComponent<Text>().color = Color.red;
        textNon.GetComponent<Text>().fontStyle = FontStyle.Normal;
        textNon.GetComponent<Text>().color = Color.white;
    }
}
