using UnityEngine;

public class Corvo : MonoBehaviour {
    public static Corvo instance;
    public bool followingPlayer = false;
    public bool isPatroller = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public float speed;
    public float timeBirdsFollow = 1f;

    public Transform[] targets;
    public Animator animator;
    GameObject player;
    SpriteRenderer spriteRenderer;
    GameObject birdEmitter;
    private bool directionChanged = true;
    private int direction = 0;
    private int destPoint = 0;

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

            GameObject action = MissionManager.instance.AddObject("ActionCorvo", "", new Vector3(0f, 0f, 0), new Vector3(1, 1, 1));
            var main = birdEmitter.GetComponent<ParticleSystem>().main;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        
        print("TIME " + birdEmitter.GetComponent<ParticleSystem>().time);
        if (birdEmitter.activeSelf && birdEmitter.GetComponent<ParticleSystem>().time <= timeBirdsFollow) {
            birdEmitter.transform.LookAt(player.transform);
        }

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
                        animator.SetInteger("direction", 2);
                        animator.SetTrigger("changeState");
                    }
                    else
                    {
                        animator.SetInteger("direction", 3);
                        animator.SetTrigger("changeState");
                    }
                }
                else
                {
                    if (player.transform.position.y < transform.position.y)
                    {
                        animator.SetInteger("direction", 4);
                        animator.SetTrigger("changeState");
                    }
                    else
                    {
                        animator.SetInteger("direction", 5);
                        animator.SetTrigger("changeState");
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (player.transform.position.y > transform.position.y)
                {
                    animator.SetInteger("direction", 1);
                    animator.SetTrigger("changeState");
                }
                else
                {
                    animator.SetInteger("direction", 0);
                    animator.SetTrigger("changeState");
                }
            }
        }

        else if (isPatroller)
        {
            GotoNextPoint();
        }

    }

    void GotoNextPoint()
    {
        float step = speed * Time.deltaTime;

        if (targets.Length == 0)
            return;

        ChangeDirection();

        transform.position = Vector3.MoveTowards(transform.position, targets[destPoint].position, step);

        float dist = Vector3.Distance(targets[destPoint].position, transform.position);
        if (dist < 0.4f)
        {
            animator.SetTrigger("changeState");
            if (destPoint + 1 == targets.Length && destroyEndPath)
            {
                Destroy(gameObject);
            }
            else if (destPoint + 1 == targets.Length && stopEndPath)
            {
                Stop();
                // Mudar animacao, ficar parado
            }
            else
            {
                destPoint = (destPoint + 1) % targets.Length;
            }

        }

    }

    void ChangeDirection()
    {
        if (Mathf.Abs(targets[destPoint].position.y - transform.position.y) <
            Mathf.Abs(targets[destPoint].position.x - transform.position.x))
        {
            if (targets[destPoint].position.x > transform.position.x)
                animator.SetInteger("direction", 2);
            else
                animator.SetInteger("direction", 3);
        }
        else
        {
            if (targets[destPoint].position.y > transform.position.y)
                animator.SetInteger("direction", 4);
            else
                animator.SetInteger("direction", 5);
        }
    }

    public void ChangePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public int GetDirection()
    {
        return direction;
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

    public void DestroyRaven()
    {
        Destroy(gameObject);
        if (ActionCorvo.instance != null) ActionCorvo.instance.DestroyAction(); 
    }
}
