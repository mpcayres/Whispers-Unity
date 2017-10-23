using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : Mission {

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
