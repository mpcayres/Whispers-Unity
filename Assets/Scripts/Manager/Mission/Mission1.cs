using UnityEngine;
using UnityEngine.SceneManagement;


public class Mission1 : Mission {
    enum enumMission { NIGHT, INICIO, GATO_APARECEU, GATO_CORREDOR,
        GATO_COZINHA, GATO_SALA, LANTERNA_ENCONTRADA, CORVO_VISTO, MAE_QUARTO, FAZER_ESCOLHA, FINAL };
    enumMission secao;

    SceneObject window;
    //ZoomObject clock;
    float portaDefaultX, portaDefaultY;
    bool areaTriggered = false, birdsActive = false;

    public override void InitMission()
    {
        sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float) -2.5;
        MissionManager.initY = (float) 0.7;
        MissionManager.initDir = 0;
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {
        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                EspecificaEnum((int)enumMission.INICIO);
                MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneStart", "M1KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
            }
        }
        else if (secao == enumMission.INICIO)
        {
            if (window.ObjectOpened() /*|| CursorLockMode.ObjectOpened()*/)
            {
                EspecificaEnum((int) enumMission.GATO_APARECEU);
            }
        }
        else if (secao == enumMission.GATO_APARECEU)
        {
            if (MissionManager.instance.currentSceneName.Equals("Corredor") && !MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.GATO_CORREDOR);
                MissionManager.instance.blocked = true;
                Cat.instance.GetComponent<Cat>().Patrol(true);
                Transform aux = new GameObject().transform;
                aux.position = new Vector3(3f, -0.7f, -0.5f);
                Transform[] catPos = { aux };
                Cat.instance.GetComponent<Cat>().targets = catPos;
                MissionManager.instance.Invoke("InvokeMission", 5.7f);
            }
        }
        else if (secao == enumMission.GATO_SALA)
        {
            if (Inventory.HasItemType(Inventory.InventoryItems.FLASHLIGHT))
            {
                EspecificaEnum((int)enumMission.LANTERNA_ENCONTRADA);
            }
        }
        else if (secao == enumMission.MAE_QUARTO)
        {
            if (!MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.FAZER_ESCOLHA);
            }
        }

        if (secao == enumMission.GATO_SALA || secao == enumMission.LANTERNA_ENCONTRADA)
        {
            if (areaTriggered && !Flashlight.GetState() && !birdsActive)
            {
                GameObject birds = GameObject.Find("BirdEmitterColliderHolder").gameObject;
                birds.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
                birdsActive = true;
            }
        }
    }

    public override void SetCorredor()
    {
        if (secao == enumMission.GATO_APARECEU)
        {
            MissionManager.instance.rpgTalk.NewTalk("M1CorridorSceneStart", "M1CorridorSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Porta Mae
        GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
        float portaMaeDefaultY = portaMae.transform.position.y;
        float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
        portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
        portaMae.tag = "Untagged";
        portaMae.GetComponent<Collider2D>().isTrigger = false;
        portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

        if (secao == enumMission.GATO_APARECEU)
        {
            MissionManager.instance.AddObject("catFollower", "", new Vector3(8f, -0.6f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;

            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.tag = "Untagged";
            portaSala.GetComponent<Collider2D>().isTrigger = false;

            // gato andando para cozinha

        }
        else if (secao == enumMission.GATO_COZINHA)
        {
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(-0.7f, -0.6f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol(true);
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(-9.8f, -0.7f, -0.5f);
            Transform[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;

            MissionManager.instance.blocked = true;
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
            MissionManager.instance.Invoke("InvokeMission", 2.5f);

            // Objeto movel que atrapalha
            MissionManager.instance.AddObject("MovingObject", "Sprites/Objects/Scene/vaso", 
                new Vector3((float)-3.59, (float)-0.45, 0), new Vector3((float)1.2, (float)1.2, 1));

            // gato andando para sala
        }
    }

    public override void SetCozinha()
    {
        //MissionManager.instance.rpgTalk.NewTalk ("M1KitchenSceneStart", "M1KitchenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Panela com tampa
        GameObject panela = GameObject.Find("Panela").gameObject;
        panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");

        if (secao == enumMission.GATO_CORREDOR)
        {
            EspecificaEnum((int)enumMission.GATO_COZINHA);
        }
        
    }

    public override void SetJardim()
    {
        //MissionManager.instance.rpgTalk.NewTalk ("M1GardenSceneStart", "M1GardenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        /*GameObject areaLight = GameObject.Find("AreaLightHolder").gameObject; //utilizar AreaLight para cenas de dia, variar Z do Holder
        areaLight.transform.Find("AreaLight").gameObject.SetActive(true);
        areaLight.transform.position = new Vector3(areaLight.transform.position.x, areaLight.transform.position.y, -20);*/
    }

    public override void SetQuartoKid()
    {
        // Luz
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (secao == enumMission.NIGHT || secao == enumMission.INICIO) {
            // Janela
            GameObject windowObject = GameObject.Find("WindowTrigger").gameObject;
            window = windowObject.GetComponent<SceneObject>();
            windowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/window-closed");
            window.sprite1 = Resources.Load<Sprite>("Sprites/Objects/Scene/window-closed");
            window.sprite2 = Resources.Load<Sprite>("Sprites/Objects/Scene/window-opened");

            // Relogio
            //clock = GameObject.Find("Relogio").gameObject.GetComponent<ZoomObject>();
        }

        if (secao == enumMission.NIGHT || secao == enumMission.INICIO || secao == enumMission.CORVO_VISTO)
        {
            // Porta
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            portaDefaultX = porta.transform.position.x;
            portaDefaultY = porta.transform.position.y;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.tag = "Untagged";
            porta.GetComponent<Collider2D>().isTrigger = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);
        }

        if(secao == enumMission.MAE_QUARTO)
        {
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().EnableFlashlight(false);
            MissionManager.instance.GetComponent<Player>().ChangePositionDefault(-2.5f, 0.7f, 0);
            MissionManager.instance.AddObject("mom", "", new Vector3(1.7f, 0.6f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneRepeat", "M1KidRoomSceneRepeatEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        }

    }

    public override void SetQuartoMae()
    {
        //MissionManager.instance.rpgTalk.NewTalk ("M1MomRoomSceneStart", "M1MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetSala()
    {
        //MissionManager.instance.rpgTalk.NewTalk ("M1LivingroomSceneStart", "M1LivingroomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLightBooks").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Lanterna - Criado Mudo
        GameObject criadoMudo = GameObject.Find("CriadoMudoSala").gameObject;
        criadoMudo.tag = "ScenePickUpObject";
        SceneObject sceneObject = criadoMudo.GetComponent<SceneObject>();
        sceneObject.enabled = false;
        ScenePickUpObject scenePickUpObject = criadoMudo.AddComponent<ScenePickUpObject>();
        scenePickUpObject.sprite1 = sceneObject.sprite1;
        scenePickUpObject.sprite2 = sceneObject.sprite2;
        scenePickUpObject.positionSprite = sceneObject.positionSprite;
        scenePickUpObject.scale = sceneObject.scale;
        scenePickUpObject.isUp = sceneObject.isUp;
        scenePickUpObject.item = Inventory.InventoryItems.FLASHLIGHT;

        // Porta Jardim
        GameObject portaJardim = GameObject.Find("DoorToGarden").gameObject;
        portaJardim.tag = "Untagged";
        portaJardim.GetComponent<Collider2D>().isTrigger = false;

        // Porta Corredor
        GameObject portaCorredor = GameObject.Find("DoorToAlley").gameObject;
        portaCorredor.tag = "Untagged";
        portaCorredor.GetComponent<Collider2D>().isTrigger = false;

        GameObject birds = GameObject.Find("BirdEmitterColliderHolder").gameObject;
        birds.transform.Find("AreaTrigger").gameObject.SetActive(true);
        birds.transform.Find("TVTrigger").gameObject.SetActive(true);
        areaTriggered = false;
        birdsActive = false;

        if (secao == enumMission.GATO_COZINHA)
        {
            EspecificaEnum((int)enumMission.GATO_SALA);
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission) pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.GATO_APARECEU)
        {
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneCat", "M1KidRoomSceneCatEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");

            // Porta abrindo
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-opened");
            porta.tag = "DoorToAlley";
            porta.GetComponent<Collider2D>().isTrigger = true;
            porta.transform.position = new Vector3(portaDefaultX, portaDefaultY, porta.transform.position.z);
        }
        else if (secao == enumMission.CORVO_VISTO)
        {
            MissionManager.instance.blocked = true;
            GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLightTV").gameObject.SetActive(true);
            GameObject.Find("TV").gameObject.GetComponent<SceneMultipleObject>().ChangeSprite();
            MissionManager.instance.AddObject("CorvoSombra", "", new Vector3(10.5f, 0, 0), new Vector3(1, 1, 1));
            MissionManager.instance.Invoke("InvokeMission", 10f);
        }
        else if (secao == enumMission.MAE_QUARTO)
        {
            SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        }
        else if (secao == enumMission.FAZER_ESCOLHA)
        {
            GameObject.Destroy(GameObject.Find("mom(Clone)").gameObject);
            MissionManager.instance.Invoke("InvokeMission", 4f);
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("AreaTrigger"))
        {
            areaTriggered = true;
        }
        else if (tag.Equals("TVTrigger") && secao == enumMission.LANTERNA_ENCONTRADA)
        {
            EspecificaEnum((int)enumMission.CORVO_VISTO);
        }
    }

    public override void InvokeMission()
    {
        if (secao == enumMission.GATO_CORREDOR || secao == enumMission.GATO_COZINHA)
        {
            GameObject.Destroy(GameObject.Find("catFollower(Clone)").gameObject);
            MissionManager.instance.blocked = false;
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 2;
        }
        else if (secao == enumMission.CORVO_VISTO)
        {
            EspecificaEnum((int)enumMission.MAE_QUARTO);
        }
        else if (secao == enumMission.FAZER_ESCOLHA)
        {
            MissionManager.instance.AddObject("catFollower", "", new Vector3(1.7f, 0.7f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneChoice", "M1KidRoomSceneChoiceEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.ChangeMission(2);
        }
    }

    public override void InvokeMissionChoice(int id)
    {
        if (secao == enumMission.FAZER_ESCOLHA)
        {
            EspecificaEnum((int)enumMission.FINAL);
            if(id == 0)
            {
                GameObject.Destroy(GameObject.Find("catFollower(Clone)").gameObject);
            }
            else
            {
                GameObject.Find("catFollower(Clone)").gameObject.GetComponent<Cat>().FollowPlayer(true);
            }
            MissionManager.instance.Invoke("InvokeMission", 8f);
        }
    }

    public void AddCountKidRoomDialog()
    {
		MissionManager.instance.countKidRoomDialog++;
	}

	public void AddCountCorridorDialog()
    {
		MissionManager.instance.countCorridorDialog++;
	}
}
