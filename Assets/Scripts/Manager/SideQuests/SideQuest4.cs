﻿using UnityEngine;
using System.Collections.Generic;

public class SideQuest4 : SideQuest
{
    SpiritManager spiritManager;
    HelpingLightManager lightManager;

    public override void InitSideQuest()
    {
        if (!MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            // Determinar posição do player (sideX e sideY)
            sideX = 0f; sideY = 8f;
            sideDir = 3;
            // Determinar tempo para terminar o nível
            timeEscape = 10f;
            SetInitialSettings();
        }

        // Determinar posição da porta
        SetDoor(0f, 0f);

        // Determinar conjuntos de espíritos
        List<float> radius = new List<float>(), originX = new List<float>(), originY = new List<float>();
        radius.Add(4f); originX.Add(0f); originY.Add(-10f);

        GameObject holderSpirit = MissionManager.instance.AddObject("Scenery/SpiritHolder", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        spiritManager = holderSpirit.GetComponent<SpiritManager>();
        spiritManager.GenerateSpiritMap(radius[0], originX[0], originY[0], 4, 2, 1, true, 3);

        // Determinar posição das luzes de borda (posição inicial do player e locais dos espíritos
        GameObject holderLight = MissionManager.instance.AddObject("Scenery/LightHolder", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        lightManager = holderLight.GetComponent<HelpingLightManager>();
        lightManager.GenerateBorderLightMap(2, 0, sideX, sideY, radius, originX, originY);

        // Determinar luzes
        //AddLight(1, 0f, 0f, 0f, false, 0, null);
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
