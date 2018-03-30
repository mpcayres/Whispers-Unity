using UnityEngine;

public class Mission2 : Mission {
    enum enumMission { NIGHT, INICIO_GATO, INICIO_SOZINHO, ENCONTRA_MAE, CONTESTA_MAE, CONTESTA_MAE2, RESPEITA_MAE, RESPEITA_MAE2,
        FINAL_CONTESTA, FINAL_CONTESTA_CORVO, FINAL_CONTESTA_GATO, FINAL_CONTESTA_ATAQUE,
        FINAL_RESPEITA, FINAL_RESPEITA_VELA, FINAL_RESPEITA_FOSFORO, FINAL };
    enumMission secao;

    GameObject vela, velaFixa, fosforo, faca, tampa;

    public override void InitMission()
    {
        sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float)1.5;
        MissionManager.initY = (float)-1.0;
        MissionManager.initDir = 1;
        MissionManager.LoadScene(sceneInit);
        secao = enumMission.FINAL_CONTESTA;//enumMission.NIGHT;
        Book.bookBlocked = true;

        MissionManager.instance.invertWorld = false;
        MissionManager.instance.invertWorldBlocked = true;

        SetInitialSettings();
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {
        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                if (MissionManager.instance.mission1AssustaGato)
                {
                    EspecificaEnum((int)enumMission.INICIO_SOZINHO);
                }
                else
                {
                    EspecificaEnum((int)enumMission.INICIO_GATO);
                }
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneStart", "M2KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
        }
        else if (secao == enumMission.RESPEITA_MAE2)
        {
            if(Inventory.HasItemType(Inventory.InventoryItems.VELA) && Inventory.HasItemType(Inventory.InventoryItems.FOSFORO)){
                EspecificaEnum((int)enumMission.FINAL_RESPEITA);
            }
        }
        else if(secao == enumMission.CONTESTA_MAE2)
        {
            if (Inventory.HasItemType(Inventory.InventoryItems.FACA) && Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
            {
                EspecificaEnum((int)enumMission.FINAL_CONTESTA);
            }
        }
        else if (secao == enumMission.FINAL_RESPEITA)
        {
            if (!Inventory.HasItemType(Inventory.InventoryItems.VELA))
            {
                EspecificaEnum((int)enumMission.FINAL_RESPEITA_VELA);
            }
        }
        else if (secao == enumMission.FINAL_RESPEITA_VELA)
        {
            if (fosforo.GetComponent<MiniGameObject>().achievedGoal)
            {
                EspecificaEnum((int)enumMission.FINAL_RESPEITA_FOSFORO);
            }
        }
    }

    public override void SetCorredor()
    {
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        if (MissionManager.instance.previousSceneName.Equals("QuartoKid") &&
            (secao == enumMission.CONTESTA_MAE2 || secao == enumMission.RESPEITA_MAE2))
        {
            MissionManager.instance.rpgTalk.NewTalk("M2CorridorSceneRepeat", "M2CorridorSceneRepeatEnd");
        }

        MissionManager.instance.scenerySounds.StopSound();
        if (secao == enumMission.INICIO_SOZINHO)
        {
            // Gato
            MissionManager.instance.AddObject("catFollower", "", new Vector3(10.8f, -0.3f, 0), new Vector3(0.15f, 0.15f, 1));
        }

        if (secao == enumMission.INICIO_SOZINHO || secao == enumMission.INICIO_GATO)
        {
            // Porta Cozinha
            GameObject portaCozinha = GameObject.Find("DoorToKitchen").gameObject;
            portaCozinha.GetComponent<SceneDoor>().isOpened = false;

            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.GetComponent<SceneDoor>().isOpened = false;

            // Porta Quarto Mae
            GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject; float portaMaeDefaultY = portaMae.transform.position.y;
            float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaMae.GetComponent<SceneDoor>().isOpened = false;
            portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

            // Mae
            MissionManager.instance.AddObject("mom", "", new Vector3(-1.5f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-1.5f, 0f, 1), new Vector3(1, 1, 1));
            trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            trigger.GetComponent<BoxCollider2D>().size = new Vector2(2f, 2f);
        }
        else if (secao == enumMission.CONTESTA_MAE2 || secao == enumMission.RESPEITA_MAE2 || 
            secao == enumMission.FINAL_CONTESTA || secao == enumMission.FINAL_RESPEITA)
        {
            // Porta Mae
            GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
            float portaMaeDefaultY = portaMae.transform.position.y;
            float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaMae.GetComponent<SceneDoor>().isOpened = false;
            portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

            // Mae patrulha
            GameObject mom = MissionManager.instance.AddObject("mom", "", new Vector3(-2f, -0.5f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            mom.GetComponent<Patroller>().isPatroller = true;
            Transform target1 = new GameObject().transform, target2 = new GameObject().transform, target3 = new GameObject().transform;
            Transform target4 = new GameObject().transform, target5 = new GameObject().transform, target6 = new GameObject().transform;
            Transform target7 = new GameObject().transform, target8 = new GameObject().transform, target9 = new GameObject().transform;
            target1.position = new Vector3(6f, -0.3f, -0.5f);
            target2.position = new Vector3(6f, 0.5f, -0.5f);
            target3.position = new Vector3(6f, -0.3f, -0.5f);
            if (Random.value > 0)
                target4.position = new Vector3(8f, -0.3f, -0.5f);
            else
                target5.position = new Vector3(7f, -0.3f, -0.5f);
            target6.position = new Vector3(-3f, -0.3f, -0.5f);
            if (Random.value > 0)
                target7.position = new Vector3(3f, -0.3f, -0.5f);
            else
                target8.position = new Vector3(5f, -0.3f, -0.5f);
            target9.position = new Vector3(-3f, -0.3f, -0.5f);
            Transform[] momTargets = { target1, target2, target3, target4, target5, target6, target7, target8, target9 };
            mom.GetComponent<Patroller>().targets = momTargets;
            MissionManager.instance.AddObject("ActionPatroller", "", new Vector3(0, 0, 0), new Vector3(0.7f, 0.7f, 1));
        }
    }

	public override void SetCozinha()
	{
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        MissionManager.instance.scenerySounds.PlayDrop();
        //MissionManager.instance.rpgTalk.NewTalk ("M2KitchenSceneStart", "M2KitchenSceneEnd");

        // Panela
        GameObject panela = GameObject.Find("Panela").gameObject;
        panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");

        GameObject armario = GameObject.Find("Armario1").gameObject;
        SceneObject sceneObject = armario.GetComponent<SceneObject>();

        if (secao == enumMission.CONTESTA_MAE2)
        {
            panela.GetComponent<ScenePickUpObject>().enabled = true;

            // Faca
            sceneObject.enabled = false;
            armario.tag = "ScenePickUpObject";
            ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = sceneObject.sprite1;
            scenePickUpObject.sprite2 = sceneObject.sprite2;
            scenePickUpObject.positionSprite = sceneObject.positionSprite;
            scenePickUpObject.scale = sceneObject.scale;
            scenePickUpObject.isUp = sceneObject.isUp;
            scenePickUpObject.item = Inventory.InventoryItems.FACA;
        }
        else if (secao == enumMission.RESPEITA_MAE2)
        {
            // Fosforo
            sceneObject.enabled = false;
            armario.tag = "ScenePickUpObject";
            ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = sceneObject.sprite1;
            scenePickUpObject.sprite2 = sceneObject.sprite2;
            scenePickUpObject.positionSprite = sceneObject.positionSprite;
            scenePickUpObject.scale = sceneObject.scale;
            scenePickUpObject.isUp = sceneObject.isUp;
            scenePickUpObject.item = Inventory.InventoryItems.FOSFORO;
        }
    }

	public override void SetJardim()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M2GardenSceneStart", "M2GardenSceneEnd");
	}

    public override void SetQuartoKid()
    {
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver") && 
            (secao == enumMission.FINAL_CONTESTA_CORVO || secao == enumMission.FINAL_CONTESTA_GATO))
        {
            secao = enumMission.FINAL_CONTESTA; // está fora do EspecificaEnum pq não é para chamar a fala de lá e aí ficava mais fácil
        }

        if ((secao == enumMission.NIGHT && !MissionManager.instance.mission1AssustaGato) || secao == enumMission.INICIO_GATO)
        {
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(2.5f, -0.8f, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }
        else if (secao == enumMission.RESPEITA_MAE)
        {
            EspecificaEnum((int)enumMission.RESPEITA_MAE2);
        }
        else if (secao == enumMission.CONTESTA_MAE)
        {
            EspecificaEnum((int)enumMission.CONTESTA_MAE2);
        }
        else if (secao == enumMission.RESPEITA_MAE2)
        {
            GameObject windowTrigger = GameObject.Find("WindowTrigger").gameObject;
            windowTrigger.tag = "WindowTrigger";
            WindowTrigger trigger = windowTrigger.GetComponent<WindowTrigger>();
            trigger.enabled = true;
            SceneObject sceneObject = windowTrigger.GetComponent<SceneObject>();
            sceneObject.enabled = false;

            if (!Inventory.HasItemType(Inventory.InventoryItems.VELA))
            {
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneVela", "M2KidRoomSceneVelaEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
            else if (!Inventory.HasItemType(Inventory.InventoryItems.FOSFORO))
            {
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneFosforo", "M2KidRoomSceneFosforoEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
        }
        else if (secao == enumMission.CONTESTA_MAE2)
        {
            if (!Inventory.HasItemType(Inventory.InventoryItems.FACA))
            {
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneFaca", "M2KidRoomSceneFacaEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
            else if (!Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
            {
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneTampa", "M2KidRoomSceneTampaEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
        }
        else if (secao == enumMission.FINAL_RESPEITA || secao == enumMission.FINAL_CONTESTA)
        {
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            float portaDefaultY = porta.transform.position.y;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.GetComponent<SceneDoor>().isOpened = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);

            if (secao == enumMission.FINAL_RESPEITA)
            {
                // Mini-game vela
                vela = GameObject.Find("Player").gameObject.transform.Find("Vela").gameObject;
                GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(0.125f, -1.38f, 0), new Vector3(1, 1, 1));
                trigger.name = "VelaTrigger";
                trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
                trigger.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1f);

                fosforo = GameObject.Find("Player").gameObject.transform.Find("Fosforo").gameObject;

                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneRepeat", "M2KidRoomSceneRepeatEnd", MissionManager.instance.rpgTalk.txtToParse);;
            }
            else if (secao == enumMission.FINAL_CONTESTA)
            {
                // Corvo atacando
                faca = GameObject.Find("Player").gameObject.transform.Find("Faca").gameObject;
                tampa = GameObject.Find("Player").gameObject.transform.Find("Tampa").gameObject;
                MissionManager.instance.Print("CORVO");
                GameObject corvo = MissionManager.instance.AddObject("CorvBabies", "", new Vector3(-1.97f, 1.42f, -0.5f), new Vector3(3f, 3f, 1));
                corvo.GetComponent<CorvBabies>().speed = 0.1f;
                corvo.GetComponent<CorvBabies>().timeBirdsFollow = 0.5f;
                var main = corvo.transform.Find("BirdEmitterCollider").gameObject.GetComponent<ParticleSystem>().main;
                main.startSpeed = 1.5f;

                MissionManager.instance.Invoke("InvokeMission", 5f);
            }
        }

    }

    public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M2MomRoomSceneStart", "M2MomRoomSceneEnd");
	}

    public override void SetSala()
    {
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        if (secao == enumMission.RESPEITA_MAE2)
        {
            // Vela
            GameObject armario = GameObject.Find("Armario").gameObject;
            armario.tag = "ScenePickUpObject";
            SceneObject sceneObject = armario.GetComponent<SceneObject>();
            sceneObject.enabled = false;
            ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = sceneObject.sprite1;
            scenePickUpObject.sprite2 = sceneObject.sprite2;
            scenePickUpObject.positionSprite = sceneObject.positionSprite;
            scenePickUpObject.scale = sceneObject.scale;
            scenePickUpObject.isUp = sceneObject.isUp;
            scenePickUpObject.item = Inventory.InventoryItems.VELA;
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.ENCONTRA_MAE)
        {
            MissionManager.instance.rpgTalk.NewTalk ("M2CorridorSceneStart", "M2CorridorSceneEnd", MissionManager.instance.rpgTalk.txtToParse);;
        }
        else if (secao == enumMission.RESPEITA_MAE)
        {
            MissionManager.instance.mission2ContestaMae = false;
        }
        else if (secao == enumMission.CONTESTA_MAE)
        {
            MissionManager.instance.mission2ContestaMae = true;
        }
        else if (secao == enumMission.RESPEITA_MAE2)
        {
            MissionManager.instance.rpgTalk.NewTalk("M2Q1C1_2", "M2Q1C1_2End", MissionManager.instance.rpgTalk.txtToParse);;
        }
        else if (secao == enumMission.CONTESTA_MAE2)
        {
            MissionManager.instance.rpgTalk.NewTalk("M2Q1C0_2", "M2Q1C0_2End", MissionManager.instance.rpgTalk.txtToParse);;
        }
        else if (secao == enumMission.FINAL_RESPEITA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M2AllObjectsRespeita", "M2AllObjectsRespeitaEnd", MissionManager.instance.rpgTalk.txtToParse);;
        }
        else if (secao == enumMission.FINAL_RESPEITA_VELA)
        {
            velaFixa = MissionManager.instance.AddObject("EmptyObject", "", new Vector3(0.125f, -1.1f, 0), new Vector3(2.5f, 2.5f, 1));
            velaFixa.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/vela");
            velaFixa.GetComponent<SpriteRenderer>().sortingOrder = 140;

            GameObject.Find("Player").gameObject.transform.Find("Fosforo").gameObject.GetComponent<MiniGameObject>().posFlareX = 0.125f;
            GameObject.Find("Player").gameObject.transform.Find("Fosforo").gameObject.GetComponent<MiniGameObject>().posFlareY = -1.05f;
        }
        else if (secao == enumMission.FINAL_RESPEITA_FOSFORO)
        {
            velaFixa.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/vela_acesa1");
            GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true);
            MissionManager.instance.Invoke("InvokeMission", 4f);
        }
        else if (secao == enumMission.FINAL_CONTESTA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M2AllObjectsContesta", "M2AllObjectsContestaEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.FINAL_CONTESTA_CORVO)
        {
            MissionManager.instance.scenerySounds.PlayBird(1);
            //CorvBabies.instance.GetComponent<CorvBabies>().FollowPlayer();
            CorvBabies.instance.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
            MissionManager.instance.Invoke("InvokeMission", 40f);
        }
        else if (secao == enumMission.FINAL_CONTESTA_GATO)
        {
            CorvBabies.instance.transform.Find("BirdEmitterCollider").gameObject.SetActive(false);
            CorvBabies.instance.Stop();

            Cat.instance.followWhenClose = false;
            Cat.instance.stopEndPath = true;
            Cat.instance.Patrol();
            Transform target1 = new GameObject().transform, target2 = new GameObject().transform;
            Vector3 posCorvo = CorvBabies.instance.transform.position;
            target1.position = posCorvo;
            target2.position = new Vector3(-2f, 0.6f, -0.5f);
            Transform[] targets = { target1, target2 };
            Cat.instance.targets = targets;

            CorvBabies.instance.Patrol();
            Transform[] targetsCorvo = { target2 };
            CorvBabies.instance.targets = targetsCorvo;
            CorvBabies.instance.speed = 0.6f;

            MissionManager.instance.rpgTalk.NewTalk("M2AtaqueContesta", "M2AtaqueContestaEnd", MissionManager.instance.rpgTalk.txtToParse);;

            MissionManager.instance.Invoke("InvokeMission", 2.5f);
        }
        else if (secao == enumMission.FINAL_CONTESTA_ATAQUE)
        {
            MissionManager.instance.AddObject("Garra", "", new Vector3(-1.48f, 1.81f, 0), new Vector3(0.1f, 0.1f, 1));
            CorvBabies.instance.DestroyCorvBabies();
            Cat.instance.Stop();
            MissionManager.instance.Invoke("InvokeMission", 6f);
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.ChangeMission(3);
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("AreaTrigger(Clone)") && (secao == enumMission.INICIO_GATO || secao == enumMission.INICIO_SOZINHO))
        {
            EspecificaEnum((int)enumMission.ENCONTRA_MAE);
        }
        else if (secao == enumMission.FINAL_RESPEITA)
        {
            if (tag.Equals("EnterVelaTrigger"))
            {
                vela.GetComponent<PlaceObject>().inArea = true;
            }
            else if (tag.Equals("ExitVelaTrigger"))
            {
                vela.GetComponent<PlaceObject>().inArea = false;
            }
        }
        else if (secao == enumMission.FINAL_RESPEITA_VELA)
        {
            if (tag.Equals("EnterVelaTrigger"))
            {
                fosforo.GetComponent<MiniGameObject>().activated = true;
            }
            else if (tag.Equals("ExitVelaTrigger"))
            {
                fosforo.GetComponent<MiniGameObject>().activated = false;
            }
        }
    }

    public override void InvokeMission()
    {
        if (secao == enumMission.FINAL_RESPEITA_FOSFORO)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }
        else if (secao == enumMission.FINAL_CONTESTA)
        {
            EspecificaEnum((int)enumMission.FINAL_CONTESTA_CORVO);
        }
        else if (secao == enumMission.FINAL_CONTESTA_CORVO)
        {
         //   EspecificaEnum((int)enumMission.FINAL_CONTESTA_GATO); //!!!!
        }
        else if (secao == enumMission.FINAL_CONTESTA_GATO)
        {
            EspecificaEnum((int)enumMission.FINAL_CONTESTA_ATAQUE);
        }
        else if (secao == enumMission.FINAL_CONTESTA_ATAQUE)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }
    }

    public override void InvokeMissionChoice(int id)
    {
        if (secao == enumMission.ENCONTRA_MAE)
        {
            if (id == 0)
            {
                EspecificaEnum((int)enumMission.CONTESTA_MAE);
            }
            else
            {
                EspecificaEnum((int)enumMission.RESPEITA_MAE);
            }
        }
    }

}