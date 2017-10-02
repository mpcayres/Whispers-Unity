using System.Collections;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("PlayerAction");
        if (collision.gameObject.tag == "SceneObject")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                collision.gameObject.GetComponent<SceneObject>().ChangeSprite();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    
}
