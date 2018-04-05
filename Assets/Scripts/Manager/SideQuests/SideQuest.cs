using UnityEngine;

public abstract class SideQuest
{
    protected bool success = false;
    protected bool hasCat = false, hasRaven = false, invertBlocked = false, wasInverted = false;
    protected string oldScene = "";
    protected float oldX = 0, oldY = 0, sideX = 0, sideY = 0;
    protected int oldDir = 0, sideDir = 0;

    public abstract void InitSideQuest();

    public abstract void UpdateSideQuest();

    public abstract void ShowFlashback();

    protected void SetInitialSettings()
    {
        oldScene = MissionManager.instance.currentSceneName;

        GameObject player = GameObject.Find("Player").gameObject;
        oldX = player.transform.position.x;
        oldY = player.transform.position.y;
        oldDir = player.GetComponent<Player>().direction;

        MissionManager.initX = sideX;
        MissionManager.initY = sideY;
        MissionManager.initDir = sideDir;
        MissionManager.LoadScene("SideQuest");

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

    public void EndSideQuest()
    {
        MissionManager.initX = oldX;
        MissionManager.initY = oldY;
        MissionManager.initDir = oldDir;
        MissionManager.LoadScene(oldScene);

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
    }

    protected void EndFlashback()
    {
        MissionManager.instance.InvertWorld(wasInverted);
        MissionManager.instance.paused = false;
    }

}
