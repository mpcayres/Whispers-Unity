using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission9 : Mission {
    enum enumMission { NIGHT, INICIO, SALA, CORREDOR, QUARTO_MAE, COZINHA, QUARTO_KID, QUARTO_KID_CORVO, QUARTO_KID_CORVO_ATACA, FINAL };
    enumMission secao;

    bool endCat = false;
    GameObject player;

    public override void InitMission()
    {
        sceneInit = "Jardim";
        MissionManager.initMission = true;
        MissionManager.initX = (float)1.54;
        MissionManager.initY = (float)1.75;
        MissionManager.initDir = 2;
        MissionManager.LoadScene(sceneInit);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();
        if (MissionManager.instance.pathCat >= MissionManager.instance.pathBird) endCat = true;

        Book.bookBlocked = true;

        MissionManager.instance.invertWorld = false;
        MissionManager.instance.invertWorldBlocked = false;
        MissionManager.instance.paused = false;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject.SetActive(false);
        GameObject.Find("HUDCanvas").transform.Find("BoxInventory").gameObject.SetActive(false);
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
        else if (endCat && MissionManager.instance.currentSceneName.Equals("Jardim") && Cat.instance == null)
        {
            // Gato, correção de um erro
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(0.92f, 1.46f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().followWhenClose = false;
            cat.GetComponent<Cat>().Stop();
            cat.GetComponent<Cat>().ChangeDirectionAnimation(5);
        }
        if (secao == enumMission.QUARTO_KID_CORVO_ATACA && !MissionManager.instance.scenerySounds.source.isPlaying)
        {
            MissionManager.instance.scenerySounds.PlayBird(1);
        }
        if (secao == enumMission.QUARTO_KID_CORVO && !MissionManager.instance.scenerySounds.source.isPlaying)
        {
            MissionManager.instance.scenerySounds.PlayDemon(1);
        }
    }

    public override void SetCorredor()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 5;
        player.GetComponent<Player>().ChangePositionDefault(0, 0, 0);
        GameObject.Find("MainCamera").transform.position = new Vector3(0, 0, -20);

        if (Cat.instance != null) Cat.instance.DestroyCat();

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);

            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            player.layer = LayerMask.NameToLayer("PlayerHidden");
            MissionManager.instance.blocked = true;
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);

            // Porta bloqueada
            GameObject porta = GameObject.Find("DoorToLivingRoom").gameObject;
            porta.GetComponent<SceneDoor>().isOpened = false;

            // Porta bloqueada
            GameObject portaF = GameObject.Find("DoorToKidRoom").gameObject;
            float portaFDefaultY = portaF.transform.position.y;
            float posXF = portaF.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaF.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaF.GetComponent<SceneDoor>().isOpened = false;
            portaF.transform.position = new Vector3(portaF.transform.position.x - posXF, portaFDefaultY, portaF.transform.position.z);

            // Porta bloqueada
            GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
            float portaMaeDefaultY = portaMae.transform.position.y;
            float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaMae.GetComponent<SceneDoor>().isOpened = false;
            portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

            // Porta bloqueada
            GameObject portaK = GameObject.Find("DoorToKitchen").gameObject;
            portaK.GetComponent<SceneDoor>().isOpened = false;

            if (MissionManager.instance.mission8BurnCorredor)
            {
                player.GetComponent<Player>().ChangePositionDefault(-6f, -0.4f, 0);
                player.GetComponent<Renderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.layer = LayerMask.NameToLayer("Player");
                MissionManager.instance.blocked = false;
            }
            else
            {
                player.GetComponent<Renderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                player.layer = LayerMask.NameToLayer("PlayerHidden");
                MissionManager.instance.blocked = true;
            }
        }

        if (secao == enumMission.CORREDOR)
        {
            MissionManager.instance.Invoke("InvokeMission", 10f);
        }

    }

    public override void SetCozinha()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 3;
        player.GetComponent<Player>().ChangePositionDefault(0, 0, 0);
        GameObject.Find("MainCamera").transform.position = new Vector3(0, 0, -20);

        if (Cat.instance != null) Cat.instance.DestroyCat();

        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        player.layer = LayerMask.NameToLayer("PlayerHidden");
        MissionManager.instance.blocked = true;

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }

        if (secao == enumMission.COZINHA)
        {
            MissionManager.instance.Invoke("InvokeMission", 6f);
        }
    }

    public override void SetJardim()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 3;

        if (Cat.instance != null) Cat.instance.DestroyCat();

        GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventTree").gameObject.SetActive(true);

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);

            // Porta bloqueada
            GameObject porta = GameObject.Find("DoorToLivingRoom").gameObject;
            porta.GetComponent<SceneDoor>().isOpened = false;

            // Player
            player.GetComponent<Renderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.layer = LayerMask.NameToLayer("Player");
            MissionManager.instance.blocked = false;

            // Gato
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(0.92f, 1.46f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().followWhenClose = false;
            cat.GetComponent<Cat>().Stop();
            cat.GetComponent<Cat>().ChangeDirectionAnimation(5);

            // Mãe
            GameObject mae = MissionManager.instance.AddObject("mom", "", new Vector3(0.46f, 1.98f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            mae.GetComponent<Patroller>().Stop();
            mae.GetComponent<Patroller>().ChangeDirectionAnimation(5);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);

            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            player.layer = LayerMask.NameToLayer("PlayerHidden");
            MissionManager.instance.blocked = true;
        }

        if (secao == enumMission.NIGHT || secao == enumMission.INICIO)
        {
            MissionManager.instance.Invoke("InvokeMission", 15f);
        }
    }

    public override void SetQuartoKid()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 3;
        player.GetComponent<Player>().ChangePositionDefault(0, 0, 0);
        GameObject.Find("MainCamera").transform.position = new Vector3(0, 0, -20);

        if (Cat.instance != null) Cat.instance.DestroyCat();

        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        player.layer = LayerMask.NameToLayer("PlayerHidden");
        MissionManager.instance.blocked = true;

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);

            // Porta bloqueada
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            float portaDefaultY = porta.transform.position.y;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.GetComponent<SceneDoor>().isOpened = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);

            // Gato
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(2.5f, -1.3f, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().followWhenClose = false;
            cat.GetComponent<Cat>().Stop();
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

        if (secao == enumMission.QUARTO_KID)
        {
            MissionManager.instance.Invoke("InvokeMission", 10f);
        }
    }

    public override void SetQuartoMae()
    {
        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 3;
        player.GetComponent<Player>().ChangePositionDefault(0, 0, 0);
        GameObject.Find("MainCamera").transform.position = new Vector3(0, -1, -20);

        if (Cat.instance != null) Cat.instance.DestroyCat();

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            player.layer = LayerMask.NameToLayer("PlayerHidden");
            MissionManager.instance.blocked = true;
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);

            // Mãe
            MissionManager.instance.AddObject("mom", "", new Vector3(2.04f, 0.94f, -0.5f), new Vector3(0.3f, 0.3f, 1));

            // Porta bloqueada
            GameObject portaMae = GameObject.Find("DoorToAlley").gameObject;
            float portaMaeDefaultY = portaMae.transform.position.y;
            float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            portaMae.GetComponent<SceneDoor>().isOpened = false;
            portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);

            if (!MissionManager.instance.mission8BurnCorredor)
            {
                player.GetComponent<Player>().ChangePositionDefault(2.6f, 1.1f, 0);
                player.GetComponent<Renderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.layer = LayerMask.NameToLayer("Player");
                MissionManager.instance.blocked = false;
            }
            else {
                player.GetComponent<Renderer>().enabled = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                player.layer = LayerMask.NameToLayer("PlayerHidden");
                MissionManager.instance.blocked = true;
            }
        }

        if (secao == enumMission.QUARTO_MAE)
        {
            MissionManager.instance.Invoke("InvokeMission", 10f);
        }
    }

    public override void SetSala()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
        player.GetComponent<Player>().ChangePositionDefault(0, -1, 0);
        GameObject.Find("MainCamera").transform.position = new Vector3(0, 0, -20);

        if (Cat.instance != null) Cat.instance.DestroyCat();

        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        player.layer = LayerMask.NameToLayer("PlayerHidden");
        MissionManager.instance.blocked = true;

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }

        if (secao == enumMission.SALA)
        {
            MissionManager.instance.Invoke("InvokeMission", 10f);
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission) pos;
        MissionManager.instance.Print("SECAO: " + secao);
        if (secao == enumMission.SALA)
        {
            MissionManager.LoadScene("Sala");
        }
        else if (secao == enumMission.CORREDOR)
        {
            MissionManager.LoadScene("Corredor");
        }
        else if (secao == enumMission.QUARTO_MAE)
        {
            MissionManager.LoadScene("QuartoMae");
        }
        else if (secao == enumMission.COZINHA)
        {
            MissionManager.LoadScene("Cozinha");
        }
        else if (secao == enumMission.QUARTO_KID)
        {
            MissionManager.LoadScene("QuartoKid");
        }
        else if (secao == enumMission.QUARTO_KID_CORVO)
        {
            MissionManager.instance.InvertWorld(true);
            if (endCat)
            {
                GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(0f, 0f, -0.5f), new Vector3(4.5f, 4.5f, 1));
                corvo.GetComponent<SpriteRenderer>().color = Color.gray;
            }
            else
            {
                GameObject corvo = MissionManager.instance.AddObject("Corvo", "", new Vector3(0f, 0f, -0.5f), new Vector3(5f, 5f, 1));
                corvo.GetComponent<SpriteRenderer>().color = Color.gray;
            }

            GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(false);

            MissionManager.instance.Invoke("InvokeMission", 8f);
        }
        else if (secao == enumMission.QUARTO_KID_CORVO_ATACA)
        {
            MissionManager.instance.scenerySounds.StopSound();
            GameObject emitter = Corvo.instance.transform.Find("BirdEmitterCollider").gameObject;
            Corvo.instance.timeBirdsFollow = 0f;
            emitter.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            var emAux = emitter.GetComponent<ParticleSystem>();
            var main = emAux.main;
            emitter.GetComponent<ParticleSystemRenderer>().sortingOrder = 2;
            emAux.emission.SetBurst(0, new ParticleSystem.Burst(0, 30, 30, 0, 10));
            main.maxParticles = 30;
            emitter.SetActive(true);

            //se quiser deixar os corvos mais rápidos
            float hSliderValue = 3.0F;
            main.simulationSpeed = hSliderValue;
            main.startSpeed = hSliderValue;

            //mudanças de tamanho
            main.startSize = 1.5F;
            MissionManager.instance.Invoke("InvokeMission", 5f);
        }
        else if (secao == enumMission.FINAL)
        {
            // FIM DO JOGO XD
            MissionManager.LoadScene("MainMenu");
        }
    }

    public override void InvokeMission()
    {
        if (secao == enumMission.INICIO)
        {
            EspecificaEnum((int)enumMission.SALA);
        }
        else if (secao == enumMission.SALA)
        {
            EspecificaEnum((int)enumMission.CORREDOR);
        }
        else if (secao == enumMission.CORREDOR)
        {
            EspecificaEnum((int)enumMission.QUARTO_MAE);
        }
        else if (secao == enumMission.QUARTO_MAE)
        {
            EspecificaEnum((int)enumMission.COZINHA);
        }
        else if (secao == enumMission.COZINHA)
        {
            EspecificaEnum((int)enumMission.QUARTO_KID);
        }
        else if (secao == enumMission.QUARTO_KID)
        {
            EspecificaEnum((int)enumMission.QUARTO_KID_CORVO);
        }
        else if (secao == enumMission.QUARTO_KID_CORVO)
        {
            EspecificaEnum((int)enumMission.QUARTO_KID_CORVO_ATACA);
        }
        else if (secao == enumMission.QUARTO_KID_CORVO_ATACA)
        {
            EspecificaEnum((int)enumMission.FINAL);
        }
    }

}
