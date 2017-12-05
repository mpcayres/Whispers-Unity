using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4 : Mission {
    enum enumMission { INICIO, FRENTE_CRIADO, GRANDE_BARULHO, VASO_GATO, VASO_SOZINHO, FINAL };
    enumMission secao;

    public override void InitMission()
	{
		sceneInit = "Cozinha";
		MissionManager.initMission = true;
		MissionManager.initX = (float) 1.5;
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
		//MissionManager.instance.rpgTalk.NewTalk ("M4CorridorSceneStart", "M4CorridorSceneEnd");
	}

	public override void SetCozinha()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M4KitchenSceneStart", "M4KitchenSceneEnd");
	}

	public override void SetJardim()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M4GardenSceneStart", "M4GardenSceneEnd");
	}

	public override void SetQuartoKid()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M4KidRoomSceneStart", "M4KidRoomSceneEnd");
	}

	public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M4MomRoomSceneStart", "M4MomRoomSceneEnd");
	}


	public override void SetSala()
	{
		//MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
		//GameObject.Find("PickUpLanterna").gameObject.SetActive(false);	
		//	MissionManager.instance.rpgTalk.NewTalk ("M4LivingRoomSceneStart", "M4LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
	}

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
    }

    /*public void AddCountCorridorDialog(){
		MissionManager.instance.countLivingroomDialog++;

	}*/
}