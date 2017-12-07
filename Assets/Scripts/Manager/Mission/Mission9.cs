using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission9 : Mission {
    enum enumMission { NIGHT, INICIO, FINAL };
    enumMission secao;

    bool endCat = false;

    public override void InitMission()
    {
        sceneInit = "QuartoKid";
        MissionManager.initMission = true;
        MissionManager.initX = (float)1.5;
        MissionManager.initY = (float)0.2;
        MissionManager.initDir = 3;
        SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        if (MissionManager.instance.pathCat >= MissionManager.instance.pathBird) endCat = true;
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {
        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                EspecificaEnum((int)enumMission.INICIO);
            }
        }
    }

    public override void SetCorredor()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void SetCozinha()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void SetJardim()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void SetQuartoKid()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void SetQuartoMae()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void SetSala()
    {
        if (endCat)
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventCat").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("FireEventHolder").gameObject.transform.Find("FireEventBird").gameObject.SetActive(true);
        }
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission) pos;
        MissionManager.instance.Print("SECAO: " + secao);
    }
}
