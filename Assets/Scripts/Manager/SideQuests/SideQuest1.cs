using UnityEngine;

public class SideQuest1 : SideQuest
{
    SpiritManager spiritManager;

    public override void InitSideQuest()
    {
        if (!MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            // Determinar posição do player (sideX e sideY)
            sideX = 0f; sideY = 18f;
            sideDir = 3;
            // Determinar tempo para terminar o nível
            timeEscape = 10f;
            SetInitialSettings();
        }

        // Determinar posição da porta
        SetDoor(0f, 20f);

        // Determinar conjuntos de espíritos
        GameObject holder = MissionManager.instance.AddObject("Scenery/SpiritHolder", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        spiritManager = holder.GetComponent<SpiritManager>();
        spiritManager.GenerateSpiritMap(3f, 0f, -5f, 4);
    }

    public override void UpdateSideQuest()
    {
        if (success && counterTimeEscape > 0f && active)
        {
            counterTimeEscape -= Time.deltaTime;
            UpdateTimeToEscape();
            if (counterTimeEscape <= 0f)
            {
                success = false;
                DeleteTimeToEscape();
                MissionManager.instance.GameOver();
            }
        }
        // Conferir se todos os SpiritManagers alcançaram sucesso
        else if (spiritManager.success && !success)
        {
            success = true;
            counterTimeEscape = timeEscape;
            SetTimeToEscape();
        }
    }

    public override void ShowFlashback()
    {
        EndFlashback();
    }

}
