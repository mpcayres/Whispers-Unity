using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission3 : Mission {
    enum enumMission { NIGHT, INICIO, GATO_CORREDOR, QUADRO, MI_DESBLOQUEADO, MI_ATIVADO,
        CORVO_APARECE, CORVO_ATACA, MI_TRAVADO, MAE_APARECE, PASSAROS_FINAL, FINAL };
    enumMission secao;

    ZoomObject quadro1, quadro2;
    bool quadro1Seen = false, quadro2Seen = false;
    GameObject livro, catShadow, person1, person2, corvo; // mesma logica do livro, colocar pros objetos de pessoas andando
    bool livroAtivado = false;

    public override void InitMission()
	{
		sceneInit = "QuartoKid";
		MissionManager.initMission = true;
		MissionManager.initX = (float) -2.5;
		MissionManager.initY = (float) -1.6;
		MissionManager.initDir = 3;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();
        Book.bookBlocked = true;

        MissionManager.instance.invertWorld = false;
        MissionManager.instance.invertWorldBlocked = true;
        MissionManager.instance.paused = false;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {

        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                EspecificaEnum((int)enumMission.INICIO);
                MissionManager.instance.rpgTalk.NewTalk("M3KidRoomSceneStart", "M3KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
            }
        }
        else if (secao == enumMission.QUADRO && MissionManager.instance.currentSceneName.Equals("Corredor"))
        {
            if (quadro1Seen && quadro2Seen)
            {
                EspecificaEnum((int)enumMission.MI_DESBLOQUEADO);
            }
            else if (quadro1.ObjectOpened() && !quadro1Seen)
            {
                MissionManager.instance.scenerySounds.PlayCat(4);
                MissionManager.instance.Print("QUADRO1");
                quadro1Seen = true;
            }
            else if (quadro2.ObjectOpened() && !quadro2Seen)
            {
                MissionManager.instance.scenerySounds.PlayCat(2);
                MissionManager.instance.Print("QUADRO2");
                quadro2Seen = true;
            }
        }
        else if (secao == enumMission.MI_DESBLOQUEADO)
        {
            if (MissionManager.instance.invertWorld)
            {
                EspecificaEnum((int)enumMission.MI_ATIVADO);
            }
        }
        else if (secao == enumMission.MI_ATIVADO)
        {
            if (!MissionManager.instance.rpgTalk.isPlaying)
            {
                MissionManager.instance.scenerySounds.PlayBird(1);
                EspecificaEnum((int)enumMission.CORVO_APARECE);
            }

            if (MissionManager.instance.invertWorld && !livroAtivado)
            {
                livro.SetActive(true);
                person1.SetActive(true);
                person2.SetActive(true);
                Cat.instance.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                catShadow.GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                livroAtivado = true;
            }
            else if (!MissionManager.instance.invertWorld && livroAtivado)
            {
                livro.SetActive(false);
                person1.SetActive(false);
                person2.SetActive(false);
                Cat.instance.GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                catShadow.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                livroAtivado = false;
            }
        }
        else if (secao == enumMission.MI_TRAVADO)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MissionManager.instance.rpgTalk.NewTalk("M3MundoInvertido3", "M3MundoInvertido3End");
            }
        }
        else if (secao == enumMission.MAE_APARECE)
        {
            if (MissionManager.instance.invertWorld)
            {
                EspecificaEnum((int) enumMission.PASSAROS_FINAL);
            }
        }
    }

	public override void SetCorredor()
	{
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            EspecificaEnum((int)enumMission.GATO_CORREDOR);
        }

        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M3CorridorSceneStart", "M3CorridorSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Definir objetos dos quadros
        GameObject quadro1Object = GameObject.Find("Quadro1").gameObject;
        quadro1 = quadro1Object.GetComponent<ZoomObject>();
        GameObject quadro2Object = GameObject.Find("Quadro2").gameObject;
        quadro2 = quadro1Object.GetComponent<ZoomObject>();

        // Quarto da mãe bloqueado
        GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
        portaMae.GetComponent<Collider2D>().isTrigger = false;

        if (secao == enumMission.INICIO || secao == enumMission.GATO_CORREDOR)
        {
            GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(1.3f, 0.4f, 1), new Vector3(1, 1, 1));
            trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            trigger.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            EspecificaEnum((int)enumMission.GATO_CORREDOR);
            MissionManager.instance.invertWorldBlocked = true;
        }
    }

    public override void SetCozinha()
    {
        MissionManager.instance.scenerySounds.PlayDrop();
        //MissionManager.instance.rpgTalk.NewTalk ("M3KitchenSceneStart", "M3KitchenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Panela para caso ainda não tenha
        if (!Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panela.GetComponent<ScenePickUpObject>().enabled = true;
        }
    }

	public override void SetJardim()
	{
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            secao = enumMission.MAE_APARECE; // não chamar as definições do EspecificaEnum

        }

        MissionManager.instance.scenerySounds.PlayBird(2);
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (secao == enumMission.MAE_APARECE)
        {
            GameObject porta = GameObject.Find("DoorToLivingRoom").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;

            MissionManager.instance.invertWorldBlocked = false;

            MissionManager.instance.rpgTalk.NewTalk("M3GardenSceneStart", "M3GardenSceneEnd");
        }
    }

	public override void SetQuartoKid()
	{
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        // Condicoes a partir da missao 2
        if (MissionManager.instance.mission2ContestaMae)
        {
            // Arranhao
            MissionManager.instance.AddObject("Garra", "", new Vector3(-1.48f, 1.81f, 0), new Vector3(0.1f, 0.1f, 1));
        }
        else
        {
            // Vela
            GameObject velaFixa = MissionManager.instance.AddObject("EmptyObject", "", new Vector3(0.125f, -1.1f, 0), new Vector3(2.5f, 2.5f, 1));
            velaFixa.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/vela_acesa1");
            velaFixa.GetComponent<SpriteRenderer>().sortingOrder = 140;
            GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true);
        }

        if(secao == enumMission.NIGHT || secao == enumMission.INICIO)
        {
            MissionManager.instance.scenerySounds.PlayCat(3);
            MissionManager.instance.AddObject("catFollower", "", new Vector3(0f, 0f, -0.5f), new Vector3(0.15f, 0.15f, 1));
        }
	}

	public override void SetQuartoMae()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M3MomRoomSceneStart", "M3MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }


	public override void SetSala()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M3LivingRoomSceneStart", "M3LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (secao == enumMission.CORVO_ATACA)
        {
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;

            GameObject portaG = GameObject.Find("DoorToGarden").gameObject;
            portaG.GetComponent<Collider2D>().isTrigger = false;

            EspecificaEnum((int)enumMission.MI_TRAVADO);
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.InvertWorld(false);
            MissionManager.instance.ChangeMission(4);
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.INICIO)
        {
            Cat.instance.Patrol();
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(1.8f, 0.8f, -0.5f);
            Transform[] catPos = { aux };
            Cat.instance.targets = catPos;
            Cat.instance.speed = 1.2f;
            Cat.instance.destroyEndPath = true;
        }
        else if (secao == enumMission.GATO_CORREDOR)
        {
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(8.2f, -0.2f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol();
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(1f, -0.1f, -0.5f);
            Transform[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;
            cat.GetComponent<Cat>().speed = 1.6f;
            cat.GetComponent<Cat>().stopEndPath = true;
        }
        else if (secao == enumMission.MI_DESBLOQUEADO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M3Painting", "M3PaintingEnd");
            MissionManager.instance.invertWorldBlocked = false;

            // Porta Mae
            GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
            float portaMaeDefaultY = portaMae.transform.position.y;
            float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaMae.GetComponent<Collider2D>().isTrigger = false;
            portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

            // Porta Kid
            GameObject portaKid = GameObject.Find("DoorToKidRoom").gameObject;
            float portaKidDefaultY = portaKid.transform.position.y;
            float posXKid = portaKid.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaKid.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaKid.GetComponent<Collider2D>().isTrigger = false;
            portaKid.transform.position = new Vector3(portaKid.transform.position.x - posXKid, portaKidDefaultY, portaKid.transform.position.z);

            // Porta Cozinha
            GameObject portaCozinha = GameObject.Find("DoorToKitchen").gameObject;
            portaCozinha.GetComponent<Collider2D>().isTrigger = false;

            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (secao == enumMission.MI_ATIVADO)
        {
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;

            // Objetos do mundo invertido
            // Livro
            livro = MissionManager.instance.AddObject("FixedObject", "", new Vector3(6.8f, 0.68f, -0.5f), new Vector3(0.5f, 0.5f, 1));
            livro.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/livro");
            livro.GetComponent<SpriteRenderer>().color = Color.black;
            livro.AddComponent<Light>();
            livroAtivado = true;

            // Gato Sombra
            // VER MAIS CAMINHOS, COMO UMA HISTORIA
            Cat.instance.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            catShadow = MissionManager.instance.AddObject("catShadow", "", new Vector3(8.2f, -0.2f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            catShadow.GetComponent<Patroller>().isPatroller = true;
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(6.8f, -0.2f, -0.5f);
            Transform[] catPos = { aux };
            catShadow.GetComponent<Patroller>().targets = catPos;
            catShadow.GetComponent<Patroller>().speed = 0.9f;
            catShadow.GetComponent<Patroller>().stopEndPath = true;

            // Pessoa 1
            person1 = MissionManager.instance.AddObject("personShadow", "", new Vector3(10f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            person1.GetComponent<Patroller>().isPatroller = true;
            Transform auxP1 = new GameObject().transform;
            auxP1.position = new Vector3(7.3f, 0f, -0.5f);
            Transform[] p1Pos = { auxP1 };
            person1.GetComponent<Patroller>().targets = p1Pos;
            person1.GetComponent<Patroller>().speed = 0.9f;
            person1.GetComponent<Patroller>().stopEndPath = true;

            // Pessoa 2
            person2 = MissionManager.instance.AddObject("personShadow", "", new Vector3(-1f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            person2.GetComponent<Patroller>().isPatroller = true;
            Transform auxP2 = new GameObject().transform;
            auxP2.position = new Vector3(6.5f, 0f, -0.5f);
            Transform[] p2Pos = { auxP2 };
            person2.GetComponent<Patroller>().targets = p2Pos;
            person2.GetComponent<Patroller>().speed = 0.9f;
            person2.GetComponent<Patroller>().stopEndPath = true;

            MissionManager.instance.rpgTalk.NewTalk("M3MundoInvertido", "M3MundoInvertidoEnd");
        }
        else if (secao == enumMission.CORVO_APARECE)
        {
            MissionManager.instance.InvertWorld(true);
            MissionManager.instance.invertWorldBlocked = true;

            MissionManager.instance.scenerySounds.PlayBat(1);
            MissionManager.instance.scenerySounds.PlayDemon(4);
            MissionManager.instance.Invoke("InvokeMission", 3f);
        }
        else if (secao == enumMission.CORVO_ATACA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M3MundoInvertido2", "M3MundoInvertido2End", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog", false);

            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.GetComponent<Collider2D>().isTrigger = true;

            GameObject.Find("BirdEmitterHolder(Corredor)").gameObject.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
            MissionManager.instance.AddObject("PaperEmitter", "", new Vector3(6.8f, 0.68f, -0.5f), new Vector3(1, 1, 1));
        }
        else if (secao == enumMission.MI_TRAVADO)
        {
            if (MissionManager.instance.rpgTalk.isPlaying)
            {
                MissionManager.instance.rpgTalk.EndTalk();
            }

            MissionManager.instance.InvertWorld(false);
            MissionManager.instance.Invoke("InvokeMission", 15f);
        }
        else if (secao == enumMission.MAE_APARECE)
        {
            MissionManager.instance.scenerySounds.PlayDemon(6);

            MissionManager.instance.rpgTalk.NewTalk("M3VoltaMundoInvertido", "M3VoltaMundoInvertidoEnd");

            MissionManager.instance.AddObject("mom", "", new Vector3(-3.1f, 1f, -0.5f), new Vector3(0.3f, 0.3f, 1));

            GameObject portaG = GameObject.Find("DoorToGarden").gameObject;
            portaG.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (secao == enumMission.PASSAROS_FINAL)
        {
            MissionManager.instance.invertWorldBlocked = true;
            MissionManager.instance.Invoke("InvokeMission", 8f);
        }
        else if (secao == enumMission.FINAL)
        {
            GameObject porta = GameObject.Find("DoorToLivingRoom").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = true;

            GameObject.Find("BirdEmitterHolder(Jardim)").gameObject.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("AreaTrigger(Clone)") && secao == enumMission.GATO_CORREDOR)
        {
            EspecificaEnum((int) enumMission.QUADRO);
        }
    }

    public override void InvokeMission()
    {
        if (secao == enumMission.CORVO_APARECE)
        {

            MissionManager.instance.scenerySounds.PlayBird(4);
            EspecificaEnum((int)enumMission.CORVO_ATACA);
        }
        else if (secao == enumMission.MI_TRAVADO)
        {
            EspecificaEnum((int)enumMission.MAE_APARECE);
        }
        else if (secao == enumMission.PASSAROS_FINAL)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }
    }

    public override void InvokeMissionChoice(int id)
    {
        
    }

}