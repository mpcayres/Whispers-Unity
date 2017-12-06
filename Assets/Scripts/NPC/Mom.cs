using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom: MonoBehaviour {
    public Transform[] targets;
    private int destPoint = 0;
    public float speed;
    SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool isPatroller = false;
    int direction = 0;
    
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetTrigger("changeDirection");
	}
	
	void Update () {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (isPatroller)
        {
            GotoNextPoint();
        }
        else
        {
            animator.SetInteger("direction", 5);
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
            animator.SetTrigger("changeDirection");
            destPoint = (destPoint + 1) % targets.Length;
        }

    }

    void ChangeDirection()
    {
        if(Mathf.Abs(targets[destPoint].position.y - transform.position.y) <
            Mathf.Abs(targets[destPoint].position.x - transform.position.x))
        {
            if (targets[destPoint].position.x > transform.position.x)
                direction = 0;
            else
                direction = 1;
        } else {
            if (targets[destPoint].position.y > transform.position.y)
                direction = 2;
            else
                direction = 3;
        }
        animator.SetInteger("direction", direction);
    }

    public int GetDirection()
    {
        return direction;
    }
}
