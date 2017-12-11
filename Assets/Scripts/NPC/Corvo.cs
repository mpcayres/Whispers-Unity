using UnityEngine;

public class Corvo : MonoBehaviour {
    public static Corvo instance;
    public bool followingPlayer = false;
    public bool isPatroller = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public float speed;
    public float timeBirdsFollow = 1f;

    private int direction = 4, oldDirection = 4;

    GameObject player;
    SpriteRenderer spriteRenderer;
    GameObject birdEmitter;
    public Animator animator;

    public Transform[] targets;
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
                        direction = 0;
                    }
                    else
                    {
                        direction = 1;
                    }
                }
                else
                {
                    if (player.transform.position.y < transform.position.y)
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
                ChangeDirection();
            }

        }
        else
        {
            ChangeDirection();
        }

    }

    void ChangeDirection()
    {
        if (Mathf.Abs(targets[destPoint].position.y - transform.position.y) <
            Mathf.Abs(targets[destPoint].position.x - transform.position.x))
        {
            if (targets[destPoint].position.x > transform.position.x)
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
            if (targets[destPoint].position.y > transform.position.y)
            {
                direction = 2;
            }
            else
            {
                direction = 3;
            }
        }
        ChangeDirectionAnimation();
    }

    public void ChangePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
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
        direction = 4;
        ChangeDirectionAnimation();
    }

    public void DestroyRaven()
    {
        Destroy(gameObject);
        if (ActionCorvo.instance != null) ActionCorvo.instance.DestroyAction(); 
    }

    void ChangeDirectionAnimation()
    {
        if (oldDirection != direction)
        {
            animator.SetInteger("direction", direction);
            animator.SetTrigger("changeDirection");
            oldDirection = direction;
        }
    }
}
