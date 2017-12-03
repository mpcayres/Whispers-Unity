using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    public Mission mission;
    public int missionSelected; // colocar 1 quando for a versão final, para começar na missão 1 quando clicar em new game
    public string previousSceneName, currentSceneName;

    public bool paused = false;
    public bool blocked = false;

    public static bool initMission = false;
    public static float initX = 0, initY = 0;
    public static int initDir = 0;

    public float pathBird, pathCat;
    private GameObject hud;

	public RPGTalk rpgTalk;
    //float startMissionDelay = 3f;

	public int countKidRoomDialog = 0;
	public int countMomRoomDialog = 0;
	public int countLivingroomDialog = 0;
	public int countKitchenDialog = 0;
	public int countGardenDialog = 0;
	public int countCorridorDialog = 0;



    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            currentSceneName = SceneManager.GetActiveScene().name;
            previousSceneName = currentSceneName;
            hud = GameObject.Find("HUDCanvas").gameObject;
            SetMission(missionSelected);
			rpgTalk.OnMadeChoice += OnMadeChoice;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
		
        if(mission != null) mission.UpdateMission();

		if(Input.GetKeyDown(KeyCode.End)){
			MissionManager.instance.rpgTalk.EndTalk ();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			MissionManager.instance.rpgTalk.PlayNext ();
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            Destroy(gameObject);
            Destroy(hud);
        }

        // teste, depois colocar pelo menu
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeMission(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeMission(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            LoadGame(0);
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
        if (!initMission) {
            GetComponent<Player>().ChangePosition();
        }
        else {
            GetComponent<Player>().ChangePositionDefault(initX, initY, initDir);
            if (currentSceneName.Equals(mission.sceneInit))
            {
                initMission = false;
                initX = initY = 0;
            }
        }
        if(mission != null) mission.LoadMissionScene();
    }

    public void AddObject(string name, Vector3 position, Vector3 scale)
    {
        GameObject moveInstance =
            Instantiate(Resources.Load("Prefab/" + name),
            position, Quaternion.identity) as GameObject;
        moveInstance.transform.localScale = scale;
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        
        save.inventory = Inventory.GetInventory();
        save.mission = missionSelected;
        save.currentItem = Inventory.GetCurrentItem();
        save.pathBird = pathBird;
        save.pathCat = pathCat;

        return save;
    }

    public void SaveGame(int m)
    {
        Save save = CreateSaveGameObject();
        
        BinaryFormatter bf = new BinaryFormatter();
        // m = 0 -> continue
        // m > 0 -> select mission
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave" + m + ".save");
        bf.Serialize(file, save);
        file.Close();
        
        Debug.Log("Game Saved " + m);
    }

    public void LoadGame(int m)
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave" + m + ".save"))
        {
            paused = true;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave" + m + ".save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            SetMission(save.mission);
            Inventory.SetInventory(save.inventory);
            if(save.currentItem != -1) Inventory.SetCurrentItem(save.currentItem);
            pathBird = save.pathBird;
            pathCat = save.pathCat;

            Debug.Log("Game Loaded " + m);

            paused = false;
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SetMission(int m)
    {
        missionSelected = m;
        if (missionSelected == 1)
        {
            mission = new Mission1();
        }
        else if (missionSelected == 2)
        {
            mission = new Mission2();
        }
        if (mission != null) mission.InitMission();
    }

    public void ChangeMission(int m)
    {
		countKidRoomDialog = 0;
		countMomRoomDialog = 0;
		countLivingroomDialog = 0;
		countKitchenDialog = 0;
		countGardenDialog = 0;
		countCorridorDialog = 0;

        SetMission(m);
        SaveGame(0);
        SaveGame(missionSelected);
    }
		

	public void AddCountKidRoomDialog(){
		countKidRoomDialog++;
	}
	public void AddCountMomRoomDialog(){
		countMomRoomDialog++;
	}
	public void AddCountKitchenDialog(){
		countKitchenDialog++;
	}
	public void AddCountGardenDialog(){
		countGardenDialog++;
	}
	public void AddCountCorridorDialog(){
		countCorridorDialog++;
	}
	public void AddCountLivingroomDialog(){
		countCorridorDialog++;
	}
		
	/*
	public void OnNewTalk(){
		MissionManager.instance.paused = true;
		MissionManager.instance.blocked = true;
	}
	public void OnEndTalk(){
		MissionManager.instance.paused = false;
		MissionManager.instance.blocked = false;
	}*/



	public void OnMadeChoice(int questionId, int choiceID){
		if ( questionId == 0) { // escolha final da missão 1
			if (choiceID == 0) {
				pathBird++;
			} else {
				pathCat++;
			}
		}
		if ( questionId == 1) { // escolha final da missão 2
			if (choiceID == 0) {
				pathBird++;
				rpgTalk.NewTalk ("M2Q1C0", "M2Q1C0End", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
			} else {
				pathCat++;
				rpgTalk.NewTalk ("M2Q1C1", "M2Q1C1End", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
			}
		}
	}
    
}
