
public class ExtrasManager
{

    public static void SideQuestsManager()
    {
        int current = MissionManager.instance.currentMission;
        int sideQuests = MissionManager.instance.sideQuests;

        if (sideQuests == 0 && current >= 4 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 1
        }
        else if (sideQuests == 1 && current >= 5 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 2
        }
        else if (sideQuests == 2 && current >= 6 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 3
        }
        else if (sideQuests == 3 && current >= 7 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 4
        }
        if (sideQuests == 4 && current >= 8 && current != 9 && current != 10 && current != 11)
        {
            // set side quest 5
        }
    }

    public static void PagesManager()
    {
        int current = MissionManager.instance.currentMission;
        int pages = MissionManager.instance.numberPages;

        if (pages == 0 && current >= 5 && current != 11)
        {
            // set page 1
        }
        else if (pages == 1 && current >= 6 && current != 11)
        {
            // set page 2
        }
        else if (pages == 2 && current >= 7 && current != 11)
        {
            // set page 3
        }
        else if (pages == 3 && current >= 8 && current != 11)
        {
            // set page 4
        }
        if (pages == 4 && current >= 9 && current != 11)
        {
            // set page 5
        }
        if (pages == 5 && current >= 10 && current != 11)
        {
            // set page 6
        }
    }

}