using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission3 : Mission {
    enum enumMission { NIGHT, INICIO, QUADRO, MI_DESBLOQUEADO, CORVO_ATACA, MI_TRAVADO, MAE_APARECE, FINAL };
    enumMission secao;

    public override void InitMission()
	{
		sceneInit = "QuartoKid";
		MissionManager.initMission = true;
		MissionManager.initX = (float) -2.5;
		MissionManager.initY = (float) -1.6;
		MissionManager.initDir = 3;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
    }

    public override void UpdateMission() //aqui coloca as ações do update específicas da missão
    {

        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                secao = enumMission.INICIO;
                MissionManager.instance.rpgTalk.NewTalk("M3KidRoomSceneStart", "M3KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
            }
        }
    }

	public override void SetCorredor()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M3CorridorSceneStart", "M3CorridorSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void SetCozinha()
    {
        //MissionManager.instance.rpgTalk.NewTalk ("M3KitchenSceneStart", "M3KitchenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

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
        //MissionManager.instance.rpgTalk.NewTalk ("M3GardenSceneStart", "M3GardenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

	public override void SetQuartoKid()
	{
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Condicoes a partir da missao 2
        if (MissionManager.instance.mission2ContestaMae)
        {
            // colocar arranhao
        }
        else
        {
            // colocar luz
        }
	}

	public override void SetQuartoMae()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M3MomRoomSceneStart", "M3MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }


	public override void SetSala()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M3LivingRoomSceneStart", "M3LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(-20, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.QUADRO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M3Painting", "M3PaintingEnd");
        }
        else if (secao == enumMission.MI_DESBLOQUEADO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M3MundoInvertido", "M3MundoInvertidoEnd");
        }
        else if (secao == enumMission.MI_TRAVADO)
        {
            MissionManager.instance.rpgTalk.NewTalk("M3VoltaMundoInvertido", "M3VoltaMundoInvertidoEnd");
        }
    }

    public void AddCountKidRoomDialog()
    {
        MissionManager.instance.countKidRoomDialog++;
    }
}