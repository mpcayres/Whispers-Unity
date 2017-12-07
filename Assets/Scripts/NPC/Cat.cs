using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    public static Cat instance;
    private bool followingPlayer = false;
    private bool isPatroller = false;
    public bool followWhenClose = true;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;

    public float speed;
    GameObject player;
    public Animator animator;
    private bool directionChanged = true;
    private int direction = 0;
    SpriteRenderer spriteRenderer;

    public Transform[] targets;
    private int destPoint = 0;

    private float timeToDestroy = 2;
    
    void Start () {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 0.6f)
            {
                Vector3 aux = player.transform.position;

                if (Mathf.Abs(player.transform.position.x - transform.position.x) >
                    Mathf.Abs(player.transform.position.y - transform.position.y))
                {
                    if (player.transform.position.x > transform.position.x)
                    {
                        aux.x -= 0.6f;
                        animator.SetInteger("direction", 2);
                        animator.SetTrigger("changeState");
                    }
                    else
                    {
                        aux.x += 0.6f;
                        animator.SetInteger("direction", 3);
                        animator.SetTrigger("changeState");
                    }
                }
                else { 
                    if (player.transform.position.y < transform.position.y)
                    {
                        aux.y += 0.6f;
                        animator.SetInteger("direction", 4);
                        animator.SetTrigger("changeState");
                    }
                    else
                    {
                        aux.y -= 0.6f;
                        animator.SetInteger("direction", 5);
                        animator.SetTrigger("changeState");
                    }
                }
                
                transform.position = Vector3.MoveTowards(transform.position, aux, speed * Time.deltaTime);
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

        animator.SetTrigger("changeState");
        ChangeDirection();

        transform.position = Vector3.MoveTowards(transform.position, targets[destPoint].position, step);

        float dist = Vector3.Distance(targets[destPoint].position, transform.position);
        if (dist < 0.4f)
        {
            
            if(destPoint + 1 == targets.Length && destroyEndPath)
            {
                animator.SetTrigger("changeState");
                animator.SetInteger("direction", 0);
                Destroy(gameObject);
            }
            else if (destPoint + 1 == targets.Length && stopEndPath)
            {
                animator.SetTrigger("changeState");
                animator.SetInteger("direction", 0);
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

    public void DestroyCat()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
}
