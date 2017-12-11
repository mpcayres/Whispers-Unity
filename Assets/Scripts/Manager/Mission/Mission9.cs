using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission9 : Mission {
    enum enumMission { NIGHT, INICIO, SALA, CORREDOR, QUARTO_MAE, COZINHA, QUARTO_KID, FINAL };
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
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();
        if (MissionManager.instance.pathCat >= MissionManager.instance.pathBird) endCat = true;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        GameObject.Find("HUDCanvas").transform.Find("SelectedObject").gameObject.SetActive(false);
        GameObject.Find("HUDCanvas").transform.Find("BoxInventory").gameObject.SetActive(false);

        Book.bookBlocked = false;
        MissionManager.instance.invertWorld = false;

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
            }
        }
    }

    public override void SetCorredor()
    {
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
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

            if (MissionManager.instance.mission8BurnCorredor)
            {
                player.GetComponent<Player>().ChangePositionDefault(0, 0, 0);
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
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 2;
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

        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);

            // Porta bloqueada
            GameObject porta = GameObject.Find("DoorToLivingRoom").gameObject;
            porta.GetComponent<Collider2D>().isTrigger = false;

            // Player
            player.GetComponent<Renderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.layer = LayerMask.NameToLayer("Player");

            // Gato
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(0.92f, 1.46f, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().followWhenClose = false;
            cat.GetComponent<Cat>().Stop();

            // Mãe
            MissionManager.instance.AddObject("mom", "", new Vector3(0.46f, 1.98f, -0.5f), new Vector3(0.3f, 0.3f, 1));
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

            if (!MissionManager.instance.mission8BurnCorredor)
            {
                player.GetComponent<Player>().ChangePositionDefault(0, -1f, 0);
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
        GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 3;
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
            SceneManager.LoadScene("Sala", LoadSceneMode.Single);
        }
        else if (secao == enumMission.CORREDOR)
        {
            SceneManager.LoadScene("Corredor", LoadSceneMode.Single);
        }
        else if (secao == enumMission.QUARTO_MAE)
        {
            SceneManager.LoadScene("QuartoMae", LoadSceneMode.Single);
        }
        else if (secao == enumMission.COZINHA)
        {
            SceneManager.LoadScene("Cozinha", LoadSceneMode.Single);
        }
        else if (secao == enumMission.QUARTO_KID)
        {
            SceneManager.LoadScene("QuartoKid", LoadSceneMode.Single);
        }
        else if (secao == enumMission.FINAL)
        {
            // FIM DO JOGO
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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
            EspecificaEnum((int)enumMission.FINAL);
        }
    }

}
