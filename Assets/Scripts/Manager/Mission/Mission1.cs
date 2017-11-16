using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Mission1 : Mission {


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
        //MissionManager.instance.AddObject("PickUpLanterna", new Vector3((float)-3.37, (float)-0.47, 0), new Vector3(1, 1, 1));
    }

}
