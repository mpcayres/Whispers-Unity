using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    public static Cat instance;
    public bool followWhenClose = true;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public float speed;

    private bool followingPlayer = false;
    private bool isPatroller = false;
    private int direction = 5, oldDirection = 5;

    GameObject player;
    public Animator animator;
    SpriteRenderer spriteRenderer;

    public Transform[] targets;
    private int destPoint = 0;
    
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
	
	void Update () {

        spriteRenderer.sortingOrder = -12 + Mathf.RoundToInt(transform.position.y * 100f) * -1;

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
            
            if(destPoint + 1 == targets.Length && destroyEndPath)
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

    public bool IsFollowing()
    {
        return followingPlayer;
    }

    public void Patrol()
    {
        followingPlayer = false;
        isPatroller = true;
    }

    public bool IsPatroller()
    {
        return isPatroller;
    }

    public void Stop()
    {
        followingPlayer = false;
        isPatroller = false;
        direction = 5;
        ChangeDirectionAnimation();
    }

    public void DestroyCat()
    {
        Destroy(gameObject);
    }

    void ChangeDirectionAnimation()
    {
        if (oldDirection != direction)
        {
            if (animator == null) animator = GetComponent<Animator>();
            animator.SetInteger("direction", direction);
            animator.SetTrigger("changeDirection");
            oldDirection = direction;
        }
    }

    public void ChangeDirectionAnimation(int d)
    {
        direction = d;
        ChangeDirectionAnimation();
    }
}
