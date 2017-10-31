using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : Mission {
    private bool decisionSet = false;
    private int optionSelected = -1;

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

    }

}
