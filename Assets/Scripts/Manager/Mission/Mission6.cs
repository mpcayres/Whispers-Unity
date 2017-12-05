using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission6 : Mission {
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
		//MissionManager.instance.rpgTalk.NewTalk ("M6CorridorSceneStart", "M6CorridorSceneEnd");
	}

	public override void SetCozinha()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M6KitchenSceneStart", "M6KitchenSceneEnd");
	}

	public override void SetJardim()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M6GardenSceneStart", "M6GardenSceneEnd");
	}

	public override void SetQuartoKid()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M6KidRoomSceneStart", "M6KidRoomSceneEnd");
	}

	public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M6MomRoomSceneStart", "M6MomRoomSceneEnd");
	}


	public override void SetSala()
	{
		//MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
		//GameObject.Find("PickUpLanterna").gameObject.SetActive(false);	
		//	MissionManager.instance.rpgTalk.NewTalk ("M6LivingRoomSceneStart", "M6LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
	}

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
    }

    /*public void AddCountCorridorDialog(){
		MissionManager.instance.countLivingroomDialog++;

	}*/
}