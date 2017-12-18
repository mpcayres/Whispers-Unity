using UnityEngine;

public abstract class Mission {

    public string sceneInit = "";

    public void LoadMission()
    {

    }

    public void LoadMissionScene()
    {
        if (MissionManager.instance.currentSceneName.Equals("Corredor"))
        {
            SetCorredor();
        }
        else if (MissionManager.instance.currentSceneName.Equals("Cozinha"))
        {
            SetCozinha();
        }
        else if (MissionManager.instance.currentSceneName.Equals("Jardim"))
        {
            SetJardim();
        }
        else if (MissionManager.instance.currentSceneName.Equals("QuartoKid"))
        {
            SetQuartoKid();
        }
        else if (MissionManager.instance.currentSceneName.Equals("QuartoMae"))
        {
            SetQuartoMae();
        }
        else if (MissionManager.instance.currentSceneName.Equals("Sala"))
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

    public virtual void AreaTriggered(string tag) { } // para chamar quando uma área é ativada

    public virtual void InvokeMission() { } // para chamar após um determinado tempo

    public virtual void InvokeMissionChoice(int id) { } // para chamar após uma escolha ser feita

    public void SetInitialSettings()
    {
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (Corvo.instance != null) Corvo.instance.DestroyRaven();

        MissionManager.instance.paused = false;

        GameObject player = GameObject.Find("Player").gameObject;
        GameObject fosforo = player.transform.Find("Fosforo").gameObject;
        GameObject isqueiro = player.transform.Find("Isqueiro").gameObject;
        GameObject faca = player.transform.Find("Faca").gameObject;
        GameObject pedra = player.transform.Find("Pedra").gameObject;

        fosforo.GetComponent<MiniGameObject>().achievedGoal = false;
        isqueiro.GetComponent<MiniGameObject>().achievedGoal = false;
        faca.GetComponent<MiniGameObject>().achievedGoal = false;
        pedra.GetComponent<MiniGameObject>().achievedGoal = false;

        fosforo.GetComponent<MiniGameObject>().activated = false;
        isqueiro.GetComponent<MiniGameObject>().activated = false;
        faca.GetComponent<MiniGameObject>().activated = false;
        pedra.GetComponent<MiniGameObject>().activated = false;

        if (MissionManager.instance.rpgTalk.isPlaying)
        {
            MissionManager.instance.rpgTalk.EndTalk();
        }
    }

}
