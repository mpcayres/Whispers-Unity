using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    private bool followingPlayer = false;
    public float speed;
    GameObject player;
    public Animator animator;
    private bool directionChanged = true;
    private int direction = 0;

    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
   
    }
	
	// Update is called once per frame
	void Update () {
        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 0.6f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

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
                else { 
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
		
	}

    public void FollowPlayer()
    {
        followingPlayer = true;
    }
}
