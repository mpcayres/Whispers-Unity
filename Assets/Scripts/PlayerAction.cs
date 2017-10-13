﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public GameObject target;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("PlayerAction");
        if (collision.gameObject.tag == "SceneObject")
        {
            collision.gameObject.GetComponent<SceneObject>().colliding = true;
        }

        if (collision.gameObject.tag == "MovingObject")
        {
            collision.gameObject.GetComponent<MovingObject>().colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SceneObject")
        {
            collision.gameObject.GetComponent<SceneObject>().colliding = false;
        }

        if (collision.gameObject.tag == "MovingObject")
        {
            collision.gameObject.GetComponent<MovingObject>().colliding = false;
        }
    }
    
}
