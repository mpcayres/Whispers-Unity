using UnityEngine;

public class SideQuest1 : SideQuest
{
    SpiritManager spiritManager;

    public override void InitSideQuest()
    {
        if (!MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            // Determinar posição do player (sideX e sideY)
            SetInitialSettings();
        }

        // Determinar posição da porta
        //GameObject.Find("ExitSideQuestDoor").gameObject.transform.position = new Vector3(0f, 0f, 0f);

        // Determinar conjuntos de espíritos
        GameObject holder = MissionManager.instance.AddObject("Scenery/SpiritHolder", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        spiritManager = holder.GetComponent<SpiritManager>();
        spiritManager.GenerateSpiritMap(3f, 0f, -5f, 4);
    }

    public override void UpdateSideQuest()
    {

    }

    public override void ShowFlashback()
    {
        EndFlashback();
    }

}
