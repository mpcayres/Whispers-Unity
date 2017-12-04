using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    GameObject target;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
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

        if (collision.gameObject.tag == "FurtiveObject")
        {
            collision.gameObject.GetComponent<FurtiveObject>().colliding = true;
        }

        if (collision.gameObject.tag == "PickUpObject")
        {
            collision.gameObject.GetComponent<PickUpObject>().colliding = true;
        }

        if (collision.gameObject.tag == "ZoomObject")
        {
            collision.gameObject.GetComponent<ZoomObject>().colliding = true;
        }

        if (collision.gameObject.tag == "ScenePickUpObject")
        {
            collision.gameObject.GetComponent<ScenePickUpObject>().colliding = true;
        }

        if (collision.gameObject.tag == "WindowTrigger")
        {
            collision.gameObject.GetComponent<WindowTrigger>().colliding = true;
            collision.gameObject.GetComponent<WindowTrigger>().ScareTrigger();
        }

        if (collision.gameObject.tag == "Cat")
        {
            print("Gato");
            collision.gameObject.GetComponent<Cat>().FollowPlayer();
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

        if (collision.gameObject.tag == "FurtiveObject")
        {
            collision.gameObject.GetComponent<FurtiveObject>().colliding = false;
        }

        if (collision.gameObject.tag == "PickUpObject")
        {
            collision.gameObject.GetComponent<PickUpObject>().colliding = false;
        }

        if (collision.gameObject.tag == "ZoomObject")
        {
            collision.gameObject.GetComponent<ZoomObject>().colliding = false;
        }

        if(collision.gameObject.tag == "ScenePickUpObject")
        {
            collision.gameObject.GetComponent<ScenePickUpObject>().colliding = false;
        }

        if (collision.gameObject.tag == "WindowTrigger")
        {
            collision.gameObject.GetComponent<WindowTrigger>().colliding = false;
        }
    }
    
}
