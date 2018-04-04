using UnityEngine;

public class CorvBabies : Follower {
    public static CorvBabies instance;
    public float timeBirdsFollow = 1f;

    protected GameObject birdEmitter;

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            birdEmitter = transform.Find("BirdEmitterCollider").gameObject;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {

        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 0.6f)
            {   
                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
            }

        }
        else if (isPatroller)
        {
            GotoNextPoint();
        }

        if (birdEmitter.activeSelf /*&& birdEmitter.GetComponent<ParticleSystem>().time <= timeBirdsFollow*/)
        {
            //birdEmitter.transform.LookAt(player.transform);
            //da forma comentada ele só segue o player por um período de tempo
            //porém assim acontecia de pássaros já existentes do nada mudarem a posição
            Vector3 dir = player.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            birdEmitter.transform.rotation = Quaternion.Lerp(birdEmitter.transform.rotation, rot, timeBirdsFollow/2 * Time.deltaTime);
        }
        else
        {
            birdEmitter.transform.LookAt(player.transform);
        }

    }

    public void LookAtPlayer()
    {
        if (birdEmitter != null && player != null)
        {
            birdEmitter.transform.LookAt(player.transform);
        }
    }

    public void DestroyCorvBabies()
    {
        Destroy(gameObject);
    }
}
