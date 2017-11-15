using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomPatrolMovement : MonoBehaviour {
    public Transform[] targets;
    private int destPoint = 0;
    public float speed;

	// Use this for initialization
	void Start () {
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

        transform.position = Vector3.MoveTowards(transform.position, targets[destPoint].position, step);

        float dist = Vector3.Distance(targets[destPoint].position, transform.position);
        if (dist < 0.4f)
        {
            destPoint = (destPoint + 1) % targets.Length;
        }

    }
}
