using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission9 : Mission {

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
       
    }

    public override void SetCozinha()
    {
		
    }

    public override void SetJardim()
    {
		
    }

    public override void SetQuartoKid()
    {

    }

    public override void SetQuartoMae()
    {
		
    }

    public override void SetSala()
    {
       
    }
}
