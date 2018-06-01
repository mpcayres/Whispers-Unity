using UnityEngine;

public class ExtrasManager
{
    //ATINGIU CONDIÇÕES DE ATIVAR RESPECTIVA SIDE QUEST
    public static bool canActivateSide1 = false;

    public static void InitSideQuest(int numSideQuest)
    {
        GameManager.instance.sideQuest = null;
        switch (numSideQuest)
        {
            case 1:
                GameManager.instance.sideQuest = new SideQuest1();
                break;
            case 2:
                GameManager.instance.sideQuest = new SideQuest2();
                break;
            case 3:
                GameManager.instance.sideQuest = new SideQuest3();
                break;
            case 4:
                GameManager.instance.sideQuest = new SideQuest4();
                break;
            case 5:
                GameManager.instance.sideQuest = new SideQuest5();
                break;
        }

        if (GameManager.instance.sideQuest != null)
        {
            GameManager.LoadScene("SideQuest");
        }   
    }

    public static void EndSideQuest()
    {
        GameManager.instance.sideQuest = null;
    }

    public static void SideQuestsManager()
    {
        int current = GameManager.instance.currentMission;
        int sideQuests = GameManager.instance.sideQuests;

        if (sideQuests == 0 && current >= 5 && current != 9 && current != 10 && current != 11 
            && GameManager.instance.currentSceneName.Equals("QuartoKid") && canActivateSide1)
        {
            // set side quest 1
            GameObject quest1 = GameManager.instance.AddObject(
                "Scenery/SideQuestObject", "", new Vector3(0f, 0f, 0f), new Vector3(1, 1, 1));
            quest1.GetComponent<SideQuestObject>().numSideQuest = 1;
        }
        else if (sideQuests == 1 && current >= 6 && current != 9 && current != 10 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 2
        }
        else if (sideQuests == 2 && current >= 7 && current != 9 && current != 10 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 3
        }
        else if (sideQuests == 3 && current >= 8 && current != 9 && current != 10 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 4
        }
        else if (sideQuests == 4 && Book.pageQuantity == 5 && current >= 12
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set side quest 5
            // flashback mostrando algo que desbloqueia a última página e chama o PagesManager ao final
        }
    }

    public static void PagesManager()
    {
        int current = GameManager.instance.currentMission;
        int pages = Book.pageQuantity;

        if (pages >= 0 && current >= 5 && current != 9 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set page 1
        }
        if (pages >= 1 && current >= 6 && current != 9 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set page 2
        }
        if (pages >= 2 && current >= 7 && current != 9 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set page 3
        }
        if (pages >= 3 && current >= 8 && current != 9 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set page 4
        }
        if (pages >= 4 && current >= 10 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // set page 5
        }
        if (pages == 5 && GameManager.instance.sideQuests == 5 && current >= 12 && current != 9 && current != 11
            && GameManager.instance.currentSceneName.Equals(""))
        {
            // página aonde ocorrer o flashback da sideQuest 5
            // set page 6
        }
    }

}