using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2 : Mission
{

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
        MissionManager.instance.AddObject("MovingObject", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        GameObject.Find("PickUpLanterna").gameObject.SetActive(false);
    }

}