using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission7 : Mission {
    enum enumMission { INICIO, FINAL };
    enumMission secao;

    public override void InitMission()
	{
		sceneInit = "Sala";
		MissionManager.initMission = true;
		MissionManager.initX = (float) 3;
		MissionManager.initY = (float) 0.2;
		MissionManager.initDir = 3;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.INICIO;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();
    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{ 

	}

	public override void SetCorredor()
	{
        //desaparecer com o livro que está no corredor
        GameObject.Find("livro").gameObject.SetActive(false);
        //MissionManager.instance.rpgTalk.NewTalk ("M7CorridorSceneStart", "M7CorridorSceneEnd");
    }

	public override void SetCozinha()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M7KitchenSceneStart", "M7KitchenSceneEnd");

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
		//MissionManager.instance.rpgTalk.NewTalk ("M7GardenSceneStart", "M7GardenSceneEnd");
	}

	public override void SetQuartoKid()
	{
        if (MissionManager.instance.countLivingroomDialog == 0)
        {
            MissionManager.instance.rpgTalk.NewTalk("M7KidRoomSceneStart", "M7KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        }
	}

	public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M7MomRoomSceneStart", "M7MomRoomSceneEnd");
	}


	public override void SetSala()
	{
		//MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
		//GameObject.Find("PickUpLanterna").gameObject.SetActive(false);	
		//	MissionManager.instance.rpgTalk.NewTalk ("M7LivingRoomSceneStart", "M7LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
	}

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);
    }

    public void AddCountKidRoomDialog(){
		MissionManager.instance.countKidRoomDialog++;

	}
}