using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mission8 : Mission {
    enum enumMission { NIGHT, INICIO, CORVO_APARECE_CAT, CORVO_ATACA_CAT, CORVO_APARECE_BIRD, CORVO_ATACA_BIRD, FINAL };
    enumMission secao;

    bool hasPanela = false, endCat = false;
    Book book;
    GameObject player;

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

        hasPanela = Inventory.HasItemType(Inventory.InventoryItems.TAMPA);
        if (MissionManager.instance.pathCat >= MissionManager.instance.pathBird) endCat = true;
        book = GameObject.Find("Player").gameObject.GetComponent<Book>();
        player = GameObject.Find("Player").gameObject;
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {
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
                EspecificaEnum((int)enumMission.CORVO_ATACA_CAT);
            }
        }
        else if (secao == enumMission.CORVO_APARECE_BIRD)
        {
            if (!MissionManager.instance.rpgTalk.isPlaying)
            {
                EspecificaEnum((int)enumMission.CORVO_ATACA_BIRD);
            }
        }
    }

    public override void SetCorredor()
    {
        MissionManager.instance.scenerySounds.StopSound();

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }
    }

    public override void SetCozinha()
    {
        MissionManager.instance.scenerySounds.PlayDrop();

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }

        // Panela para caso ainda não tenha
        if (!hasPanela)
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panela.GetComponent<ScenePickUpObject>().enabled = true;
        }
    }

    public override void SetJardim()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }
    }

    public override void SetQuartoKid()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
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
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public override void SetQuartoMae()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }
    }

    public override void SetSala()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(30, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            MissionManager.instance.Invoke("InvokeMission", 2f);
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if(secao == enumMission.INICIO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M8KidRoomSceneStart", "M8KidRoomSceneEnd");
        }
        else if (secao == enumMission.CORVO_APARECE_CAT)
        {
            MissionManager.instance.rpgTalk.NewTalk("Dica8PC", "Dica8PCEnd");

            CreateCorvoCat();

            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (secao == enumMission.CORVO_APARECE_BIRD)
        {
            MissionManager.instance.rpgTalk.NewTalk("Dica8PB", "Dica8PBEnd");

            CreateCorvoBird();

            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (secao == enumMission.CORVO_ATACA_CAT || secao == enumMission.CORVO_ATACA_BIRD)
        {
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = true;

            Corvo.instance.FollowPlayer();
        }
    }

    public override void AreaTriggered(string tag)
    {

    }

    public override void InvokeMission()
    {
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            if (secao == enumMission.CORVO_APARECE_CAT)
            {
                CreateCorvoCat();
                player.GetComponent<Player>().ChangeCorvoPosition();
            }
            else if (secao == enumMission.CORVO_APARECE_BIRD)
            {
                CreateCorvoBird();
                player.GetComponent<Player>().ChangeCorvoPosition();
            }
        }
    }

    private void CreateCorvoCat()
    {
        GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(-1.7f, 0.6f, -0.5f), new Vector3(0.4f, 0.4f, 1));
        corvo.GetComponent<Corvo>().speed = 0.3f;
        corvo.GetComponent<Corvo>().timeBirdsFollow = 1f;
        var main = corvo.transform.Find("BirdEmitterCollider").gameObject.GetComponent<ParticleSystem>().main;
        main.startSpeed = 2;
        corvo.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
    }

    private void CreateCorvoBird()
    {
        GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(-1.7f, 0.6f, -0.5f), new Vector3(0.4f, 0.4f, 1));
        corvo.GetComponent<Corvo>().speed = 0.3f;
        corvo.GetComponent<Corvo>().timeBirdsFollow = 1.5f;
        var main = corvo.transform.Find("BirdEmitterCollider").gameObject.GetComponent<ParticleSystem>().main;
        main.startSpeed = 3;
        corvo.transform.Find("BirdEmitterCollider").gameObject.SetActive(true);
    }
 }
