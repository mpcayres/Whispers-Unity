using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Mission1 : Mission {
    enum enumMission { NIGHT, INICIO, GATO_APARECEU, GATO_CORREDOR,
        GATO_COZINHA, GATO_SALA, LANTERNA_ENCONTRADA, CORVO_VISTO, SMILE, MAE_QUARTO, FAZER_ESCOLHA, FINAL };
    enumMission secao;

    bool left = false;
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
        MissionManager.LoadScene(sceneInit);
        secao = enumMission.NIGHT;
        Book.bookBlocked = true;
        MissionManager.instance.invertWorld = false;
        MissionManager.instance.invertWorldBlocked = true;

        PlayerPrefs.DeleteKey("MO_Corredor_0X");
        PlayerPrefs.DeleteKey("MO_Corredor_0Y");

        SetInitialSettings();
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
        else if (secao == enumMission.INICIO)
        {
            if (GameObject.Find("CrowHolder").gameObject.transform.GetComponent<SickCrow>().fly)
            {
                EspecificaEnum((int) enumMission.GATO_APARECEU);
            }
        }
        else if (secao == enumMission.GATO_APARECEU)
        {
            var cat = GameObject.Find("catFollower(Clone)").gameObject;
            if (MissionManager.instance.currentSceneName.Equals("Corredor") && !MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.GATO_CORREDOR);
            } else if (MissionManager.instance.currentSceneName.Equals("QuartoKid") && !MissionManager.instance.rpgTalk.isPlaying) {
                if (!left)
                {
                    
                    Vector3 aux = new Vector3(1.8f, 1f, -0.5f);
                    Vector3[] catPos = { aux };
                    cat.GetComponent<Cat>().targets = catPos;
                    Cat.instance.destroyEndPath = true;
                    left = true;
                }
               // else if(cat.GetComponent<Cat>().targets == null) {
               //     GameObject.Destroy(GameObject.Find("catFollower(Clone)").gameObject);
               // }
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
                MissionManager.instance.scenerySounds.PlayBird(1);
                GameObject birds = GameObject.Find("BirdEmitterHolder(Sala)").gameObject;
                birds.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
                birdsActive = true;

            }
            if(birdsActive && !MissionManager.instance.scenerySounds.source.isPlaying && (!MissionManager.instance.currentSceneName.Equals("GameOver") || !MissionManager.instance.currentSceneName.Equals("MainMenu")))
            {
                MissionManager.instance.scenerySounds.StopSound();
                float value = Random.value;
                if(value > 0)
                    MissionManager.instance.scenerySounds.PlayBird(1);
                else
                    MissionManager.instance.scenerySounds.PlayBird(4);

            }
            
        }
    }

    public override void SetCorredor()
    {

        MissionManager.instance.scenerySounds.StopSound();
        if (secao == enumMission.GATO_APARECEU)
        {
            MissionManager.instance.rpgTalk.NewTalk("M1CorridorSceneStart", "M1CorridorSceneEnd", MissionManager.instance.rpgTalk.txtToParse);
            MissionManager.instance.scenerySounds.PlayCat(2);
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Porta Mae
        GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
        float portaMaeDefaultY = portaMae.transform.position.y;
        float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
        portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
        portaMae.GetComponent<SceneDoor>().isOpened = false;
        portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

        // Objeto movel que atrapalha
        GameObject chair = MissionManager.instance.AddObject("Objects/MovingObject", "Sprites/Objects/Scene/vaso",
            new Vector3((float)-3.59, (float)-0.45, 0), new Vector3((float)1.2, (float)1.2, 1));
        chair.GetComponent<MovingObject>().prefName = "MO_Corredor_0";

        if (secao == enumMission.GATO_APARECEU)
        {
            MissionManager.instance.AddObject("NPCs/catFollower", "", new Vector3(8.3f, -0.6f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
            GameObject.Find("MainCamera").GetComponent<Camera>().transform.position = new Vector3(0f, 0f, -20f);
            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.GetComponent<SceneDoor>().isOpened = false;

            // Porta QuartoKid
            GameObject portaKid = GameObject.Find("DoorToKidRoom").gameObject;
            portaKid.GetComponent<SceneDoor>().isOpened = false;
        }
        else if (secao == enumMission.GATO_COZINHA)
        {
            GameObject cat = MissionManager.instance.AddObject("NPCs/catFollower", "", new Vector3(-0.7f, -0.6f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol();
            Vector3 aux = new Vector3(-9.8f, -0.4f, -0.5f);
            Vector3[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;

            MissionManager.instance.pausedObject = true;
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
            GameObject.Find("MainCamera").GetComponent<Camera>().transform.position = new Vector3(0f, 0f, -20f);
            MissionManager.instance.Invoke("InvokeMission", 2.5f);

            // gato andando para sala
            MissionManager.instance.scenerySounds.PlayCat(3);
        }
    }

    public override void SetCozinha()
    {

        MissionManager.instance.scenerySounds.StopSound();
        MissionManager.instance.scenerySounds.PlayDrop();
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

        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M1GardenSceneStart", "M1GardenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        /*GameObject areaLight = GameObject.Find("AreaLightHolder").gameObject; //utilizar AreaLight para cenas de dia, variar Z do Holder
        areaLight.transform.Find("AreaLight").gameObject.SetActive(true);
        areaLight.transform.position = new Vector3(areaLight.transform.position.x, areaLight.transform.position.y, -20);*/
        MissionManager.instance.scenerySounds.PlayWolf(2);
    }

    public override void SetQuartoKid()
    {

        MissionManager.instance.scenerySounds.StopSound();
        // Luz
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true);

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
            porta.GetComponent<SceneDoor>().isOpened = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);
        }

        if(secao == enumMission.MAE_QUARTO)
        {
            GameObject.Find("Flashlight").gameObject.GetComponent<Flashlight>().EnableFlashlight(false);
            MissionManager.instance.GetComponent<Player>().ChangePositionDefault(-2.5f, 0.7f, 0);
            MissionManager.instance.AddObject("NPCs/mom", "", new Vector3(1.7f, 0.6f, -0.5f), new Vector3(0.3f, 0.3f, 1));

            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<SceneDoor>().isOpened = false;

            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneRepeat", "M1KidRoomSceneRepeatEnd", MissionManager.instance.rpgTalk.txtToParse);
        }

    }

    public override void SetQuartoMae()
    {

        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M1MomRoomSceneStart", "M1MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetSala()
    {

        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M1LivingroomSceneStart", "M1LivingroomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLightBooks").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Lanterna - Criado Mudo
        MissionManager.instance.CreateScenePickUp("CriadoMudoSala", Inventory.InventoryItems.FLASHLIGHT);

        // Porta Jardim
        GameObject portaJardim = GameObject.Find("DoorToGarden").gameObject;
        portaJardim.GetComponent<SceneDoor>().isOpened = false;

        // Porta Corredor
        GameObject portaCorredor = GameObject.Find("DoorToAlley").gameObject;
        portaCorredor.GetComponent<SceneDoor>().isOpened = false;

        GameObject trigger = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(0f, 0f, 0), new Vector3(1, 1, 1));
        trigger.name = "AreaTrigger";
        trigger.GetComponent<Collider2D>().offset = new Vector2(2.68f, 0);
        trigger.GetComponent<BoxCollider2D>().size = new Vector2(10.2f, 5f);

        GameObject trigger2 = MissionManager.instance.AddObject("Scenery/AreaTrigger", "", new Vector3(0f, 0f, 0), new Vector3(1, 1, 1));
        trigger2.name = "TVTrigger";
        trigger2.GetComponent<Collider2D>().offset = new Vector2(7f, -0.4f);
        trigger2.GetComponent<BoxCollider2D>().size = new Vector2(1.55f, 0.8f);
        
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

        if(secao == enumMission.INICIO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneStart", "M1KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.GATO_APARECEU)
        {
            // Porta abrindo
            MissionManager.instance.scenerySounds2.PlayDoorOpen(2);
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-opened");
            porta.GetComponent<SceneDoor>().isOpened = true;
            porta.transform.position = new Vector3(portaDefaultX, portaDefaultY, porta.transform.position.z);
            
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneCat", "M1KidRoomSceneCatEnd", MissionManager.instance.rpgTalk.txtToParse);
            
            // Gato entrando
            MissionManager.instance.scenerySounds.PlayCat(2);
            GameObject cat = MissionManager.instance.AddObject("NPCs/catFollower", "", new Vector3(1.8f, 1f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol();
            Vector3 aux = new Vector3(1f, 0f, -0.5f);
            Vector3[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;
            Cat.instance.stopEndPath = true;

           
        }
        else if (secao == enumMission.GATO_CORREDOR)
        {
            Cat.instance.GetComponent<Cat>().Patrol();
            Vector3 aux = new Vector3(2.6f, -0.7f, -0.5f);
            Vector3[] catPos = { aux };
            Cat.instance.GetComponent<Cat>().targets = catPos;
            Cat.instance.stopEndPath = true;
            Cat.instance.speed = 1.4f;
            MissionManager.instance.Invoke("InvokeMission", 5.5f);
            MissionManager.instance.pausedObject = true;
        }
        else if (secao == enumMission.CORVO_VISTO)
        {
            MissionManager.instance.scenerySounds.PlayBird(1);
            MissionManager.instance.blocked = true;
            //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLightTV").gameObject.SetActive(true);
            GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
            mainLight.transform.Rotate(new Vector3(-25, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
            MissionManager.instance.AddObject("Effects/BlinkMainLight", "", new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
            GameObject.Find("TV").gameObject.GetComponent<SceneMultipleObject>().ChangeSprite();
            MissionManager.instance.Invoke("InvokeMission", 5f);
        }
        else if (secao == enumMission.SMILE)
        {
            MissionManager.instance.scenerySounds.PlayScare(3);
            
            GameObject darkness = GameObject.Find("DarknessHolder").gameObject;
            darkness.transform.Find("Darkness1").gameObject.SetActive(true);
            darkness.transform.Find("Darkness2").gameObject.SetActive(true);
            MissionManager.instance.Invoke("InvokeMission", 3f);
        }
        else if (secao == enumMission.MAE_QUARTO)
        {
            MissionManager.LoadScene(sceneInit);
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
        else if (tag.Equals("TVTrigger") && secao == enumMission.LANTERNA_ENCONTRADA && !birdsActive)
        {
            EspecificaEnum((int)enumMission.CORVO_VISTO);
        }
    }

    public override void InvokeMission()
    {
        if (secao == enumMission.GATO_CORREDOR || secao == enumMission.GATO_COZINHA)
        {
            GameObject.Destroy(GameObject.Find("catFollower(Clone)").gameObject);
            MissionManager.instance.pausedObject = false;
            GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 2;
        }
        else if (secao == enumMission.CORVO_VISTO)
        {
            EspecificaEnum((int)enumMission.SMILE);
        }
        else if (secao == enumMission.SMILE)
        {
            EspecificaEnum((int)enumMission.MAE_QUARTO);
        }
        else if (secao == enumMission.FAZER_ESCOLHA)
        {
            MissionManager.instance.AddObject("NPCs/catFollower", "", new Vector3(1.7f, 0.7f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            MissionManager.instance.rpgTalk.NewTalk("M1KidRoomSceneChoice", "M1KidRoomSceneChoiceEnd", MissionManager.instance.rpgTalk.txtToParse);
        }
        else if (secao == enumMission.FINAL)
        {
            PlayerPrefs.DeleteKey("Corredor_0X");
            PlayerPrefs.DeleteKey("Corredor_0Y");
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
                MissionManager.instance.mission1AssustaGato = true;
                GameObject.Destroy(GameObject.Find("catFollower(Clone)").gameObject);
            }
            else
            {
                MissionManager.instance.mission1AssustaGato = false;
                GameObject.Find("catFollower(Clone)").gameObject.GetComponent<Cat>().FollowPlayer();
            }
            MissionManager.instance.Invoke("InvokeMission", 8f);
        }
    }

}
