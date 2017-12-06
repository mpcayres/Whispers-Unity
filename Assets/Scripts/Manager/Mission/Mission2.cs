using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2 : Mission {
    enum enumMission { NIGHT, INICIO_GATO, INICIO_SOZINHO, ENCONTRA_MAE,
        CONTESTA_MAE, CONTESTA_MAE2, RESPEITA_MAE, RESPEITA_MAE2, FINAL_CONTESTA, FINAL_RESPEITA };
    enumMission secao;

    public override void InitMission()
    {
        sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float)1.5;
        MissionManager.initY = (float)-1.0;
        MissionManager.initDir = 1;
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
                if (MissionManager.instance.mission1AssustaGato)
                {
                    EspecificaEnum((int)enumMission.INICIO_SOZINHO);
                }
                else
                {
                    EspecificaEnum((int)enumMission.INICIO_GATO);
                }
                MissionManager.instance.rpgTalk.NewTalk("M2KidRoomSceneStart", "M2KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
            }
        }
    }

    public override void SetCorredor()
    {
        if (secao == enumMission.INICIO_SOZINHO || secao == enumMission.INICIO_GATO)
        {
            // Porta Cozinha
            GameObject portaCozinha = GameObject.Find("DoorToKitchen").gameObject;
            portaCozinha.tag = "Untagged";
            portaCozinha.GetComponent<Collider2D>().isTrigger = false;

            // Porta Sala
            GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
            portaSala.tag = "Untagged";
            portaSala.GetComponent<Collider2D>().isTrigger = false;

            // Porta Quarto Mae
            GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
            portaMae.tag = "Untagged";
            portaMae.GetComponent<Collider2D>().isTrigger = false;

            // Mae
            MissionManager.instance.AddObject("mom", "", new Vector3(1.8f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(1.8f, 0f, 1), new Vector3(1, 1, 1));
            trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
            trigger.GetComponent<BoxCollider2D>().size = new Vector2(2f, 2f);
        }
        else if (secao == enumMission.CONTESTA_MAE2 || secao == enumMission.RESPEITA_MAE2)
        {
            // Mae patrulha
            GameObject mom = MissionManager.instance.AddObject("mom", "", new Vector3(1.8f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            mom.GetComponent<Mom>().isPatroller = true;
            Transform target1 = new GameObject().transform, target2 = new GameObject().transform;
            target1.position = new Vector3(-2.6f, 0f, -0.5f);
            target2.position = new Vector3(6.8f, 0f, -0.5f);
            Transform[] momTargets = { target1, target2 };
            mom.GetComponent<Mom>().targets = momTargets;
            MissionManager.instance.AddObject("MomAction", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        }

        if (secao == enumMission.INICIO_SOZINHO)
        {
            // Gato
            MissionManager.instance.AddObject("catFollower", "", new Vector3(10.8f, -0.3f, 0), new Vector3(0.15f, 0.15f, 1));
        }
    }

	public override void SetCozinha()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M2KitchenSceneStart", "M2KitchenSceneEnd");

        // Panela
        GameObject panela = GameObject.Find("Panela").gameObject;
        panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");

        GameObject armario = GameObject.Find("Armario1").gameObject;
        SceneObject sceneObject = armario.GetComponent<SceneObject>();

        if (secao == enumMission.CONTESTA_MAE2) {
            ScenePickUpObject panelaPickUp = panela.AddComponent<ScenePickUpObject>();
            panelaPickUp.sprite1 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panelaPickUp.sprite2 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela");
            panelaPickUp.item = Inventory.InventoryItems.TAMPA;
            panelaPickUp.blockAfterPick = true;
            
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
        if((secao == enumMission.NIGHT && !MissionManager.instance.mission1AssustaGato) || secao == enumMission.INICIO_GATO)
        {
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(2.5f,-1.3f,0), new Vector3(0.15f,0.15f,1));
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
        }
    }

    public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M2MomRoomSceneStart", "M2MomRoomSceneEnd");
	}

    public override void SetSala()
    {
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
            MissionManager.instance.rpgTalk.NewTalk ("M2CorridorSceneStart", "M2CorridorSceneEnd");
        }
    }

    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("AreaTrigger(Clone)") && (secao == enumMission.INICIO_GATO || secao == enumMission.INICIO_SOZINHO))
        {
            EspecificaEnum((int)enumMission.ENCONTRA_MAE);
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
        else if (secao == enumMission.RESPEITA_MAE2)
        {
            GameObject.Destroy(GameObject.Find("mom(Clone)").gameObject);
            MissionManager.instance.rpgTalk.NewTalk("M2Q1C1_2", "M2Q1C1_2End", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");
        }
        else if (secao == enumMission.CONTESTA_MAE2)
        {
            GameObject.Destroy(GameObject.Find("mom(Clone)").gameObject);
            MissionManager.instance.rpgTalk.NewTalk("M2Q1C0_2", "M2Q1C0_2End", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");
        }
    }

    public void AddCountLivingroomDialog()
    {
		MissionManager.instance.countLivingroomDialog++;
	}

    public void AddCountKidRoomDialog()
    {
        MissionManager.instance.countKidRoomDialog++;
    }
}