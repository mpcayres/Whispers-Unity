
public class ExtrasManager
{

    public static void SideQuestsManager()
    {
        int current = MissionManager.instance.currentMission;
        int unlocked = MissionManager.instance.unlockedMission;
        int sideQuests = MissionManager.instance.sideQuests;

        if (unlocked >= 4 && sideQuests == 0 && current >= 4 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 1
        }
        else if (unlocked >= 5 && sideQuests == 1 && current >= 5 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 2
        }
        else if (unlocked >= 6 && sideQuests == 2 && current >= 6 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 3
        }
        else if (unlocked >= 7 && sideQuests == 3 && current >= 7 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 4
        }
        if (unlocked >= 8 && sideQuests == 4 && current >= 8 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 5
        }
    }

    public static void PagesManager()
    {
        int current = MissionManager.instance.currentMission;
        int unlocked = MissionManager.instance.unlockedMission;
        int pages = MissionManager.instance.numberPages;

        if (unlocked >= 5 && pages == 0 && current >= 5 && current != 11)
        {
            // set page 1
        }
        else if (unlocked >= 6 && pages == 1 && current >= 6 && current != 11)
        {
            // set page 2
        }
        else if (unlocked >= 7 && pages == 2 && current >= 7 && current != 11)
        {
            // set page 3
        }
        else if (unlocked >= 8 && pages == 3 && current >= 8 && current != 11)
        {
            // set page 4
        }
        if (unlocked >= 9 && pages == 4 && current >= 9 && current != 11)
        {
            // set page 5
        }
        if (unlocked >= 10 && pages == 5 && current >= 10 && current != 11)
        {
            // set page 6
        }
    }

}