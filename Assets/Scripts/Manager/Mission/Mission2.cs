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
        MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        GameObject.Find("PickUpLanterna").gameObject.SetActive(false);	
		if(MissionManager.instance.countLivingroomDialog == 0)
			MissionManager.instance.rpgTalk.NewTalk ("M2LivingRoomSceneStart", "M2LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");
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