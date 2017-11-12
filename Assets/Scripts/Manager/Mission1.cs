using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission1 : Mission {
    private bool decisionSet = false;
    private int optionSelected = -1;

    public override void InitMission()
    {
        // colocar de volta para versão final
        /*sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float) 1.5;
        MissionManager.initY = (float) 0.2;
        MissionManager.initDir = 3;
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);*/
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {
        if (!decisionSet)
        {
            //MissionManager.instance.SetDecision("Não matar", "Matar"); //define os textos para decisão
            decisionSet = true;
        }
        else if(optionSelected == -1)
        {
            //optionSelected = MissionManager.instance.MakeDecision(); //lógica de update para definir seleção
        }
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
