using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4 : Mission {
    enum enumMission { INICIO, FRENTE_CRIADO, GRANDE_BARULHO, VASO_GATO, VASO_SOZINHO, INDICACAO_NECESSIDADE, FINAL };
    enumMission secao;

    public override void InitMission()
	{
		sceneInit = "QuartoKid";
		MissionManager.initMission = true;
        MissionManager.initX = (float)-2.5;
        MissionManager.initY = (float)0.7;
        MissionManager.initDir = 1;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.INICIO;
        if (Cat.instance != null) Cat.instance.DestroyCat();
    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{
        
    }

	public override void SetCorredor()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4CorridorSceneStart", "M4CorridorSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetCozinha()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4KitchenSceneStart", "M4KitchenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Panela para caso ainda não tenha
        if (!Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            ScenePickUpObject panelaPickUp = panela.AddComponent<ScenePickUpObject>();
            panelaPickUp.sprite1 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panelaPickUp.sprite2 = Resources.Load<Sprite>("Sprites/Objects/Scene/panela");
            panelaPickUp.item = Inventory.InventoryItems.TAMPA;
            panelaPickUp.blockAfterPick = true;
        }
    }

    public override void SetJardim()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4GardenSceneStart", "M4GardenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetQuartoKid()
	{
        if (MissionManager.instance.countKidRoomDialog == 0)
        {
            MissionManager.instance.rpgTalk.NewTalk("M4KidRoomSceneStart", "M4KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
        }

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetQuartoMae()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4MomRoomSceneStart", "M4MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }


    public override void SetSala()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4LivingRoomSceneStart", "M4LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.FRENTE_CRIADO)
        {
            MissionManager.instance.rpgTalk.NewTalk("frenteCriadoStart", "frenteCriadoEnd");
        }
        else if (secao == enumMission.GRANDE_BARULHO)
        {
            MissionManager.instance.rpgTalk.NewTalk("GrandeBarulhoStart", "GrandeBarulhoEnd");
        }
        else if (secao == enumMission.INDICACAO_NECESSIDADE)
        {
            MissionManager.instance.rpgTalk.NewTalk("IndicarNecessidade", "IndicarNecessidadeEnd");
        }
        else if (secao == enumMission.FINAL)
        {
            MissionManager.instance.rpgTalk.NewTalk("Final", "FinalEnd");
        }
    }

    public void AddCountKidRoomDialog()
    {
        MissionManager.instance.countKidRoomDialog++;
    }
}