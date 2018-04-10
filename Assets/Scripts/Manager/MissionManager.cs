using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance;

    // MISSÕES
    public Mission mission;
    public SideQuest sideQuest;
    public int currentMission = 0;
    public static bool initMission = false;
    public static float initX = 0, initY = 0;
    public static int initDir = 0;

    // CENAS
    public string previousSceneName = "", currentSceneName = "";

    // EXTRAS
    public int sideQuests = 0;

    // ESCOLHAS
    public float pathBird = 0, pathCat = 0;

    // ESCOLHAS ESPECÍFICAS
    public bool mission1AssustaGato = false;
    public bool mission2ContestaMae = false;
    public bool mission4QuebraSozinho = false;
    public bool mission8BurnCorredor = false;

    // CONIDÇÕES DO JOGO
    public bool paused = false;
    public bool pausedObject = false;
    public bool blocked = false;
    public bool invertWorld = false;
    public bool invertWorldBlocked = true;
    public bool playerProtected = false;

    // HUD - INÍCIO MISSÃO
    public bool showMissionStart = true;
    private float startMissionDelay = 3f;
    private GameObject hud;
    private GameObject levelImage;
    private Text levelText;

    // RPG TALK
    public RPGTalk rpgTalk;

    // SAVE
    private Save currentSave;

    // SONS
    public ScenerySounds scenerySounds;
    public ScenerySounds2 scenerySounds2;

    // CRIAÇÃO JOGO
    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            currentSceneName = SceneManager.GetActiveScene().name;
            previousSceneName = currentSceneName;

            hud = GameObject.Find("HUDCanvas").gameObject;

            currentMission = PlayerPrefs.GetInt("Mission");
            // Salva o jogo somente quando jogar através do New Game ou Continue
            if (currentMission == -1 || currentMission == 0)
            {
                PlayerPrefs.SetInt("SaveGame", 0);
            }
            else
            {
                PlayerPrefs.SetInt("SaveGame", 1);
            }

            if (currentMission == -1)
            {
                currentMission = 1;

                Inventory.SetInventory(null);
                GameObject.Find("Player").gameObject.transform.Find("Tampa").gameObject.GetComponent<ProtectionObject>().life = 80;

                Book.pageQuantity = 0;
                sideQuests = 0;

                pathBird = 0;
                pathCat = 0;

                mission1AssustaGato = false;
                mission2ContestaMae = false;
                mission4QuebraSozinho = false;
                mission8BurnCorredor = false;

                SetMission(currentMission);
                SaveGame(1, -1);
            }
            else
            {
                LoadGame(currentMission);
            }

            rpgTalk.OnChoiceMade += OnChoiceMade;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ATUALIZAÇÕES DO JOGO
    public void Update()
    {
        if (!showMissionStart)
        {

            if (mission != null)
            {
                mission.UpdateMission();
            }

            if (sideQuest != null)
            {
                sideQuest.UpdateSideQuest();
            }

            if (CrossPlatformInputManager.GetButtonDown("EndText"))
            {
                rpgTalk.EndTalk();
            }

            if (!blocked && (!paused || Book.show) && CrossPlatformInputManager.GetButtonDown("keyInvert") && !invertWorldBlocked)
            {
                scenerySounds.PlayDemon(6);
                InvertWorld(!invertWorld);
            }

        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("JumpText"))
            {
                HideLevelImage();
            }
        }

        // CONDIÇÃO PARA SAIR - MENU
        if (CrossPlatformInputManager.GetButtonDown("Exit"))
        {
            LoadScene(0);
        }

        // CHEATS
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMission(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMission(2);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ChangeMission(3);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			ChangeMission(4);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			ChangeMission(5);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			ChangeMission(6);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			ChangeMission(7);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			ChangeMission(8);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeMission(9);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadGame(0);
        }

    }

    /************ FUNÇÕES DE CENA ************/

    // FUNÇÕES DE MUDANÇA DE CENA
    public static void LoadScene(string name)
    {
        SaveObjectsVariables();
        SceneManager.LoadScene(name);
    }

    public static void LoadScene(int index)
    {
        SaveObjectsVariables();
        SceneManager.LoadScene(index);
    }

    // FUNÇÕES PARA CONTAR MUDANÇA DE CENA
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // FUNÇÃO APÓS MUDANÇA DE CENA
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        previousSceneName = currentSceneName;
        currentSceneName = scene.name;
        //print("OLDSCENE" + previousSceneName);
        //print("NEWSCENE" + currentSceneName);

        if (!initMission && !currentSceneName.Equals("SideQuest") && !previousSceneName.Equals("SideQuest"))
        {
            GetComponent<Player>().ChangePosition();
        }
        else if (currentSceneName.Equals("SideQuest") || previousSceneName.Equals("SideQuest"))
        {
            if (currentSceneName.Equals("SideQuest") && sideQuest != null)
            {
                sideQuest.InitSideQuest();
            }
            else if (previousSceneName.Equals("SideQuest") && !currentSceneName.Equals("GameOver") && sideQuest != null)
            {
                sideQuest.EndSideQuest();
            }
            GetComponent<Player>().ChangePositionDefault(initX, initY, initDir);
        }
        else
        {
            GetComponent<Player>().ChangePositionDefault(initX, initY, initDir);
            if (initMission && currentSceneName.Equals(mission.sceneInit))
            {
                initMission = false;
                initX = initY = 0;
            }
        }

        if (previousSceneName.Equals("GameOver"))
        {
            GetComponent<Player>().enabled = true;
            GetComponent<Renderer>().enabled = true;
        }

        if (currentSceneName.Equals("GameOver"))
        {
            GetComponent<Player>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            if (rpgTalk.isPlaying)
            {
                rpgTalk.EndTalk();
            }
        }
        else if (currentSceneName.Equals("MainMenu"))
        {
            Destroy(gameObject);
            Destroy(hud);
            if (Cat.instance != null) Cat.instance.DestroyCat();
            if (Corvo.instance != null) Corvo.instance.DestroyRaven();
            DeleteAllPlayerPrefs();
        }

        InvertWorld(invertWorld);

        if(mission != null) mission.LoadMissionScene();
        ExtrasManager.SideQuestsManager();
        ExtrasManager.PagesManager();
        SetObjectsVariables();
    }

    /************ FUNÇÕES DE OBJETO ************/

    // ADICIONAR OBJETO NA CENA
    public GameObject AddObject(string name, string sprite, Vector3 position, Vector3 scale)
    {
        print("ADD OBJECT: " + name + " [" + sprite + "]");
        GameObject instance =
            Instantiate(Resources.Load("Prefab/" + name),
            position, Quaternion.identity) as GameObject;
        if (sprite != "") instance.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
        instance.transform.localScale = scale;
        return instance;
    }

    // ADICIONAR OBJETO NA CENA, COM DADOS REDUZIDOS
    public GameObject AddObject(string name)
    {
        print("ADD OBJECT: " + name);
        GameObject instance = Instantiate(Resources.Load("Prefab/" + name)) as GameObject;
        return instance;
    }

    // ADICIONAR OBJETO NA CENA COM PAI ASSOCIADO
    public GameObject AddObjectWithParent(string name, string sprite, Vector3 position, Vector3 scale, Transform parent)
    {
        GameObject instance = AddObject(name, sprite, position, scale);
        instance.transform.parent = parent;
        return instance;
    }

    // ADICIONAR OBJETO NA CENA COM PAI ASSOCIADO, COM DADOS REDUZIDOS
    public GameObject AddObjectWithParent(string name, Transform parent)
    {
        GameObject instance = AddObject(name);
        instance.transform.SetParent(parent);
        return instance;
    }

    // SALVA AS VARIÁVEIS DE OBJETOS
    private static void SaveObjectsVariables()
    {
        SaveMovingObjectsPosition();
        SaveRotateObjectsPosition();
        SaveSceneObjectsState();
        SaveLampState();
    }

    // SALVAR POSIÇÃO DE OBJETOS MÓVEIS
    private static void SaveMovingObjectsPosition()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("MovingObject");
        foreach (GameObject i in list)
        {
            if (!i.GetComponent<MovingObject>().prefName.Equals(""))
            {
                PlayerPrefs.SetFloat(i.GetComponent<MovingObject>().prefName + "X", i.GetComponent<Rigidbody2D>().position.x);
                PlayerPrefs.SetFloat(i.GetComponent<MovingObject>().prefName + "Y", i.GetComponent<Rigidbody2D>().position.y);
            }
        }
    }

    // SALVAR POSIÇÃO DE OBJETOS ROTACIONÁVEIS
    private static void SaveRotateObjectsPosition()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("RotateObject");
        foreach (GameObject i in list)
        {
            if (!i.GetComponent<RotateObject>().prefName.Equals(""))
            {
                PlayerPrefs.SetFloat(i.GetComponent<RotateObject>().prefName + "X", i.GetComponent<Rigidbody2D>().position.x);
                PlayerPrefs.SetFloat(i.GetComponent<RotateObject>().prefName + "Y", i.GetComponent<Rigidbody2D>().position.y);
            }
        }
    }

    // SALVAR ESTADO DOS OBJETOS DE CENA
    private static void SaveSceneObjectsState()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("SceneObject");
        foreach (GameObject i in list)
        {
            if (!i.GetComponent<SceneObject>().prefName.Equals(""))
            {
                int active = 0;
                if (i.GetComponent<SceneObject>().IsActive()) active = 1;
                PlayerPrefs.SetInt(i.GetComponent<SceneObject>().prefName, active);
            }
        }
    }

    // SALVAR ESTADO DAS LÂMPADAS
    private static void SaveLampState()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject i in list)
        {
            if (!i.GetComponent<Lamp>().prefName.Equals(""))
            {
                int enabled = 0;
                if (i.GetComponent<Light>().enabled) enabled = 1;
                PlayerPrefs.SetInt(i.GetComponent<Lamp>().prefName, enabled);
            }
        }
    }

    // ESPECIFICA AS VARIÁVEIS DE OBJETOS
    private void SetObjectsVariables()
    {
        SetMovingObjectsPosition();
        SetRotateObjectsPosition();
        SetSceneObjectsState();
        SetLampState();
    }

    // POSICIONA OS OBJETOS MÓVEIS
    private void SetMovingObjectsPosition()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("MovingObject");
        foreach (GameObject i in list)
        {
            string prefName = i.GetComponent<MovingObject>().prefName;
            if (!prefName.Equals("") && PlayerPrefs.HasKey(prefName + "X") && PlayerPrefs.HasKey(prefName + "Y"))
            {
                //print("MOVING: " + prefName);
                i.GetComponent<MovingObject>().ChangePosition(
                    PlayerPrefs.GetFloat(prefName + "X"),
                    PlayerPrefs.GetFloat(prefName + "Y"));
            }
        }
    }

    // POSICIONA OS OBJETOS ROTACIONÁVEIS
    private void SetRotateObjectsPosition()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("RotateObject");
        foreach (GameObject i in list)
        {
            string prefName = i.GetComponent<RotateObject>().prefName;
            if (!prefName.Equals("") && PlayerPrefs.HasKey(prefName + "X") && PlayerPrefs.HasKey(prefName + "Y"))
            {
                //print("ROTATE: " + prefName);
                i.GetComponent<RotateObject>().ChangePosition(
                    PlayerPrefs.GetFloat(prefName + "X"),
                    PlayerPrefs.GetFloat(prefName + "Y"));
            }
        }
    }

    // DETERMINA O ESTADO DOS OBJETOS DE CENA
    private void SetSceneObjectsState()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("SceneObject");
        foreach (GameObject i in list)
        {
            string prefName = i.GetComponent<SceneObject>().prefName;
            if (!prefName.Equals("") && PlayerPrefs.HasKey(prefName))
            {
                //print("SCENEOBJ: " + prefName);
                bool active = false;
                if (PlayerPrefs.GetInt(prefName) == 1) active = true;
                i.GetComponent<SceneObject>().ChangeSpriteActive(active);
            }
        }
    }

    // DETERMINA O ESTADO DAS LÂMAPDAS
    private void SetLampState()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject i in list)
        {
            if (!i.GetComponent<Lamp>().prefName.Equals(""))
            {
                //print("LAMP: " + i.GetComponent<Lamp>().prefName);
                bool enabled = false;
                if (PlayerPrefs.GetInt(i.GetComponent<Lamp>().prefName) == 1) enabled = true;
                i.GetComponent<Light>().enabled = enabled;
            }
        }
    }

    // DELETAR TODOS OS ESTADOS DOS OBJETOS
    public void DeleteAllPlayerPrefs()
    {
        string language = "";
        int currentSave = 0, maxSave = 0;
        if (PlayerPrefs.HasKey("Language"))
        {
            language = PlayerPrefs.GetString("Language");
        }
        if (PlayerPrefs.HasKey("CurrentSaveGame"))
        {
            currentSave = PlayerPrefs.GetInt("CurrentSaveGame");
        }
        if (PlayerPrefs.HasKey("MaxSaveGame"))
        {
            maxSave = PlayerPrefs.GetInt("MaxSaveGame");
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.SetInt("CurrentSaveGame", currentSave);
        PlayerPrefs.SetInt("MaxSaveGame", maxSave);
    }

    /************ FUNÇÕES DE SAVE ************/

    // CRIAR SAVE
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.mission = currentMission;

        save.inventory = Inventory.GetInventoryItems();
        save.currentItem = Inventory.GetCurrentItem();
        if (Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
        {
            save.lifeTampa = GameObject.Find("Player").gameObject.transform.Find("Tampa").gameObject.GetComponent<ProtectionObject>().life;
        }
        else
        {
            save.lifeTampa = 0;
        }
        save.pedraCount = Inventory.pedraCount;


        save.numberPages = Book.pageQuantity;
        save.sideQuests = sideQuests;

        save.pathBird = pathBird;
        save.pathCat = pathCat;
        
        save.mission1AssustaGato = mission1AssustaGato;
        save.mission2ContestaMae = mission2ContestaMae;
        save.mission4QuebraSozinho = mission4QuebraSozinho;
        save.mission8BurnCorredor = mission8BurnCorredor;

        save.time = DateTime.UtcNow.ToString();

        return save;
    }

    // SALVAR JOGO - COMPLETO
    public void SaveGame(int m, int opt)
    {
        Save save = CreateSaveGameObject();
        SaveGame(save, m, opt);
    }

    // SALVAR JOGO - ATUALIZAÇÃO
    public void UpdateSave()
    {
        if (currentSave != null)
        {
            currentSave.sideQuests = sideQuests;
            currentSave.numberPages = Book.pageQuantity;
            currentSave.time = DateTime.UtcNow.ToString();
            SaveGame(currentSave, currentMission, 0);
        }
    }

    // SALVAR JOGO
    private void SaveGame(Save save, int m, int opt)
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        // opt = -1 -> new game
        // opt = 0 -> continue
        if ((opt == -1 || opt == 0) && PlayerPrefs.HasKey("SaveGame") && PlayerPrefs.GetInt("SaveGame") == 0)
        {
            int numSave = 0;
            if (PlayerPrefs.HasKey("CurrentSaveNumber"))
            {
                numSave = PlayerPrefs.GetInt("CurrentSaveNumber");
                if (opt == -1)
                {
                    numSave++;
                    PlayerPrefs.SetInt("CurrentSaveNumber", numSave);
                    PlayerPrefs.SetInt("MaxSaveNumber", numSave);
                }
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSaveNumber", numSave);
                PlayerPrefs.SetInt("MaxSaveNumber", numSave);
            }

            FileStream file = File.Create(Application.persistentDataPath + "/gamesave" + 0 + "_" + numSave + ".save");
            bf.Serialize(file, save);
            file.Close();

            FileStream fileMission = File.Create(Application.persistentDataPath + "/gamesave" + m + "_" + numSave + ".save");
            bf.Serialize(fileMission, save);
            fileMission.Close();

            Debug.Log("Game Saved " + m);
        }
        
    }

    // CARREGAR JOGO
    public void LoadGame(int m)
    {
        if (PlayerPrefs.HasKey("CurrentSaveNumber") && 
            File.Exists(Application.persistentDataPath + "/gamesave" + m + "_" + PlayerPrefs.GetInt("CurrentSaveNumber") + ".save"))
        {
            paused = true;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave" + m + "_" + PlayerPrefs.GetInt("CurrentSaveNumber") + ".save", FileMode.Open);
            currentSave = (Save)bf.Deserialize(file);
            file.Close();
            
            Inventory.SetInventory(currentSave.inventory);
            if (currentSave.currentItem != -1) Inventory.SetCurrentItem(currentSave.currentItem);
            if (Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
            {
                GameObject.Find("Player").gameObject.transform.Find("Tampa").gameObject.GetComponent<ProtectionObject>().life = currentSave.lifeTampa;
            }
            Inventory.pedraCount = currentSave.pedraCount;

            Book.pageQuantity = currentSave.numberPages;
            sideQuests = currentSave.sideQuests;

            pathBird = currentSave.pathBird;
            pathCat = currentSave.pathCat;
           
            mission1AssustaGato = currentSave.mission1AssustaGato;
            mission2ContestaMae = currentSave.mission2ContestaMae;
            mission4QuebraSozinho = currentSave.mission4QuebraSozinho;
            mission8BurnCorredor = currentSave.mission8BurnCorredor;

            SetMission(currentSave.mission);

            Debug.Log("Game Loaded " + m + " [" + PlayerPrefs.GetInt("CurrentSaveNumber") + "]");

            paused = false;
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    /************ FUNÇÕES DE MISSÃO ************/

    // INICIALIZAR MISSÃO
    public void SetMission(int m)
    {
        currentMission = m;
        print("MISSAO: " + currentMission);

		switch(currentMission){
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
        case 9:
            mission = new Mission9();
            break;
        }
        
        levelImage = hud.transform.Find("LevelImage").gameObject;
        levelText = levelImage.transform.Find("LevelText").GetComponent<Text>();

        if (mission != null)
        {
            //DeleteAllPlayerPrefs(); - talvez resetar ao mudar de missão, já que é outra noite (mas dá erro)
            levelText.text = "Chapter  " + m;
            levelImage.SetActive(true);
            showMissionStart = true;
            mission.InitMission();
            Invoke("HideLevelImage", startMissionDelay);
        }
        else {
            showMissionStart = false;
        }
    }

    // MUDAR MISSÃO
    public void ChangeMission(int m)
    {
        SetMission(m);
        SaveGame(currentMission, 0);
    }

    // FINALIZAR JOGO
    public void GameOver()
    {
        blocked = true;
        hud.SetActive(false);
        InvertWorld(false);
        if (sideQuest == null)
        {
            if (Cat.instance != null) Cat.instance.DestroyCat();
            if (Corvo.instance != null) Corvo.instance.DestroyRaven();
        }
        LoadScene("GameOver");
    }

    // CONTINUAR JOGO
    public void ContinueGame()
    {
        LoadScene(previousSceneName);
        blocked = false;
        hud.SetActive(true);
    }

    /************ FUNÇÕES ESPECIAIS ************/

    // INVERTER MUNDO
    public void InvertWorld(bool sel)
    {
        invertWorld = sel;
        if (!currentSceneName.Equals("GameOver") && !currentSceneName.Equals("MainMenu"))
        {
            GameObject.Find("MainCamera").GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionLookup>().enabled = invertWorld;
        }
    }

    // AUXILIAR PARA RETORNAR NOMES DE ARQUIVO SEGUINDO PADRÃO
    public static string[] GetFilesByPattern(string path, string pattern)
    {
        string[] files = Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
        if (files.Length > 0)
        {
            return files;
        }
        return null;
    }

    // AUXILIAR PARA IDENTIFICAÇÃO DA EXISTÊNCIA DE PADRÕES DE ARQUIVO
    public static bool FilePatternExists(string path, string pattern)
    {
        return (GetFilesByPattern(path, pattern) != null);
    }

    // AUXILIAR PARA INVOCAÇÃO NAS MISSÕES
    public void InvokeMission()
    {
        //print("INVOKEMISSION");
        mission.InvokeMission();
    }

    // AUXILIAR PARA IMPRIMIR NAS MISSÕES
    public void Print(string text)
    {
        print(text);
    }

    // ESCONDER IMAGEM INICIAL
    void HideLevelImage()
    {
        if (levelImage != null) levelImage.SetActive(false);
        showMissionStart = false;
    }

    /************ FUNÇÕES DE ESCOLHA ************/

    // FUNÇÃO APÓS ESCOLHA SER FEITA
    public void OnChoiceMade(int questionId, int choiceID)
    {
		if (questionId == 0) { // escolha final da missão 1
			if (choiceID == 0) { // assustar gato
				pathBird += 2;
                rpgTalk.NewTalk ("M1Q0C0", "M1Q0C0End", rpgTalk.txtToParse);
			} else { // ficar com gato
				pathCat += 4;
                rpgTalk.NewTalk ("M1Q0C1", "M1Q0C1End", rpgTalk.txtToParse);
			}
		}
		else if (questionId == 1) { // escolha final da missão 2
			if (choiceID == 0) { // contestar mãe
				pathBird += 6;
				rpgTalk.NewTalk ("M2Q1C0", "M2Q1C0End", rpgTalk.txtToParse);
			} else { // respeitar mãe
				pathCat += 5;
				rpgTalk.NewTalk ("M2Q1C1", "M2Q1C1End", rpgTalk.txtToParse);
			}
		}
        else if (questionId == 2) { // escolha final da missão 3
            if (choiceID == 0) { // mentir
                pathBird += 4;
                rpgTalk.NewTalk ("M3Q2C0", "M3Q2C0End", rpgTalk.txtToParse);
            } else { // contar a verdade
                pathCat += 4;
                rpgTalk.NewTalk ("M3Q2C1", "M3Q2C1End", rpgTalk.txtToParse);
            }
        }
        else if (questionId == 3) { // escolha inicial da missão 4 - escolha de quem vai quebrar o vaso
            if (choiceID == 0) { // quebra com o gato
                pathCat += 4;
                mission4QuebraSozinho = false;
                rpgTalk.NewTalk("M4Q3C0", "M4Q3C0End");
            }
            else{ // quebra sozinho
                pathBird += 4;
                mission4QuebraSozinho = true;
                rpgTalk.NewTalk("M4Q3C1", "M4Q3C1End"); //essa escolha está sem fala definida. falas vazias não devem ser chamadas.
            }
        }
        if (questionId == 4) { // escolha final da missão 4
            if (choiceID == 0) { // falar a verdade
                pathCat += 4;
                rpgTalk.NewTalk("M4Q4C0", "M4Q4C0End");
            }
            else { // mentir
                pathBird += 6;
                rpgTalk.NewTalk("M4Q4C1", "M4Q4C1End");
            }
        }
        if (questionId == 5) { // escolha final da missão 5
            if (choiceID == 0) { // esconder
                pathBird += 4;
                rpgTalk.NewTalk("M5Q5C0", "M5Q5C0End");
            }
            else { // investigar
                pathCat += 5;
                rpgTalk.NewTalk("M5Q5C1", "M5Q5C1End");
            }
        }
        mission.InvokeMissionChoice(choiceID);
    }
    
}
