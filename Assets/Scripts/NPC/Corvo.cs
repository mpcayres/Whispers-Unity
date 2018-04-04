using UnityEngine;

public class Corvo : Follower {
    public static Corvo instance;
    public float timeBirdsFollow = 1f;

    protected GameObject birdEmitter;

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            spriteRenderer = GetComponent<SpriteRenderer>();
            birdEmitter = transform.Find("BirdEmitterCollider").gameObject;

            GameObject action = MissionManager.instance.AddObject(
                "ActionCorvo", "", new Vector3(transform.position.x, transform.position.y, 0), new Vector3(1*transform.localScale.x/5, 1 * transform.localScale.y / 5, 1));
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 0.6f)
            {

                if (Mathf.Abs(player.transform.position.x - transform.position.x) >
                    Mathf.Abs(player.transform.position.y - transform.position.y))
                {
                    if (player.transform.position.x > transform.position.x)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction = 1;
                    }
                }
                else
                {
                    if (player.transform.position.y > transform.position.y)
                    {
                        direction = 2;
                    }
                    else
                    {
                        direction = 3;
                    }
                }
                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (player.transform.position.y < transform.position.y)
                {
                    direction = 4;
                }
                else
                {
                    direction = 5;
                }
            }

            ChangeDirectionAnimation();

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

    }

    public void LookAtPlayer()
    {
        if (birdEmitter != null && player != null)
        {
            birdEmitter.transform.LookAt(player.transform);
        }
    }

    public void DestroyRaven()
    {
        Destroy(gameObject);
        if (ActionCorvo.instance != null) ActionCorvo.instance.DestroyAction(); 
    }
}
