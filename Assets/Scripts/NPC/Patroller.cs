﻿using UnityEngine;

public class Patroller : MonoBehaviour {
    public float speed;
    public Animator animator;
    public bool isPatroller = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public Transform[] targets;

    protected SpriteRenderer spriteRenderer;

    protected int direction = 4, oldDirection = 4;
    protected int destPoint = 0;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (isPatroller)
        {
            GotoNextPoint();
        }
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
}