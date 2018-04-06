using UnityEngine;

public class ExtrasManager
{

    public static SideQuest sideQuest;

    public static void InitSideQuest(int numSideQuest)
    {
        sideQuest = null;
        switch (numSideQuest)
        {
            case 1:
                sideQuest = new SideQuest1();
                break;
            case 2:
                sideQuest = new SideQuest2();
                break;
            case 3:
                sideQuest = new SideQuest3();
                break;
            case 4:
                sideQuest = new SideQuest4();
                break;
            case 5:
                sideQuest = new SideQuest5();
                break;
        }

        if (sideQuest != null)
        {
            MissionManager.LoadScene("SideQuest");
        }   
    }

    public static void SideQuestsManager()
    {
        int current = MissionManager.instance.currentMission;
        int sideQuests = MissionManager.instance.sideQuests;

        if (sideQuests == 0 && current >= 4 && current != 9 && current != 10 && current != 11 
            && MissionManager.instance.currentSceneName.Equals("QuartoKid"))
        {
            // set side quest 1
            GameObject quest1 = MissionManager.instance.AddObject(
                "Scenery/SideQuestObject", "", new Vector3(0f, 0f, 0f), new Vector3(1, 1, 1));
            quest1.GetComponent<SideQuestObject>().numSideQuest = 1;
        }
        else if (sideQuests == 1 && current >= 5 && current != 9 && current != 10 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 2
        }
        else if (sideQuests == 2 && current >= 6 && current != 9 && current != 10 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 3
        }
        else if (sideQuests == 3 && current >= 7 && current != 9 && current != 10 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 4
        }
        if (sideQuests == 4 && current >= 8 && current != 9 && current != 10 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 5
        }
    }

    public static void PagesManager()
    {
        int current = MissionManager.instance.currentMission;
        int pages = MissionManager.instance.numberPages;

        if (pages == 0 && current >= 5 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 1
        }
        else if (pages == 1 && current >= 6 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 2
        }
        else if (pages == 2 && current >= 7 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 3
        }
        else if (pages == 3 && current >= 8 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 4
        }
        if (pages == 4 && current >= 9 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 5
        }
        if (pages == 5 && current >= 10 && current != 11
            && MissionManager.instance.currentSceneName.Equals(""))
        {
            // set page 6
        }
    }

}