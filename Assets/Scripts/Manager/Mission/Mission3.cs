using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission3 : Mission
{

	public override void InitMission()
	{
		sceneInit = "QuartoKid";
		MissionManager.initMission = true;
		MissionManager.initX = (float) 1.5;
		MissionManager.initY = (float) 0.2;
		MissionManager.initDir = 3;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
	}

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{ 

	}

	public override void SetCorredor()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M3CorridorSceneStart", "M3CorridorSceneEnd");
	}

	public override void SetCozinha()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M3KitchenSceneStart", "M3KitchenSceneEnd");
	}

	public override void SetJardim()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M3GardenSceneStart", "M3GardenSceneEnd");
	}

	public override void SetQuartoKid()
	{
		if (MissionManager.instance.countKidRoomDialog == 0) {
            MissionManager.instance.rpgTalk.NewTalk ("M3KidRoomSceneStart", "M3KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        
        }
	}

	public override void SetQuartoMae()
	{
		//MissionManager.instance.rpgTalk.NewTalk ("M3MomRoomSceneStart", "M3MomRoomSceneEnd");
	}


	public override void SetSala()
	{
		//MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
		//GameObject.Find("PickUpLanterna").gameObject.SetActive(false);	
		//	MissionManager.instance.rpgTalk.NewTalk ("M3LivingRoomSceneStart", "M3LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
	}
	/*public void AddCountCorridorDialog(){
		MissionManager.instance.countLivingroomDialog++;

	}*/
}