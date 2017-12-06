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

}
