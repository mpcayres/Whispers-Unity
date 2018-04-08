using UnityEngine;

public class Mission6 : Mission {
    enum enumMission { NIGHT, INICIO, LIGOULANTERNA, DESLIGOULANTERNA, SALA, GATOCOMFOLHA, DICA,
        ACENDE, TENTAOUTRAARVORE, TENTOUARVORECERTA, TEMTUDO, NAOTEMTUDO, TEMTUDOFORAJARDIM, FOGO, CHECOUMAE, FINAL };
    enumMission secao;

    bool arvore1Trigger = false, arvoreOtherTrigger = false;
    bool arvoreBurn = false, doorSet = false;
    GameObject player, fosforo, isqueiro, fireEvent;

    public override void InitMission()
	{
		sceneInit = "QuartoMae";
		MissionManager.initMission = true;
		MissionManager.initX = (float) 3;
		MissionManager.initY = (float) 0.2;
		MissionManager.initDir = 3;
		MissionManager.LoadScene(sceneInit);
        secao = enumMission.NIGHT;
        Book.bookBlocked = false;

        MissionManager.instance.invertWorld = true;
        MissionManager.instance.invertWorldBlocked = true;

        SetInitialSettings();

        player = GameObject.Find("Player").gameObject;
        fosforo = player.transform.Find("Fosforo").gameObject;
        isqueiro = player.transform.Find("Isqueiro").gameObject;

        fosforo.GetComponent<MiniGameObject>().posFlareX = -1.37f;
        fosforo.GetComponent<MiniGameObject>().posFlareY = 3.51f;
        isqueiro.GetComponent<MiniGameObject>().posFlareX = -1.37f;
        isqueiro.GetComponent<MiniGameObject>().posFlareY = 3.51f;
    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{
        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.showMissionStart)
            {
                EspecificaEnum((int)enumMission.INICIO);
            }
        }
        if (secao == enumMission.INICIO && Flashlight.GetState())
        {
            EspecificaEnum((int)enumMission.LIGOULANTERNA);
        }

        if (secao == enumMission.LIGOULANTERNA && !Flashlight.GetState())
        {
            EspecificaEnum((int)enumMission.DESLIGOULANTERNA);
        }

        if (secao == enumMission.DESLIGOULANTERNA && MissionManager.instance.currentSceneName.Equals("Sala"))
        {
            EspecificaEnum((int)enumMission.SALA);
        }
        if(secao == enumMission.SALA)//&& Spirit.maxEvilKilled < 2)
        {
            EspecificaEnum((int)enumMission.DICA);
        }
        if ((secao == enumMission.SALA || secao == enumMission.DICA)
            && MissionManager.instance.currentSceneName.Equals("Jardim"))
        {
            EspecificaEnum((int)enumMission.GATOCOMFOLHA);
        }
        if ((secao == enumMission.SALA || secao == enumMission.DICA) &&
            !doorSet && MissionManager.instance.currentSceneName.Equals("Sala"))
        {
            GameObject portaJardim = GameObject.Find("DoorToGarden").gameObject;
            portaJardim.GetComponent<SceneDoor>().isOpened = true;
            doorSet = true;
        }
        
        if (secao == enumMission.GATOCOMFOLHA && arvoreOtherTrigger)
        {
            EspecificaEnum((int)enumMission.TENTAOUTRAARVORE);
        }

        if ((secao == enumMission.GATOCOMFOLHA || secao == enumMission.TENTAOUTRAARVORE) && arvore1Trigger)
        {
            EspecificaEnum((int)enumMission.TENTOUARVORECERTA);
        }

        if(secao == enumMission.NAOTEMTUDO && (Inventory.HasItemType(Inventory.InventoryItems.FOSFORO)
            || Inventory.HasItemType(Inventory.InventoryItems.ISQUEIRO)) && !MissionManager.instance.currentSceneName.Equals("Jardim"))
        {
            EspecificaEnum((int)enumMission.TEMTUDOFORAJARDIM);
            arvore1Trigger = false;
        }
        if(secao == enumMission.TEMTUDOFORAJARDIM && MissionManager.instance.currentSceneName.Equals("Jardim") && arvore1Trigger)
        {
            EspecificaEnum((int)enumMission.TEMTUDO);
        }

        if (secao == enumMission.TEMTUDO)
        {
            if (fosforo.GetComponent<MiniGameObject>().achievedGoal || isqueiro.GetComponent<MiniGameObject>().achievedGoal)
            {
                fosforo.GetComponent<MiniGameObject>().achievedGoal = false;
                isqueiro.GetComponent<MiniGameObject>().achievedGoal = false;

                fosforo.GetComponent<MiniGameObject>().activated = false;
                isqueiro.GetComponent<MiniGameObject>().activated = false;

                if (arvore1Trigger)
                {
                    arvoreBurn = true;
                    fireEvent = GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventTree").gameObject;
                    fireEvent.SetActive(true);
                    EspecificaEnum((int)enumMission.FOGO);
                }
            }
        }

        if(secao == enumMission.FOGO)
        {
            EspecificaEnum((int)enumMission.CHECOUMAE);
        }

        if(secao == enumMission.CHECOUMAE && !MissionManager.instance.invertWorld)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }

        if(secao == enumMission.FINAL && !MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.InvertWorld(false);
            MissionManager.instance.ChangeMission(7);
        }
    }

	public override void SetCorredor()
	{
        MissionManager.instance.scenerySounds.StopSound();

        GameObject.Find("VasoNaoEmpurravel").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/vasoPlanta_quebrado");
        //MissionManager.instance.rpgTalk.NewTalk ("M6CorridorSceneStart", "M6CorridorSceneEnd");
    }

	public override void SetCozinha()
	{
        MissionManager.instance.scenerySounds.PlayDrop();
        //MissionManager.instance.rpgTalk.NewTalk ("M6KitchenSceneStart", "M6KitchenSceneEnd");

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
        //MissionManager.instance.rpgTalk.NewTalk ("M6GardenSceneStart", "M6GardenSceneEnd");
        MissionManager.instance.AddObject("Scenery/Leafs", "", new Vector3(1.8f, 2.4f, -0.5f), new Vector3(1f, 1f, 1));
        MissionManager.instance.AddObject("NPCs/catFollower", "", new Vector3(2f, 1.5f, -0.5f), new Vector3(0.15f, 0.15f, 1));

        GameObject triggerArv1 = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(-1.379f, 3.51f, 0), new Vector3(1, 1, 1));
        triggerArv1.name = "Arv1Trigger";
        triggerArv1.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        triggerArv1.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 2f);

        GameObject triggerArv2 = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(-5.02f, 3.37f, 0), new Vector3(1, 1, 1));
        triggerArv2.name = "Arv2Trigger";
        triggerArv2.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        triggerArv2.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 2.5f);

        GameObject triggerArv3 = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(-4.37f, -2.12f, 0), new Vector3(1, 1, 1));
        triggerArv3.name = "Arv3Trigger";
        triggerArv3.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        triggerArv3.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 2f);

        GameObject triggerArv4 = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(4.91f, -1.48f, 0), new Vector3(1, 1, 1));
        triggerArv4.name = "Arv4Trigger";
        triggerArv4.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        triggerArv4.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 2.5f);

        if (secao == enumMission.FOGO)
        {
            fireEvent = GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventTree").gameObject;
            fireEvent.SetActive(true);
        }
    }

    public override void SetQuartoKid()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M6KidRoomSceneStart", "M6KidRoomSceneEnd");

        if (MissionManager.instance.mission2ContestaMae)
        {
            // Arranhao
            MissionManager.instance.AddObject("Scenery/Garra", "", new Vector3(-1.48f, 1.81f, 0), new Vector3(0.1f, 0.1f, 1));
        }
        else
        {
            // Vela
            GameObject velaFixa = MissionManager.instance.AddObject("Objects/EmptyObject", "", new Vector3(0.125f, -1.1f, 0), new Vector3(2.5f, 2.5f, 1));
            velaFixa.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Inventory/vela_acesa1");
            velaFixa.GetComponent<SpriteRenderer>().sortingOrder = 140;
            GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true);
        }
    }

	public override void SetQuartoMae()
	{
        MissionManager.instance.scenerySounds.StopSound();

        if (Cat.instance == null)
        {
            // Gato
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "NPCs/catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.InvertWorld(true);
        }

        GameObject portaCorredor = GameObject.Find("DoorToAlley").gameObject;
        portaCorredor.GetComponent<SceneDoor>().isOpened = false;

        GameObject camaMae = GameObject.Find("Cama").gameObject;
        camaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/camaMaeDoente");

        if (secao != enumMission.FOGO)
        {
            MissionManager.instance.AddObject("Scenery/Gloom", "", new Vector3(4.26f, 1.98f, 0), new Vector3(1f, 1f, 1));
        }
        else
        {
            GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
            mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

            MissionManager.instance.AddObject("Objects/Pagina", "", new Vector3(3.59f, 1.73f, 0), new Vector3(0.535f, 0.483f, 1));
        }
    }


    public override void SetSala()
	{
        //MissionManager.instance.AddObject("Objects/MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        //GameObject.Find("PickUpLanterna").gameObject.SetActive(false);
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        if (Cat.instance == null)
        {
            // Gato
            GameObject player = GameObject.Find("Player").gameObject;
            GameObject cat = MissionManager.instance.AddObject(
                "NPCs/catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.InvertWorld(true);
        }

        GameObject armario = GameObject.Find("escrivaninha").gameObject;
        SceneObject sceneObject = armario.GetComponent<SceneObject>();
        armario.tag = "ScenePickUpObject";
        ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
        scenePickUpObject.sprite1 = sceneObject.sprite1;
        scenePickUpObject.sprite2 = sceneObject.sprite2;
        scenePickUpObject.positionSprite = sceneObject.positionSprite;
        scenePickUpObject.scale = sceneObject.scale;
        scenePickUpObject.isUp = sceneObject.isUp;
        scenePickUpObject.item = Inventory.InventoryItems.ISQUEIRO;

        if (secao == enumMission.DESLIGOULANTERNA || secao == enumMission.DICA || secao == enumMission.SALA)
        {

            //if (!Book.pages[2])
            //{
                GameObject portaJardim = GameObject.Find("DoorToGarden").gameObject;
                portaJardim.GetComponent<SceneDoor>().isOpened = false;

                MissionManager.instance.AddObject("Objects/Pagina", "", new Vector3(6.2f, 0f, 0), new Vector3(0.535f, 0.483f, 1));
            //}

            //SpiritManager.GenerateSpiritMap();
        }
        
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.INICIO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6MomRoomSceneStart", "M6MomRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.LIGOULANTERNA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6AfterFlashlight", "M6AfterFlashlightEnd", MissionManager.instance.rpgTalk.txtToParse);

        }
        else if (secao == enumMission.DESLIGOULANTERNA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6AfterFlashlightShutdown", "M6AfterFlashlightShutdownEnd", MissionManager.instance.rpgTalk.txtToParse);
            GameObject portaCorredor = GameObject.Find("DoorToAlley").gameObject;
            portaCorredor.GetComponent<SceneDoor>().isOpened = true;
        }
        else if (secao == enumMission.SALA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6LivingroomSceneStart", "M6LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.DICA)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6LivingroomTipStart", "M6LivingroomTipEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.GATOCOMFOLHA)
        {
            MissionManager.instance.scenerySounds.PlayCat(3);
            MissionManager.instance.rpgTalk.NewTalk("M6GardenSceneStart", "M6GardenSceneEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.TENTAOUTRAARVORE)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6IncorrectTree", "M6IncorrectTreeEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.TENTOUARVORECERTA)
        {
            if (Inventory.HasItemType(Inventory.InventoryItems.FOSFORO) || Inventory.HasItemType(Inventory.InventoryItems.ISQUEIRO))
                EspecificaEnum((int)enumMission.TEMTUDO);
            else
                EspecificaEnum((int)enumMission.NAOTEMTUDO);
        }
        else if (secao == enumMission.TEMTUDO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6HasEverything", "M6HasEverythingEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.NAOTEMTUDO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6HasntEverything", "M6HasntEverythingEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.FOGO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6FireTree", "M6FireTreeEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.CHECOUMAE)
        {
            MissionManager.instance.invertWorldBlocked = false;
            MissionManager.instance.rpgTalk.NewTalk("M6BeforeFinal", "M6BeforeFinalEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.rpgTalk.NewTalk("M6Final", "M6FinalEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("EnterArv1Trigger") && !arvoreBurn)
        {
            arvore1Trigger = true;
            fosforo.GetComponent<MiniGameObject>().activated = true;
            isqueiro.GetComponent<MiniGameObject>().activated = true;
        }
        else if (tag.Equals("ExitArv1Trigger") && !arvoreBurn)
        {
            arvore1Trigger = false;
            fosforo.GetComponent<MiniGameObject>().activated = false;
            isqueiro.GetComponent<MiniGameObject>().activated = false;
        }
        else if (tag.Equals("EnterArv2Trigger") || tag.Equals("EnterArv3Trigger") || tag.Equals("EnterArv4Trigger"))
        {
            arvoreOtherTrigger = true;
        }
        else if (tag.Equals("ExitArv2Trigger") || tag.Equals("ExitArv3Trigger") || tag.Equals("ExitArv4Trigger"))
        {
            arvoreOtherTrigger = false;
        }
        
    }
}