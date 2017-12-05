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
    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{ 

	}

	public override void SetCorredor()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M7CorridorSceneStart", "M7CorridorSceneEnd");
	}

	public override void SetCozinha()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M7KitchenSceneStart", "M7KitchenSceneEnd");
	}

	public override void SetJardim()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M7GardenSceneStart", "M7GardenSceneEnd");
	}

	public override void SetQuartoKid()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M7KidRoomSceneStart", "M7KidRoomSceneEnd");
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
    }

    /*public void AddCountCorridorDialog(){
		MissionManager.instance.countLivingroomDialog++;

	}*/
}