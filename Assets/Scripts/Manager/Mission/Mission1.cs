using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission1 : Mission {
	private int countKidRoomDialog = 0;

    public override void InitMission()
    {
        // colocar de volta para versão final
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
        MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
		MissionManager.instance.rpgTalk.NewTalk ("M1CorridorSceneStart", "M1CorridorSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountCorridorDialog");

    }

    public override void SetCozinha()
    {
		//MissionManager.instance.rpgTalk.NewTalk ("M1KitchenSceneStart", "M1KitchenSceneEnd");
    }

    public override void SetJardim()
    {
		//MissionManager.instance.rpgTalk.NewTalk ("M1GardenSceneStart", "M1GardenSceneEnd");
    }

    public override void SetQuartoKid()
    {

		if (MissionManager.instance.countKidRoomDialog == 0) {
			MissionManager.instance.rpgTalk.NewTalk ("M1KidRoomSceneStart", "M1KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
		
		} else if (MissionManager.instance.countKidRoomDialog == 1) {
			MissionManager.instance.rpgTalk.NewTalk ("M1KidRoomSceneRepeat", "M1KidRoomSceneRepeatEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
		}
  }

    public override void SetQuartoMae()
    {
		//MissionManager.instance.rpgTalk.NewTalk ("M1MomRoomSceneStart", "M1MomRoomSceneEnd");
    }

    public override void SetSala()
    {
        MissionManager.instance.AddObject("PickUpLanterna", new Vector3((float)-3.37, (float)-0.47, 0), new Vector3(1, 1, 1));
		//MissionManager.instance.rpgTalk.NewTalk ("M1LivingroomSceneStart", "M1LivingroomSceneEnd");
    }
		
	public void AddCountKidRoomDialog(){
		MissionManager.instance.countKidRoomDialog++;

	}
	public void AddCountCorridorDialog(){
		MissionManager.instance.countCorridorDialog++;

	}
}
