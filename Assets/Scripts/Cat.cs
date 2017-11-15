using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    private bool followingPlayer = false;
    public float speed;
    GameObject player;
    public Animator animator;

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
            if (dist > 0.6f){
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

           

        }
		
	}

    public void FollowPlayer()
    {
        followingPlayer = true;
    }
}
