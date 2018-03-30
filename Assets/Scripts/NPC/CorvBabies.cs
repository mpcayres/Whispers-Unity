using UnityEngine;

public class CorvBabies : MonoBehaviour {
    public static CorvBabies instance;
    public bool followingPlayer = false;
    public bool isPatroller = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public float speed;
    public float timeBirdsFollow = 1f;

    GameObject player;
    GameObject birdEmitter;

    public Transform[] targets;
    private int destPoint = 0;

    void Start()
    {
        if (instance == null)
        {
            print("INST");
            DontDestroyOnLoad(gameObject);
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            birdEmitter = transform.Find("BirdEmitterCollider").gameObject;
            //var main = birdEmitter.GetComponent<ParticleSystem>().main;
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

    void GotoNextPoint()
    {
        float step = speed * Time.deltaTime;

        if (targets.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, targets[destPoint].position, step);

        float dist = Vector3.Distance(targets[destPoint].position, transform.position);
        if (dist < 0.4f)
        {
            if (destPoint + 1 == targets.Length && destroyEndPath)
            {
                Destroy(gameObject);
            }
            else if (destPoint + 1 == targets.Length && stopEndPath)
            {
                Stop();
            }
            else
            {
                destPoint = (destPoint + 1) % targets.Length;
            }

        }

    }

    public void ChangePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void LookAtPlayer()
    {
        if (birdEmitter != null && player != null)
        {
            birdEmitter.transform.LookAt(player.transform);
        }
    }

    public void FollowPlayer()
    {
        isPatroller = false;
        followingPlayer = true;
    }

    public void Patrol()
    {
        followingPlayer = false;
        isPatroller = true;
    }

    public void Stop()
    {
        followingPlayer = false;
        isPatroller = false;
    }

    public void DestroyCorvBabies()
    {
        Destroy(gameObject);
    }
}
