using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomPatrolMovement : MonoBehaviour {
    public Transform[] targets;
    private int destPoint = 0;
    public float speed;
    public Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        animator.SetTrigger("changeDirection");
        GotoNextPoint();
	}
	
	// Update is called once per frame
	void Update () {
        GotoNextPoint();
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
        if(Mathf.Abs(targets[destPoint].position.y - transform.position.y) < 0.3){
            if (targets[destPoint].position.x > transform.position.x)
                animator.SetInteger("direction", 0);
            else
                animator.SetInteger("direction", 1);
        }else if(Mathf.Abs(targets[destPoint].position.x - transform.position.x) < 0.3)
        {
            if (targets[destPoint].position.y > transform.position.y)
                animator.SetInteger("direction", 2);
            else
                animator.SetInteger("direction", 3);
        }
    }
}
