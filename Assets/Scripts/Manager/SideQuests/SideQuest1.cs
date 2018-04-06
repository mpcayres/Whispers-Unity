using UnityEngine;

public class SideQuest1 : SideQuest
{
    SpiritManager spiritManager;

    public override void InitSideQuest()
    {
        if (!MissionManager.instance.previousSceneName.Equals("GameOver")) {
            sideX = -2.5f;
            sideY = 0.7f;
            sideDir = 0;
            SetInitialSettings();
        }

        GameObject holder = MissionManager.instance.AddObject("Scenery/SpiritHolder", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        spiritManager = holder.GetComponent<SpiritManager>();
        spiritManager.GenerateSpiritMap();
    }

    public override void UpdateSideQuest()
    {

    }

    public override void ShowFlashback()
    {
        EndFlashback();
    }

}
