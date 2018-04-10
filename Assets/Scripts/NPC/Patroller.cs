using UnityEngine;

public class Patroller : MonoBehaviour {
    public float speed;
    public Animator animator;
    public bool isPatroller = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public Transform[] targets;

    public bool hasActionPatroller = false;
    public float offsetActionPatroller = 0f;

    protected SpriteRenderer spriteRenderer;

    protected int direction = 4, oldDirection = 4;
    protected int destPoint = 0;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (hasActionPatroller)
        {
            CircleCollider2D[] col = GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D i in col)
            {
                i.enabled = true;
            }
        }
    }

    protected void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (isPatroller)
        {
            GotoNextPoint();
        }
        SetActionPatrollerDirection();
    }

    protected void GotoNextPoint()
    {
        if (targets.Length == 0 || transform == null) return;

        float step = speed * Time.deltaTime;

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

    protected void ChangeDirection()
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

    protected void SetActionPatrollerDirection()
    {
        if (hasActionPatroller && offsetActionPatroller != 0)
        {
            switch (direction)
            {
                case 0:
                    GetComponent<CircleCollider2D>().offset = new Vector2(offsetActionPatroller, 0f);
                    break;
                case 1:
                    GetComponent<CircleCollider2D>().offset = new Vector2(-offsetActionPatroller, 0f);
                    break;
                case 2:
                    GetComponent<CircleCollider2D>().offset = new Vector2(0f, offsetActionPatroller);
                    break;
                case 3:
                    GetComponent<CircleCollider2D>().offset = new Vector2(0f, -offsetActionPatroller);
                    break;
                default:
                    GetComponent<CircleCollider2D>().offset = new Vector2(0f, 0f);
                    break;
            }
        }
    }

    public int GetDirection()
    {
        return direction;
    }

    public void Stop()
    {
        isPatroller = false;
        direction = 4;
        ChangeDirectionAnimation();
    }

    protected void ChangeDirectionAnimation()
    {
        if (oldDirection != direction)
        {
            if(animator == null) animator = GetComponent<Animator>();
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

    //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasActionPatroller)
        {
            print("ActionPatroller: " + collision.tag);
            if (collision.gameObject.tag.Equals("Player"))
            {
                MissionManager.instance.GameOver();
            }
        }
    }

}