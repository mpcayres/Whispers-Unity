using UnityEngine;
using CrowShadowManager;
using CrowShadowNPCs;
using CrowShadowPlayer;

public abstract class Mission {

    public string sceneInit = "";

    public int tipTimerSmall = 40;
    public int tipTimerMedium = 60;
    public int tipTimerLonger = 80;
    public bool usedTip1 = false;

    public void LoadMission()
    {

    }

    public void LoadMissionScene()
    {
        if (GameManager.instance.currentSceneName.Equals("Corredor"))
        {
            SetCorredor();
        }
        else if (GameManager.instance.currentSceneName.Equals("Cozinha"))
        {
            SetCozinha();
        }
        else if (GameManager.instance.currentSceneName.Equals("Jardim"))
        {
            SetJardim();
        }
        else if (GameManager.instance.currentSceneName.Equals("QuartoKid"))
        {
            SetQuartoKid();
        }
        else if (GameManager.instance.currentSceneName.Equals("QuartoMae"))
        {
            SetQuartoMae();
        }
        else if (GameManager.instance.currentSceneName.Equals("Sala"))
        {
            SetSala();
        }
    }

    public abstract void InitMission();

    public abstract void UpdateMission();

    public abstract void SetCorredor();

    public abstract void SetCozinha();

    public abstract void SetJardim();

    public abstract void SetQuartoKid();

    public abstract void SetQuartoMae();

    public abstract void SetSala();

    public abstract void EspecificaEnum(int pos);

    public abstract void ForneceDica();

    public virtual void AreaTriggered(string tag) { } // para chamar quando uma área é ativada

    public virtual void InvokeMission() { } // para chamar após um determinado tempo

    public virtual void InvokeMissionChoice(int id) { } // para chamar após uma escolha ser feita

    public void SetInitialSettings()
    {
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Crow.instance != null) Crow.instance.DestroyRaven();

        GameManager.instance.paused = false;

        GameObject player = GameObject.Find("Player").gameObject;
        GameObject fosforo = player.transform.Find("Fosforo").gameObject;
        GameObject isqueiro = player.transform.Find("Isqueiro").gameObject;
        GameObject faca = player.transform.Find("Faca").gameObject;
        GameObject pedra = player.transform.Find("Pedra").gameObject;

        fosforo.GetComponent<MiniGameObject>().achievedGoal = false;
        isqueiro.GetComponent<MiniGameObject>().achievedGoal = false;
        //faca.GetComponent<MiniGameObject>().achievedGoal = false;
        //pedra.GetComponent<MiniGameObject>().achievedGoal = false;

        fosforo.GetComponent<MiniGameObject>().activated = false;
        isqueiro.GetComponent<MiniGameObject>().activated = false;
        //faca.GetComponent<MiniGameObject>().activated = false;
        //pedra.GetComponent<MiniGameObject>().activated = false;

        if (GameManager.instance.rpgTalk.isPlaying)
        {
            GameManager.instance.rpgTalk.EndTalk();
        }
    }

}
