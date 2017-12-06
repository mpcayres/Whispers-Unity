using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    public Mission mission;
    public int missionSelected; // colocar 1 quando for a versão final, para começar na missão 1 quando clicar em new game
    public string previousSceneName, currentSceneName;
    public bool mission2ContestaMae = false;

    public bool paused = false;
    public bool pausedObject = false;
    public bool blocked = false;

    public static bool initMission = false;
    public static float initX = 0, initY = 0;
    public static int initDir = 0;

    public float pathBird, pathCat;
    public bool invertWorld = false;

    private GameObject hud;
    float startMissionDelay = 3f;
    bool showMissionStart = true;
    private Text levelText;
    private GameObject levelImage;
    public RPGTalk rpgTalk;

    public int countKidRoomDialog = -1;
	public int countMomRoomDialog = -1;
	public int countLivingroomDialog = -1;
	public int countKitchenDialog = -1;
	public int countGardenDialog = -1;
	public int countCorridorDialog = -1;


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
            Invoke("HideLevelImage", startMissionDelay);

            rpgTalk.OnMadeChoice += OnMadeChoice;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (!showMissionStart)
        {

            if (mission != null)
            {
                mission.UpdateMission();
            }

            if (Input.GetKeyDown(KeyCode.End))
            {
                rpgTalk.EndTalk();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rpgTalk.PlayNext();
            }

            if (!blocked && !paused && Input.GetKeyDown(KeyCode.E))
            {
                InvertWorld(!invertWorld);
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HideLevelImage();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
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
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			ChangeMission(3);
		}
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			ChangeMission(4);
		}
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			ChangeMission(4);
		}
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			ChangeMission(5);
		}
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			ChangeMission(6);
		}
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			ChangeMission(7);
		}
        if (Input.GetKeyDown(KeyCode.Keypad8))
		{
			ChangeMission(8);
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
                countKidRoomDialog = 0;
                countMomRoomDialog = 0;
                countLivingroomDialog = 0;
                countKitchenDialog = 0;
                countGardenDialog = 0;
                countCorridorDialog = 0;
            }
        }

        if (currentSceneName.Equals("GameOver"))
        {
            GetComponent<Player>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
        else if (previousSceneName.Equals("GameOver"))
        {
            GetComponent<Player>().enabled = true;
            GetComponent<Renderer>().enabled = true;
        }
        else if (currentSceneName.Equals("MainMenu"))
        {
            Destroy(gameObject);
            Destroy(hud);
        }

        InvertWorld(invertWorld);

        if(mission != null) mission.LoadMissionScene();
    }

    public void AddObject(string name, string sprite, Vector3 position, Vector3 scale)
    {
        GameObject moveInstance =
            Instantiate(Resources.Load("Prefab/" + name),
            position, Quaternion.identity) as GameObject;
        if(sprite != "") moveInstance.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
        moveInstance.transform.localScale = scale;
    }

    public void InvertWorld(bool sel)
    {
        invertWorld = sel;
        if (!currentSceneName.Equals("GameOver") && !currentSceneName.Equals("MainMenu"))
        {
            GameObject.Find("MainCamera").GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionLookup>().enabled = invertWorld;
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        
        save.inventory = Inventory.GetInventory();
        save.mission = missionSelected;
        save.currentItem = Inventory.GetCurrentItem();
        save.pathBird = pathBird;
        save.pathCat = pathCat;
        save.mission2ContestaMae = mission2ContestaMae;

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
            mission2ContestaMae = save.mission2ContestaMae;

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

		switch(missionSelected){
		case 1:
			mission = new Mission1();
			break;
		case 2:
			mission = new Mission2();
			break;
		case 3:
			mission = new Mission3();
			break;
		case 4:
			mission = new Mission4();
			break;
		case 5:
			mission = new Mission5();
			break;
		case 6:
			mission = new Mission6();
			break;
		case 7:
			mission = new Mission7();
			break;
		case 8:
			mission = new Mission8();
			break;

		}
        
        levelImage = hud.transform.Find("LevelImage").gameObject;
        levelText = levelImage.transform.Find("LevelText").GetComponent<Text>();

        if (mission != null)
        {
            levelText.text = "Night " + m;
            levelImage.SetActive(true);
            showMissionStart = true;
            mission.InitMission();
        }
        else {
            showMissionStart = false;
        }
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

    public void GameOver()
    {
        blocked = true;
        hud.SetActive(false);
        InvertWorld(false);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(previousSceneName, LoadSceneMode.Single);
        blocked = false;
        hud.SetActive(true);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        showMissionStart = false;
    }

    public bool GetMissionStart()
    {
        return showMissionStart;
    }

    public void InvokeMission()
    {
        print("INVOKEMISSION");
        mission.InvokeMission();
    }

    public void AddCountKidRoomDialog()
    {
		countKidRoomDialog++;
	}

	public void AddCountMomRoomDialog()
    {
		countMomRoomDialog++;
	}

	public void AddCountKitchenDialog()
    {
		countKitchenDialog++;
	}

	public void AddCountGardenDialog()
    {
		countGardenDialog++;
	}

	public void AddCountCorridorDialog()
    {
		countCorridorDialog++;
	}

	public void AddCountLivingroomDialog()
    {
		countCorridorDialog++;
	}
		
	/*
	public void OnNewTalk(){
		paused = true;
		blocked = true;
	}
	public void OnEndTalk(){
		paused = false;
		blocked = false;
	}*/



	public void OnMadeChoice(int questionId, int choiceID)
    {
		if ( questionId == 0) { // escolha final da missão 1
			if (choiceID == 0) {
				pathCat -= 2;
                rpgTalk.NewTalk ("M1Q0C0", "M1Q0C0End", rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
			} else {
				pathCat+=5;
                rpgTalk.NewTalk ("M1Q0C1", "M1Q0C1End", rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
			}
		}
		if ( questionId == 1) { // escolha final da missão 2
			if (choiceID == 0) {
				pathBird+=6;
				rpgTalk.NewTalk ("M2Q1C0", "M2Q1C0End", rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");
			} else {
				pathCat+=5;
				rpgTalk.NewTalk ("M2Q1C1", "M2Q1C1End", rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");
			}
		}
        if ( questionId == 2) { // escolha final da missão 3
            if (choiceID == 0) {
                pathBird+=5;
                rpgTalk.NewTalk ("M3Q2C0", "M3Q2C0End", rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
            } else {
                pathCat+=5;
                rpgTalk.NewTalk ("M3Q2C1", "M3Q2C1End", rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
            }
        }
        if (questionId == 3)
        { // escolha inicial da missão 4
            if (choiceID == 0)
            {
                pathCat += 3;
                rpgTalk.NewTalk("M4Q3C0", "M4Q3C0End");
            }
            else
            {
                pathBird += 3;
                //rpgTalk.NewTalk("M4Q3C1", "M4Q3C1End"); essa escolha está sem fala definida. falas vazias não devem ser chamadas.
            }
        }
    }

    public void Print(string text)
    {
        print(text);
    }
    
}
