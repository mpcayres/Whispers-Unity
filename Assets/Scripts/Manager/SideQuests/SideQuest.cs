using UnityEngine;
using UnityEngine.UI;

public abstract class SideQuest
{
    protected bool active = true, success = false;
    protected bool hasCat = false, hasRaven = false, invertBlocked = false, wasInverted = false;
    protected string oldScene = "";

    protected float timeEscape = 10f, counterTimeEscape = 0f;
    protected GameObject timerSideQuest;

    protected float oldX = 0, oldY = 0;
    protected int oldDir = 0;

    public float sideX = 0f, sideY = 8f;
    public int sideDir = 3;

    public abstract void InitSideQuest();

    public abstract void UpdateSideQuest();

    public abstract void ShowFlashback();

    protected void SetInitialSettings()
    {
        oldScene = MissionManager.instance.previousSceneName;

        GameObject player = GameObject.Find("Player").gameObject;
        oldX = player.transform.position.x;
        oldY = player.transform.position.y;
        oldDir = player.GetComponent<Player>().direction;

        MissionManager.initX = sideX;
        MissionManager.initY = sideY;
        MissionManager.initDir = sideDir;

        if (Cat.instance != null)
        {
            Cat.instance.gameObject.SetActive(false);
            hasCat = true;
        }
        if (Corvo.instance != null)
        {
            Corvo.instance.gameObject.SetActive(false);
            hasRaven = true;
        }

        invertBlocked = MissionManager.instance.invertWorldBlocked;
        MissionManager.instance.invertWorldBlocked = false;
        wasInverted = MissionManager.instance.invertWorld;
        MissionManager.instance.InvertWorld(false);
        MissionManager.instance.paused = false;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }
    }

    protected void SetDoor(float x, float y)
    {
        GameObject door = GameObject.Find("ExitSideQuestDoor").gameObject;
        door.transform.position = new Vector3(x, y, 0f);
        switch (oldScene)
        {
            case "Sala":
                door.tag = "DoorToLivingroom";
                break;
            case "Corredor":
                door.tag = "DoorToAlley";
                break;
            case "Jardim":
                door.tag = "DoorToGarden";
                break;
            case "Cozinha":
                door.tag = "DoorToKitchen";
                break;
            case "QuartoMae":
                door.tag = "DoorToMomRoom";
                break;
            case "QuartoKid":
                door.tag = "DoorToKidRoom";
                break;
            case "Banheiro":
                door.tag = "DoorToBathroom";
                break;
            case "Porao":
                door.tag = "DoorToBasement";
                break;
        }
    }

    protected void SetTimeToEscape()
    {
        timerSideQuest = MissionManager.instance.AddObjectWithParent("UI/TimerSideQuest", GameObject.Find("HUDCanvas").transform);
        timerSideQuest.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        timerSideQuest.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -40, 0);
    }

    protected void UpdateTimeToEscape()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(counterTimeEscape);
        string timerFormatted = string.Format("{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
        timerSideQuest.GetComponent<Text>().text = timerFormatted;
    }

    protected void DeleteTimeToEscape()
    {
        if (timerSideQuest != null)
        {
            GameObject.Destroy(timerSideQuest);
            timerSideQuest = null;
        }
    }

    public void EndSideQuest()
    {
        MissionManager.initX = oldX;
        MissionManager.initY = oldY;
        MissionManager.initDir = oldDir;

        if (hasCat)
        {
            Cat.instance.gameObject.SetActive(true);
        }
        if (hasRaven)
        {
            Corvo.instance.gameObject.SetActive(true);
        }

        MissionManager.instance.invertWorldBlocked = invertBlocked;
        MissionManager.instance.InvertWorld(true);
        MissionManager.instance.paused = true;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }

        if (success)
        {
            MissionManager.instance.sideQuests++;
            MissionManager.instance.UpdateSave();
            ShowFlashback();
        }
        else
        {
            EndFlashback();
        }

        active = false;
        DeleteTimeToEscape();
        ExtrasManager.EndSideQuest();
    }

    protected void EndFlashback()
    {
        MissionManager.instance.InvertWorld(wasInverted);
        MissionManager.instance.paused = false;
    }

}
