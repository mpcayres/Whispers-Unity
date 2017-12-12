using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mission8 : Mission {
    enum enumMission { NIGHT, INICIO, CORVO_APARECE_CAT, CORVO_ATACA_CAT_INIT, CORVO_ATACA_CAT, MAE_CAT, FINAL_CAT,
        CORVO_APARECE_BIRD, CORVO_ATACA_BIRD_INIT, CORVO_ATACA_BIRD, BOTIJAO_BIRD, FINAL_BIRD, FINAL };
    enumMission secao;

    bool hasPanela = false, endCat = false, invertLocal = false, gameOverSet = false;
    bool estanteTrigger = false, poltronaTrigger = false, sofaTrigger = false;
    bool estanteBurn = false, poltronaBurn = false, sofaBurn = false;
    bool falaMae = false, falaGato = false;
    bool changed = false;
    float portaDefaultX, portaDefaultY;
    Book book;
    GameObject player, fosforo, isqueiro, faca, pedra, luminaria, fireEvent;

    bool birdsActive = false;

    // ANALISAR DIFICULDADE DO NIVEL E DOS DIFERENTES OBJETOS - FACA, PEDRO, FOSFORO, ISQUEIRO
    public override void InitMission()
    {
        sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float) 1.5;
        MissionManager.initY = (float) 0.2;
        MissionManager.initDir = 3;
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();
        Book.bookBlocked = false;

        MissionManager.instance.invertWorld = false;
        MissionManager.instance.invertWorldBlocked = false;

        // Adiciona todas as páginas
        Book.pageQuantity = 5;
        bool[] pages = { true, true, true, true, true };
        Book.pages = pages;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        hasPanela = Inventory.HasItemType(Inventory.InventoryItems.TAMPA);
        if (MissionManager.instance.pathCat >= MissionManager.instance.pathBird) endCat = true;
        book = GameObject.Find("Player").gameObject.GetComponent<Book>();
        player = GameObject.Find("Player").gameObject;
        fosforo = player.transform.Find("Fosforo").gameObject;
        isqueiro = player.transform.Find("Isqueiro").gameObject;
        faca = player.transform.Find("Faca").gameObject;
        pedra = player.transform.Find("Pedra").gameObject;
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {

        if (MissionManager.instance.invertWorld && !invertLocal)
        {
            invertLocal = true;
            GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }
        else if (!MissionManager.instance.invertWorld && invertLocal)
        {
            invertLocal = false;
            GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
            mainLight.transform.Rotate(new Vector3(40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                EspecificaEnum((int)enumMission.INICIO);
            }
        }
        else if (secao == enumMission.INICIO)
        {
            if (book.SeenAll())
            {
                if (endCat)
                {
                    EspecificaEnum((int)enumMission.CORVO_APARECE_CAT);
                }
                else
                {
                    EspecificaEnum((int)enumMission.CORVO_APARECE_BIRD);
                }
            }
        }
        else if (secao == enumMission.CORVO_APARECE_CAT)
        {
            if (!MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.CORVO_ATACA_CAT_INIT);
            }
        }
        else if (secao == enumMission.CORVO_APARECE_BIRD)
        {
            if (!MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.CORVO_ATACA_BIRD_INIT);
            }
        }
        else if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT)
        {
            if (fosforo.GetComponent<MiniGameObject>().achievedGoal || isqueiro.GetComponent<MiniGameObject>().achievedGoal)
            {
                fosforo.GetComponent<MiniGameObject>().achievedGoal = false;
                isqueiro.GetComponent<MiniGameObject>().achievedGoal = false;

                fosforo.GetComponent<MiniGameObject>().activated = false;
                isqueiro.GetComponent<MiniGameObject>().activated = false;

                if (estanteTrigger)
                {
                    estanteBurn = true;
                    fireEvent.transform.Find("FogoEstante2").gameObject.SetActive(true);
                }
                if (poltronaTrigger)
                {
                    poltronaBurn = true;
                    fireEvent.transform.Find("FogoPoltrona").gameObject.SetActive(true);
                }
                if (sofaTrigger)
                {
                    sofaBurn = true;
                    fireEvent.transform.Find("FogoSofa").gameObject.SetActive(true);
                }

                if (estanteBurn && poltronaBurn && sofaBurn)
                {
                    EspecificaEnum((int)enumMission.FINAL_CAT);
                }
                else if ((estanteBurn && poltronaBurn) ||
                    (estanteBurn && sofaBurn) ||
                    (poltronaBurn && sofaBurn))
                {
                    EspecificaEnum((int)enumMission.MAE_CAT);
                }
            }
        }
        else if (secao == enumMission.CORVO_ATACA_BIRD)
        {
            if (faca.GetComponent<MiniGameObject>().achievedGoal || pedra.GetComponent<MiniGameObject>().achievedGoal)
            {
                faca.GetComponent<MiniGameObject>().achievedGoal = false;
                pedra.GetComponent<MiniGameObject>().achievedGoal = false;

                faca.GetComponent<MiniGameObject>().activated = false;
                pedra.GetComponent<MiniGameObject>().activated = false;

                EspecificaEnum((int) enumMission.BOTIJAO_BIRD);
            }
        }
        else if (secao == enumMission.BOTIJAO_BIRD)
        {
            if (MissionManager.instance.currentSceneName.Equals("Corredor") && luminaria != null)
            {
                if (luminaria.GetComponent<Lamp>().Changed())
                {
                    MissionManager.instance.mission8BurnCorredor = true;
                    EspecificaEnum((int)enumMission.FINAL_BIRD);
                }
            }
            else if (MissionManager.instance.currentSceneName.Equals("QuartoMae") && luminaria != null)
            {
                if (luminaria.GetComponent<Lamp>().Changed())
                {
                    MissionManager.instance.mission8BurnCorredor = false;
                    EspecificaEnum((int)enumMission.FINAL_BIRD);
                }
            }
        }
        if(GameObject.Find("BirdEmitterCollider"))
            birdsActive = GameObject.Find("BirdEmitterCollider").gameObject.activeInHierarchy;
        if (birdsActive && !MissionManager.instance.scenerySounds.source.isPlaying)
        {
            float value = Random.value;
            if (value > 0)
                MissionManager.instance.scenerySounds.PlayBird(4);
            else
                MissionManager.instance.scenerySounds.PlayBird(1);

        }

    }

    public override void SetCorredor()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        MissionManager.instance.scenerySounds.StopSound();

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        GameObject.Find("VasoNaoEmpurravel").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/vasoPlanta_quebrado");

        if (secao == enumMission.BOTIJAO_BIRD)
        {
            // Luminaria com faisca
            luminaria = GameObject.Find("Luminaria").gameObject;

            // FireEvent
            fireEvent = GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject;
        }
    }

    public override void SetCozinha()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        MissionManager.instance.scenerySounds.PlayDrop();

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            faca.GetComponent<MiniGameObject>().StopMiniGame();
            pedra.GetComponent<MiniGameObject>().StopMiniGame();

            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        // Panela para caso ainda não tenha
        if (!hasPanela)
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panela.GetComponent<ScenePickUpObject>().enabled = true;
        }

        if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT)
        {
            // Isqueiro
            GameObject armario = GameObject.Find("Armario2").gameObject;
            SceneObject sceneObject = armario.GetComponent<SceneObject>();
            armario.tag = "ScenePickUpObject";
            ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = sceneObject.sprite1;
            scenePickUpObject.sprite2 = sceneObject.sprite2;
            scenePickUpObject.positionSprite = sceneObject.positionSprite;
            scenePickUpObject.scale = sceneObject.scale;
            scenePickUpObject.isUp = sceneObject.isUp;
            scenePickUpObject.item = Inventory.InventoryItems.ISQUEIRO;
        }
        else if (secao == enumMission.CORVO_ATACA_BIRD)
        {
            // Botijao
            GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-4.1f, 1f, 0), new Vector3(1, 1, 1));
            trigger.name = "GasTrigger";
            trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            trigger.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }
    }

    public override void SetJardim()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        // Pedra, caso não tenha
        if (!Inventory.HasItemType(Inventory.InventoryItems.PEDRA))
        {
            GameObject pedra1 = GameObject.Find("monte_pedra").gameObject;
            pedra1.tag = "ScenePickUpObject";
            ScenePickUpObject scenePickUpObject = pedra1.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = pedra1.GetComponent<SpriteRenderer>().sprite;
            scenePickUpObject.sprite2 = pedra1.GetComponent<SpriteRenderer>().sprite;
            scenePickUpObject.blockAfterPick = true;
            scenePickUpObject.item = Inventory.InventoryItems.PEDRA;

            GameObject pedra2 = GameObject.Find("monte_pedra (1)").gameObject;
            pedra2.tag = "ScenePickUpObject";
            ScenePickUpObject scenePickUpObject2 = pedra2.AddComponent<ScenePickUpObject>();
            scenePickUpObject2.sprite1 = pedra2.GetComponent<SpriteRenderer>().sprite;
            scenePickUpObject2.sprite2 = pedra2.GetComponent<SpriteRenderer>().sprite;
            scenePickUpObject2.blockAfterPick = true;
            scenePickUpObject2.item = Inventory.InventoryItems.PEDRA;

            GameObject pedra = MissionManager.instance.AddObject("PickUp", "Sprites/Objects/Inventory/pedra", new Vector3((float)-3.59, (float)-0.45, 0), new Vector3((float)1.2, (float)1.2, 1.3f));
            pedra.GetComponent<PickUpObject>().item = Inventory.InventoryItems.PEDRA;
        }

        if (!Inventory.HasItemType(Inventory.InventoryItems.FACA) && !Inventory.HasItemType(Inventory.InventoryItems.PEDRA))
        {
            MissionManager.instance.rpgTalk.NewTalk("M8PedraJardim", "M8PedraJardimEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
        }

        if (secao == enumMission.FINAL_CAT)
        {
            EspecificaEnum((int) enumMission.FINAL);
        }
    }

    public override void SetQuartoKid()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

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

        if (secao == enumMission.NIGHT || secao == enumMission.INICIO)
        {
            // Porta momentaneamente travada
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            portaDefaultX = porta.transform.position.x;
            portaDefaultY = porta.transform.position.y;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.GetComponent<Collider2D>().isTrigger = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);
        }
    }

    public override void SetQuartoMae()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        // Mae sumida
        GameObject triggerM = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-2.21f, -3.39f, 0), new Vector3(1, 1, 1));
        triggerM.name = "MaeTrigger";
        triggerM.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        triggerM.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1f);

        if (secao == enumMission.BOTIJAO_BIRD)
        {
            // Luminaria com faisca
            luminaria = GameObject.Find("Luminaria").gameObject;

            // FireEvent
            fireEvent = GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject;
        }
    }

    public override void SetSala()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.invertWorld)
        {
            invertLocal = true;
            mainLight.transform.Rotate(new Vector3(-40, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        }

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            fosforo.GetComponent<MiniGameObject>().StopMiniGame();
            isqueiro.GetComponent<MiniGameObject>().StopMiniGame();

            gameOverSet = true;
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT)
        {
            MissionManager.instance.Invoke("InvokeMission", 1.2f);

            estanteTrigger = poltronaTrigger = sofaTrigger = false;

            // Estante
            GameObject triggerE = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-5.71f, 1.64f, 0), new Vector3(1, 1, 1));
            triggerE.name = "EstanteTrigger";
            triggerE.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            triggerE.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1.6f);

            // Poltrona
            GameObject triggerP = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-1f, 1.6f, 0), new Vector3(1, 1, 1));
            triggerP.name = "PoltronaTrigger";
            triggerP.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            triggerP.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1.2f);

            // Sofa
            GameObject triggerS = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(5.53f, 0.4f, 0), new Vector3(1, 1, 1));
            triggerS.name = "SofaTrigger";
            triggerS.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            triggerS.GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 1f);

            // FireEvent
            fireEvent = GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject;
            fireEvent.SetActive(true);
            fireEvent.transform.Find("FumacaArmario").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoArmario").gameObject.SetActive(false);
            fireEvent.transform.Find("FumacaSofa").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoSofa").gameObject.SetActive(false);
            fireEvent.transform.Find("FumacaCriado").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoCriado").gameObject.SetActive(false);
            fireEvent.transform.Find("FumacaEstante").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoEstante1").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoEstante2").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoEstante3").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoCadeira").gameObject.SetActive(false);
            fireEvent.transform.Find("FumacaPoltrona").gameObject.SetActive(false);
            fireEvent.transform.Find("FogoPoltrona").gameObject.SetActive(false);

            if (estanteBurn)
            {
                fireEvent.transform.Find("FogoEstante2").gameObject.SetActive(true);
            }
            if (poltronaBurn)
            {
                fireEvent.transform.Find("FogoPoltrona").gameObject.SetActive(true);
            }
            if (sofaBurn)
            {
                fireEvent.transform.Find("FogoSofa").gameObject.SetActive(true);
            }

            if (Cat.instance == null)
            {
                // Gato
                GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(2.5f, -1.3f, 0), new Vector3(0.15f, 0.15f, 1));
                cat.GetComponent<Cat>().speed = 2.4f;
                cat.GetComponent<Cat>().FollowPlayer();
            }
        }
        else if (secao == enumMission.CORVO_ATACA_BIRD || secao == enumMission.BOTIJAO_BIRD)
        {
            // Fala sobre gato sumido
            GameObject triggerG = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(-4.96f, -2f, 0), new Vector3(1, 1, 1));
            triggerG.name = "GatoTrigger";
            triggerG.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            triggerG.GetComponent<BoxCollider2D>().size = new Vector2(6f, 1f);
            
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if(secao == enumMission.INICIO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8KidRoomSceneStart", "M8KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
        }
        else if (secao == enumMission.CORVO_APARECE_CAT)
        {
            MissionManager.instance.rpgTalk.NewTalk("Dica8PC", "Dica8PCEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);

            CreateCorvoCat();

            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (secao == enumMission.CORVO_APARECE_BIRD)
        {
            MissionManager.instance.rpgTalk.NewTalk("Dica8PB", "Dica8PBEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);

            CreateCorvoBird();

            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (secao == enumMission.CORVO_ATACA_CAT_INIT || secao == enumMission.CORVO_ATACA_BIRD_INIT)
        {
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = true;
            MissionManager.instance.scenerySounds2.PlayDoorOpen(1);
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-opened");
            porta.transform.position = new Vector3(portaDefaultX, portaDefaultY, porta.transform.position.z);

            MissionManager.instance.Invoke("InvokeMission", 3f);
        }
        else if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.CORVO_ATACA_BIRD)
        {
            if (Corvo.instance != null) {
                Corvo.instance.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
                Corvo.instance.FollowPlayer();
            }
        }
        else if (secao == enumMission.MAE_CAT)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8MomCat", "M8MomCatEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);

            MissionManager.instance.AddObject("mom", "", new Vector3(-3.1f, 1f, -0.5f), new Vector3(0.3f, 0.3f, 1));
        }
        else if (secao == enumMission.FINAL_CAT)
        {
            Corvo.instance.Stop();
            Corvo.instance.transform.Find("BirdEmitterCollider").gameObject.SetActive(false);

            MissionManager.instance.rpgTalk.NewTalk("M8LivingroomSceneRepeat", "M8LivingroomSceneRepeatEnd", false);
        }
        else if (secao == enumMission.BOTIJAO_BIRD)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8KitchenSceneRepeat", "M8KitchenSceneRepeatEnd", false);
        }
        else if (secao == enumMission.FINAL_BIRD)
        {
            fireEvent.SetActive(true);
            Corvo.instance.Stop();
            Corvo.instance.transform.Find("BirdEmitterCollider").gameObject.SetActive(false);
            MissionManager.instance.blocked = true;
            MissionManager.instance.Invoke("InvokeMission", 5f);
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.ChangeMission(9);
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("EnterMomTrigger") && !falaMae)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8MomRoomSceneStart", "M8MomRoomSceneEnd", false);
            falaMae = true;
        }
        else if (tag.Equals("EnterGatoTrigger") && !falaGato)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8LivingroomSceneRepeat2", "M8LivingroomSceneRepeat2End", false);
            falaGato = true;
        }
        else if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT)
        {
            if (tag.Equals("EnterEstanteTrigger") && !estanteBurn)
            {
                fosforo.GetComponent<MiniGameObject>().activated = true;
                isqueiro.GetComponent<MiniGameObject>().activated = true;
                fosforo.GetComponent<MiniGameObject>().posFlareX = -5.71f;
                fosforo.GetComponent<MiniGameObject>().posFlareY = 1.64f;
                isqueiro.GetComponent<MiniGameObject>().posFlareX = -5.71f;
                isqueiro.GetComponent<MiniGameObject>().posFlareY = 1.64f;
                estanteTrigger = true;
            }
            else if (tag.Equals("EnterPoltronaTrigger") && !poltronaBurn)
            {
                fosforo.GetComponent<MiniGameObject>().activated = true;
                isqueiro.GetComponent<MiniGameObject>().activated = true;
                fosforo.GetComponent<MiniGameObject>().posFlareX = -1f;
                fosforo.GetComponent<MiniGameObject>().posFlareY = 1.6f;
                isqueiro.GetComponent<MiniGameObject>().posFlareX = -1f;
                isqueiro.GetComponent<MiniGameObject>().posFlareY = 1.6f;
                poltronaTrigger = true;
            }
            else if (tag.Equals("EnterSofaTrigger") && !sofaBurn)
            {
                fosforo.GetComponent<MiniGameObject>().activated = true;
                isqueiro.GetComponent<MiniGameObject>().activated = true;
                fosforo.GetComponent<MiniGameObject>().posFlareX = 5.53f;
                fosforo.GetComponent<MiniGameObject>().posFlareY = 0.4f;
                isqueiro.GetComponent<MiniGameObject>().posFlareX = 5.53f;
                isqueiro.GetComponent<MiniGameObject>().posFlareY = 0.5f;
                sofaTrigger = true;
            }
            else if (tag.Equals("ExitEstanteTrigger") || tag.Equals("ExitPoltronaTrigger") || tag.Equals("ExitSofaTrigger"))
            {
                fosforo.GetComponent<MiniGameObject>().activated = false;
                isqueiro.GetComponent<MiniGameObject>().activated = false;
                estanteTrigger = poltronaTrigger = sofaTrigger = false;
            }
        }
        else if (secao == enumMission.CORVO_ATACA_BIRD)
        {
            if (tag.Equals("EnterGasTrigger"))
            {
                if (!Inventory.HasItemType(Inventory.InventoryItems.FACA) && !Inventory.HasItemType(Inventory.InventoryItems.PEDRA))
                {
                    MissionManager.instance.rpgTalk.NewTalk("M8PedraCozinha", "M8PedraCozinhaEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
                }
                faca.GetComponent<MiniGameObject>().posFlareX = -4.1f;
                faca.GetComponent<MiniGameObject>().posFlareY = 1f;
                pedra.GetComponent<MiniGameObject>().posFlareX = -4.1f;
                pedra.GetComponent<MiniGameObject>().posFlareY = 1f;
                faca.GetComponent<MiniGameObject>().activated = true;
                pedra.GetComponent<MiniGameObject>().activated = true;
            }
            else if (tag.Equals("ExitGasTrigger"))
            {
                faca.GetComponent<MiniGameObject>().activated = false;
                pedra.GetComponent<MiniGameObject>().activated = false;
            }
        }
    }

    public override void InvokeMission()
    {
        if (gameOverSet)
        {
            gameOverSet = false;
            if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT || secao == enumMission.FINAL_CAT)
            {
                GameObject corvo = CreateCorvoCat();
                corvo.GetComponent<Corvo>().ChangePosition(
                    player.GetComponent<Player>().GetCorvoPositionX(), player.GetComponent<Player>().GetCorvoPositionY());
                corvo.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
                corvo.GetComponent<Corvo>().FollowPlayer();
                if (secao == enumMission.FINAL_CAT) EspecificaEnum((int)enumMission.FINAL_CAT);
            }
            else if (secao == enumMission.CORVO_ATACA_BIRD || secao == enumMission.BOTIJAO_BIRD)
            {
                GameObject corvo = CreateCorvoBird();
                corvo.GetComponent<Corvo>().ChangePosition(
                    player.GetComponent<Player>().GetCorvoPositionX(), player.GetComponent<Player>().GetCorvoPositionY());
                corvo.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
                corvo.GetComponent<Corvo>().FollowPlayer();
            }
        }
        else if  (secao == enumMission.CORVO_ATACA_CAT_INIT)
        {
            EspecificaEnum((int)enumMission.CORVO_ATACA_CAT);
        }
        else if (secao == enumMission.CORVO_ATACA_BIRD_INIT)
        {
            EspecificaEnum((int)enumMission.CORVO_ATACA_BIRD);
        }
        else if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.MAE_CAT)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8LivingroomSceneStart", "M8LivingroomSceneEnd");
        }
        else if (secao == enumMission.FINAL_BIRD)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }
    }

    //!!!!
    //Quando mais no caminho do gato, mais fraco o corvo
    public GameObject CreateCorvoCat()
    {
        GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(-1.7f, 0.6f, -0.5f), new Vector3(3.5f, 4.8f, 1));
        corvo.GetComponent<Corvo>().LookAtPlayer();
        corvo.GetComponent<Corvo>().speed = 0.08f; //-(MissionManager.instance.pathCat/1000); // velocidade do corvo
        corvo.GetComponent<Corvo>().timeBirdsFollow = 0.6f; //-(MissionManager.instance.pathCat/100); // tempo que os pássaros analisam onde o player está, quando menor, o delay será maior
        var em = corvo.transform.Find("BirdEmitterCollider").gameObject.GetComponent<ParticleSystem>();
        var main = em.main;
        em.emission.SetBurst(0, new ParticleSystem.Burst(0, 8, 12, 0, 10)); // min, max pássaros por burst e tempo para outro ciclo
        main.startSpeed = 1f; // velocidade dos pássaros
        main.duration = 10f; // tempo do ciclo de ataque dos pássaros        
        main.startLifetime = 18f; // tempo de vida dos pássaros, tem que ser menor que o ciclo
        main.maxParticles = 20;

        return corvo;
    }

    //!!!!
    //Quando mais no caminho do corvo, mais forte ele será
    public GameObject CreateCorvoBird()
    {
        GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(-1.7f, 0.6f, -0.5f), new Vector3(4f, 5.2f, 1));
        corvo.GetComponent<Corvo>().LookAtPlayer();
        corvo.GetComponent<Corvo>().speed = 0.08f; //+(MissionManager.instance.pathBird/1000);
        corvo.GetComponent<Corvo>().timeBirdsFollow = 0.6f; //+(MissionManager.instance.pathBird/100);
        var em = corvo.transform.Find("BirdEmitterCollider").gameObject.GetComponent<ParticleSystem>();
        var main = em.main;
        em.emission.SetBurst(0, new ParticleSystem.Burst(0, 8, 12, 0, 8));
        main.startSpeed = 1f;
        main.duration = 8f;
        main.startLifetime = 14f;
        main.maxParticles = 20;

        return corvo;
    }

 }
