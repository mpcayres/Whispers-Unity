using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowNPCs;
using CrowShadowObjects;
using CrowShadowPlayer;
using CrowShadowScenery;

public class Mission5 : Mission {
    enum enumMission { NIGHT, INICIO, OPENED, FINAL};
    enumMission secao;

    private float timeToTip = 2;
    private int timesInMI = 0;

    public override void InitMission()
	{
		sceneInit = "QuartoKid";
		GameManager.initMission = true;
		GameManager.initX = (float) 3.0;
		GameManager.initY = (float) 0.2;
		GameManager.initDir = 3;
        GameManager.LoadScene(sceneInit);
        secao = enumMission.NIGHT;
        Book.bookBlocked = false;

        GameManager.instance.invertWorld = false;
        GameManager.instance.invertWorldBlocked = false;

        SetInitialSettings();
    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{
        if (secao == enumMission.NIGHT)
        {
            if (!GameManager.instance.showMissionStart)
            {
                EspecificaEnum((int)enumMission.INICIO);
            }
        }
        else if (secao == enumMission.INICIO && 
            (CrossPlatformInputManager.GetButtonDown("keyJournal") || Inventory.GetCurrentItemType() == Inventory.InventoryItems.LIVRO)) {
            EspecificaEnum((int)enumMission.OPENED);
        }
    }

	public override void SetCorredor()
	{
        GameManager.instance.scenerySounds.StopSound();

        GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
        portaMae.GetComponent<SceneDoor>().isOpened = false;

        GameObject portaCozinha = GameObject.Find("DoorToKitchen").gameObject;
        portaCozinha.GetComponent<SceneDoor>().isOpened = false;

        GameObject portaQuarto = GameObject.Find("DoorToKidRoom").gameObject;
        portaQuarto.GetComponent<SceneDoor>().isOpened = false;
        
        GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
        portaSala.GetComponent<SceneDoor>().isOpened = false;
        
        GameObject.Find("VasoNaoEmpurravel").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/vasoPlanta_quebrado");

        //GameManager.instance.rpgTalk.NewTalk ("M5CorridorSceneStart", "M5CorridorSceneEnd");
    }

	public override void SetCozinha()
	{
        GameManager.instance.scenerySounds.PlayDrop();
        //GameManager.instance.rpgTalk.NewTalk ("M5KitchenSceneStart", "M5KitchenSceneEnd");

        // Panela para caso ainda não tenha
        if (!Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panela.GetComponent<ScenePickUpObject>().enabled = true;
        }
    }

	public override void SetJardim()
    {
        
        GameObject portaSala = GameObject.Find("DoorToLivingRoom").gameObject;
        portaSala.GetComponent<SceneDoor>().isOpened = false;

    }

	public override void SetQuartoKid()
	{
        if(secao == enumMission.NIGHT)
        {
            GameManager.instance.rpgTalk.NewTalk("M5KidRoomSceneStart", "M5KidRoomSceneEnd");
            // Porta
            GameObject porta = GameObject.Find("DoorToAlley").gameObject;
            float portaDefaultX = porta.transform.position.x;
            float portaDefaultY = porta.transform.position.y;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.GetComponent<SceneDoor>().isOpened = false;
            porta.transform.position = new Vector3(porta.transform.position.x - posX, portaDefaultY, porta.transform.position.z);

        }

        if (GameManager.instance.mission2ContestaMae)
        {
            // Arranhao
            GameManager.instance.AddObject("Scenery/Garra", "", new Vector3(-1.48f, 1.81f, 0), new Vector3(0.1f, 0.1f, 1));
        }
        else
        {
            // Vela
            GameObject velaFixa = GameObject.Find("velaMesa").gameObject;
            velaFixa.transform.GetChild(0).gameObject.SetActive(true);
            velaFixa.transform.GetChild(1).gameObject.SetActive(true);
            velaFixa.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 140;
        }

        if (Cat.instance == null)
        {
            // Gato
            GameObject cat = GameManager.instance.AddObject(
                "NPCs/catFollower", "", new Vector3(player.transform.position.x + 0.6f, player.transform.position.y, 0), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().FollowPlayer();
        }
    }

	public override void SetQuartoMae()
	{
		//GameManager.instance.rpgTalk.NewTalk ("M5MomRoomSceneStart", "M5MomRoomSceneEnd");
	}


    public override void SetSala()
    {
        
    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        GameManager.instance.Print("SECAO: " + secao);

        if (secao == enumMission.INICIO)
        {

        }
        else if (secao == enumMission.OPENED)
        {
            ExtrasManager.canActivateSide1 = true;
            ExtrasManager.SideQuestsManager();
        }
    }

    public override void ForneceDica()
    {

    }


}