using UnityEngine;

public class SideQuest5 : SideQuest
{

    public override void InitSideQuest()
    {
        sideX = -2.5f;
        sideY = 0.7f;
        sideDir = 0;
        SetInitialSettings();
    }

    public override void UpdateSideQuest()
    {

    }

    public override void ShowFlashback()
    {
        EndFlashback();
    }

}
