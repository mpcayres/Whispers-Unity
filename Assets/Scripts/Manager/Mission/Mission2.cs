using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2 : Mission
{
	//public RPGTalk rpgTalk;

    public override void InitMission()
    {
        sceneInit = "Sala";
        MissionManager.initMission = true;
        MissionManager.initX = (float)-3.5;
        MissionManager.initY = (float)0.15;
        MissionManager.initDir = 1;

        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    { 
		
    }

    public override void SetCorredor()
    {
		MissionManager.instance.rpgTalk.NewTalk ("M2CorridorSceneStart", "M2CorridorSceneEnd");
    }

	public override void SetCozinha()
	{
		MissionManager.instance.rpgTalk.NewTalk ("M21KitchenSceneStart", "M2KitchenSceneEnd");
	}

	public override void SetJardim()
	{
		MissionManager.instance.rpgTalk.NewTalk ("M2GardenSceneStart", "M2GardenSceneEnd");
	}

    public override void SetQuartoKid()
    {
		MissionManager.instance.rpgTalk.NewTalk ("M2KidRoomSceneStart", "M2KidRoomSceneEnd");
    }

	public override void SetQuartoMae()
	{
		MissionManager.instance.rpgTalk.NewTalk ("M2MomRoomSceneStart", "M2MomRoomSceneEnd");
	}


    public override void SetSala()
    {
        MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        GameObject.Find("PickUpLanterna").gameObject.SetActive(false);
		MissionManager.instance.rpgTalk.NewTalk ("M2LivingroomSceneStart", "M2LivingroomSceneEnd");
    }

}