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
            if ((collision.transform.position.y < transform.position.y) && 
                collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals("ObjectsBack"))
            {
                print("Back->Lower");
                collision.gameObject.GetComponent<SceneObject>().ChangeSortingLayer("ObjectsFront");
            }
            else if ((collision.transform.position.y >= transform.position.y) &&
                collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals("ObjectsFront"))
            {
                print("Front->Back");
                collision.gameObject.GetComponent<SceneObject>().ChangeSortingLayer("ObjectsBack");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                collision.gameObject.GetComponent<SceneObject>().ChangeSprite();
            }
        }

        if (collision.gameObject.tag == "MovingObject")
        {
            if ((collision.transform.position.y < transform.position.y) &&
                collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals("ObjectsBack"))
            {
                print("Back->Lower");
                collision.gameObject.GetComponent<MovingObject>().ChangeSortingLayer("ObjectsFront");
            }
            else if ((collision.transform.position.y >= transform.position.y) &&
                collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals("ObjectsFront"))
            {
                print("Front->Back");
                collision.gameObject.GetComponent<MovingObject>().ChangeSortingLayer("ObjectsBack");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                collision.gameObject.GetComponent<MovingObject>().SetOffset();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                collision.gameObject.GetComponent<MovingObject>().Move();
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                collision.gameObject.GetComponent<MovingObject>().EndMove();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingObject")
        {
            collision.gameObject.GetComponent<MovingObject>().EndMove();
        }
    }
    
}
