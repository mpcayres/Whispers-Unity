using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4 : Mission {
    enum enumMission { NIGHT, INICIO, GATO_CORREDOR, FRENTE_CRIADO, MI_DESATIVADO, MI_ATIVADO,ARMARIO, GRANDE_BARULHO, VASO_GATO, VASO_SOZINHO, QUEBRADO, INDICACAO_NECESSIDADE, POP_UP, MAE_CHEGA, VERDADE_MENTIRA,  FINAL };
    enumMission secao;

    float portaMaeDefaultY, portaMaeDefaultX;

    GameObject novelo;

    GameObject mom, bird;

    public override void InitMission()
	{
		sceneInit = "QuartoKid";
		MissionManager.initMission = true;
        MissionManager.initX = (float)-2.5;
        MissionManager.initY = (float)0.7;
        MissionManager.initDir = 1;
		SceneManager.LoadScene(sceneInit, LoadSceneMode.Single);
        secao = enumMission.NIGHT;
        if (Cat.instance != null) Cat.instance.DestroyCat();
        MissionManager.instance.invertWorldBlocked = false;

    }

	public override void UpdateMission() //aqui coloca as ações do update específicas da missão
	{
        if (secao == enumMission.NIGHT)
        {
            if (!MissionManager.instance.GetMissionStart())
            {
                EspecificaEnum((int)enumMission.INICIO);
                MissionManager.instance.rpgTalk.NewTalk("M4KidRoomSceneStart", "M4KidRoomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountKidRoomDialog");
           }
        }
        if (secao == enumMission.INICIO && !MissionManager.instance.rpgTalk.isPlaying)
        {
            Cat.instance.speed = 1f;
        }
        else if(secao != enumMission.GATO_CORREDOR) {
            Cat.instance.speed = 0.8f;
        }
        if (MissionManager.instance.invertWorld && secao == enumMission.FRENTE_CRIADO)
        {
           // GameObject.Find("MainCamera").GetComponent<Camera>().orthographicSize = 4;
            EspecificaEnum((int)enumMission.MI_ATIVADO);
        }
        else if(secao == enumMission.MI_ATIVADO && !MissionManager.instance.invertWorld) {
            EspecificaEnum((int)enumMission.MI_DESATIVADO);
        }
        if(secao == enumMission.ARMARIO)
        {
            if(MissionManager.instance.invertWorld == true) {
                mom.gameObject.SetActive(false);
                bird.gameObject.SetActive(true);
            }
            else
            {
                mom.gameObject.SetActive(true);
                bird.gameObject.SetActive(false);
            }


        }

        if (secao == enumMission.MAE_CHEGA /*&& Mãe pegou player */)
        {
            // proxima: VERDADE_MENTIRA
        }
        if (secao == enumMission.VERDADE_MENTIRA && !MissionManager.instance.rpgTalk.isPlaying)
        {
            // fim de missão
        }



    }

	public override void SetCorredor()
	{
        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M4CorridorSceneStart", "M4CorridorSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
       

        //PortaMãe
        GameObject portaMae = GameObject.Find("DoorToMomRoom").gameObject;
        portaMaeDefaultY = portaMae.transform.position.y;
        portaMaeDefaultX = portaMae.transform.position.x;
        float posX = portaMae.GetComponent<SpriteRenderer>().bounds.size.x / 5;
        portaMae.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
        portaMae.GetComponent<Collider2D>().isTrigger = false;
        portaMae.transform.position = new Vector3(portaMae.transform.position.x - posX, portaMaeDefaultY, portaMae.transform.position.z);


        GameObject trigger = MissionManager.instance.AddObject("AreaTrigger", "", new Vector3(6.5f, -0.1f, 1), new Vector3(1, 1, 1));
        trigger.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        trigger.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);

        if (secao == enumMission.INICIO || secao == enumMission.GATO_CORREDOR)
        {
            //Cat.instance.DestroyCat();
            EspecificaEnum((int)enumMission.GATO_CORREDOR);
        }
        if ((secao == enumMission.ARMARIO))
        {
            EspecificaEnum((int)enumMission.GRANDE_BARULHO);
        }




    }

    public override void SetCozinha()
	{
        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M4KitchenSceneStart", "M4KitchenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        // Panela para caso ainda não tenha
        if (!Inventory.HasItemType(Inventory.InventoryItems.TAMPA))
        {
            GameObject panela = GameObject.Find("Panela").gameObject;
            panela.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/panela_tampa");
            panela.GetComponent<ScenePickUpObject>().enabled = true;
        }

        MissionManager.instance.scenerySounds.PlayDrop();

        //tirar quando terminar
        //if (secao == enumMission.GRANDE_BARULHO && MissionManager.instance.mission4QuebraSozinho)
        //{
        // Ração no armário altos
        GameObject armario = GameObject.Find("Armario1").gameObject;
        armario.tag = "ScenePickUpObject";
        SceneObject sceneObject = armario.GetComponent<SceneObject>();
        sceneObject.enabled = false;
        ScenePickUpObject scenePickUpObject = armario.AddComponent<ScenePickUpObject>();
        scenePickUpObject.sprite1 = sceneObject.sprite1;
        scenePickUpObject.sprite2 = sceneObject.sprite2;
        scenePickUpObject.positionSprite = sceneObject.positionSprite;
        scenePickUpObject.scale = sceneObject.scale;
        scenePickUpObject.isUp = sceneObject.isUp;
        scenePickUpObject.item = Inventory.InventoryItems.RACAO;
        //}
    }


    public override void SetJardim()
	{
        MissionManager.instance.scenerySounds.StopSound();
        //MissionManager.instance.rpgTalk.NewTalk ("M4GardenSceneStart", "M4GardenSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        GameObject pedra = MissionManager.instance.AddObject("PickUp", "Sprites/Objects/Inventory/pedra", new Vector3((float)-3.59, (float)-0.45, 0), new Vector3((float)1.2, (float)1.2, 1.3f));

    }

    public override void SetQuartoKid()
	{
        MissionManager.instance.scenerySounds.StopSound();
        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z
        

    }

    public override void SetQuartoMae()
	{
        MissionManager.instance.scenerySounds.StopSound();
        MissionManager.instance.rpgTalk.NewTalk ("M4MomRoomSceneStart", "M4MomRoomSceneEnd");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));
        //GameObject.Find("AreaLightHolder").gameObject.transform.Find("AreaLight").gameObject.SetActive(true); //utilizar AreaLight para cenas de dia, variar Z

        if ((secao == enumMission.MI_ATIVADO || secao == enumMission.MI_DESATIVADO) )
        {
            EspecificaEnum((int)enumMission.ARMARIO);
        }
        if (secao == enumMission.POP_UP){
            // carregar sprite do livro



            // aqui realmente é o melhor lugar para contagem do tempo?
            // if de afinadade passaro e gato pathCat >= pathBird
                // contagem de tempo e chama miado do gato 
            //else
                // contagem de tempo, sem miado
                // muda sessão e chama mãe

            //proxima : MAE_CHEGA
        }

    }


    public override void SetSala()
	{
        //MissionManager.instance.rpgTalk.NewTalk ("M4LivingRoomSceneStart", "M4LivingroomSceneEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "AddCountLivingroomDialog");

        GameObject mainLight = GameObject.Find("MainLight").gameObject; // Variar X (-50 - claro / 50 - escuro) - valor original: 0-100 (-50)
        mainLight.transform.Rotate(new Vector3(50, mainLight.transform.rotation.y, mainLight.transform.rotation.z));

        GameObject novelo = MissionManager.instance.AddObject("PickUp", "Sprites/Objects/Inventory/novelo", new Vector3(7f, 1f, 0), new Vector3((float)1.2, (float)1.2, 0.2f));

    }

    public override void EspecificaEnum(int pos)
    {
        secao = (enumMission)pos;
        MissionManager.instance.Print("SECAO: " + secao);
        if (secao == enumMission.NIGHT || secao == enumMission.INICIO)
        {
            MissionManager.instance.scenerySounds.PlayCat(3);
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(-1f, 0.5f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol();
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(1.8f, 0.8f, -0.5f);
            Transform[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;
            cat.GetComponent<Cat>().speed = 0.3f;
            cat.GetComponent<Cat>().destroyEndPath = true;

        }
        else if (secao == enumMission.GATO_CORREDOR)
        {
            MissionManager.instance.scenerySounds.PlayCat(2);
            GameObject cat = MissionManager.instance.AddObject("catFollower", "", new Vector3(10f, -0.2f, -0.5f), new Vector3(0.15f, 0.15f, 1));
            cat.GetComponent<Cat>().Patrol();
            Transform aux = new GameObject().transform;
            aux.position = new Vector3(7f, -0.2f, -0.5f);
            Transform[] catPos = { aux };
            cat.GetComponent<Cat>().targets = catPos;
            cat.GetComponent<Cat>().speed = 1.6f;
            cat.GetComponent<Cat>().stopEndPath = true;
        }
        else if (secao == enumMission.FRENTE_CRIADO)
        {
            MissionManager.instance.rpgTalk.NewTalk("frenteCriadoStart", "frenteCriadoEnd");
        }
        else if (secao == enumMission.MI_ATIVADO)
        {
            // Porta abrindo
            GameObject porta = GameObject.Find("DoorToMomRoom").gameObject;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-opened");
            porta.GetComponent<Collider2D>().isTrigger = true;
            porta.transform.position = new Vector3(portaMaeDefaultX, portaMaeDefaultY, porta.transform.position.z);

        }
        else if (secao == enumMission.MI_DESATIVADO)
        {
            // Porta fechada
            GameObject porta = GameObject.Find("DoorToMomRoom").gameObject;
            float posX = porta.GetComponent<SpriteRenderer>().bounds.size.x / 5;
            porta.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Objects/Scene/door-closed");
            porta.GetComponent<Collider2D>().isTrigger = false;
            porta.transform.position = new Vector3(portaMaeDefaultX, portaMaeDefaultY, porta.transform.position.z);

        }
        else if (secao == enumMission.ARMARIO)
        {
            mom = MissionManager.instance.AddObject("mom", "", new Vector3(2f, -1f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            bird = MissionManager.instance.AddObject("Corvo", "", new Vector3(2f, -1f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            mom.GetComponent<Patroller>().isPatroller = true;
            MissionManager.instance.AddObject("ActionPatroller", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            bird.GetComponent<SpriteRenderer>().color = Color.black;
            bird.GetComponent<Patroller>().isPatroller = true;
            MissionManager.instance.AddObject("ActionPatroller", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        }
        else if (secao == enumMission.GRANDE_BARULHO)
        {
            // if vaso quebrado
                // mudar sprite
                // proxima: QUEBRADO
            MissionManager.instance.rpgTalk.NewTalk("GrandeBarulhoStart", "GrandeBarulhoEnd");
        }
        else if (secao == enumMission.QUEBRADO)
        {
            // Mae patrulha
            // TODO: ADICIONAR MAIS MOVIMENTOS PARA MAE, PARA NAO SER UM CAMINHO TAO OBVIO
            GameObject mom = MissionManager.instance.AddObject("mom", "", new Vector3(0f, 0f, -0.5f), new Vector3(0.3f, 0.3f, 1));
            mom.GetComponent<Patroller>().isPatroller = true;
            Transform target1 = new GameObject().transform, target2 = new GameObject().transform;
            target1.position = new Vector3(6.8f, 0f, -0.5f);
            target2.position = new Vector3(-2.6f, 0f, -0.5f);
            Transform[] momTargets = { target1, target2 };
            mom.GetComponent<Patroller>().targets = momTargets;
            MissionManager.instance.AddObject("ActionPatroller", "", new Vector3(0, 0, 0), new Vector3(1, 1, 1));


        }
        else if (secao == enumMission.POP_UP)
        {
            //if mundo invertido desativado
            // livro vazio
            //else
            // livro com conteúdo
        }
        else if (secao == enumMission.MAE_CHEGA)
        {
            // movimento da mãe em direção ao player que não leva à game over
            // leva à proxima fala
            // verdade ou mentira


        }
        else if (secao == enumMission.VERDADE_MENTIRA)
        {
            MissionManager.instance.rpgTalk.NewTalk("VerdadeOuMentira", "VerdadeOuMentiraEnd");
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
    public override void AreaTriggered(string tag)
    {
        if (tag.Equals("AreaTrigger(Clone)") && secao == enumMission.GATO_CORREDOR)
        {
            EspecificaEnum((int)enumMission.FRENTE_CRIADO);
        }
    }
    public void AddCountKidRoomDialog()
    {
        MissionManager.instance.countKidRoomDialog++;
    }

}