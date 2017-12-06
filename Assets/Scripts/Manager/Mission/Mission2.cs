using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2 : Mission {
    enum enumMission { INICIO_GATO, INICIO_SOZINHO, CONTESTA_MAE, RESPEITA_MAE, FINAL_CONTESTA, FINAL_RESPEITA };
    enumMission secao;

    public override void InitMission()
    {
        sceneInit = "Sala";
        MissionManager.initMission = true;
        MissionManager.initX = (float)-3.5;
        MissionManager.initY = (float)0.15;
        MissionManager.initDir = 1;
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.INICIO_GATO;
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    { 
		
    }

    public override void SetCorredor()
    {
		//MissionManager.instance.rpgTalk.NewTalk ("M2CorridorSceneStart", "M2CorridorSceneEnd");
    }

	public override void SetCozinha()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M2KitchenSceneStart", "M2KitchenSceneEnd");

        GameObject panela = GameObject.Find("Panela").gameObject;
        panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");

        GameObject armario = GameObject.Find("Armario1").gameObject;
        SceneObject sceneObject = armario.GetComponent<SceneObject>();

        if (secao == enumMission.CONTESTA_MAE) {
            ScenePickUpObject panelaPickUp = panela.AddComponent<ScenePickUpObject>();
            panelaPickUp.sprite1 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panelaPickUp.sprite2 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela");
            panelaPickUp.item = Inventory.InventoryItems.TAMPA;
            panelaPickUp.blockAfterPick = true;
            
            sceneObject.enabled = false;
            ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
            scenePickUpObject.sprite1 = sceneObject.sprite1;
            scenePickUpObject.sprite2 = sceneObject.sprite2;
            scenePickUpObject.positionSprite = sceneObject.positionSprite;
            scenePickUpObject.scale = sceneObject.scale;
            scenePickUpObject.isUp = sceneObject.isUp;
            scenePickUpObject.item = Inventory.InventoryItems.FACA;
        }
        else if (secao == enumMission.RESPEITA_MAE)
        {
            sceneObject.enabled = false;
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
		if (MissionManager.instance.countKidRoomDialog == 0) {
            MissionManager.instance.rpgTalk.NewTalk ("M2KidRoomSceneStart", "M2KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        }

        GameObject windowTrigger = GameObject.Find("WindowTrigger").gameObject;
        windowTrigger.tag = "ScenePickUpObject";
        WindowTrigger sceneObject = windowTrigger.GetComponent<WindowTrigger>();
        sceneObject.enabled = true;
        SceneObject sceneObjectNew = windowTrigger.GetComponent<SceneObject>();
        sceneObjectNew.enabled = false;
    }

    public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M2MomRoomSceneStart", "M2MomRoomSceneEnd");
	}

    public override void SetSala()
    {
        if (MissionManager.instance.countLivingroomDialog == 0)
        {
            MissionManager.instance.rpgTalk.NewTalk("M2LivingRoomSceneStart", "M2LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
        }

        if (secao == enumMission.RESPEITA_MAE)
        {
            GameObject armario = GameObject.Find("Armario").gameObject;
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