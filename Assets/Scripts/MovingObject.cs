using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public GameObject player;
    public bool colliding = false;
    Player script;
    SpriteRenderer spriteRenderer;
    bool blockedMove = false;
    float distanceWantedX = 0.6f;
    float distanceWantedY = 0.6f;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        script = player.GetComponent<Player>();
    }
	
	void Update ()
    {
        if (colliding)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                SetOffset();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                Move();
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                EndMove();
            }
        }
    }

    public void SetOffset()
    {
        print("INITMOVE");
        script.playerState = Player.Actions.MOVING_OBJECT;
        script.animator.SetTrigger("movingObject");
    }

    public void Move()
    {
        print("MOVE");
        //if (!blockedMove)
        //{
            var relativePoint = transform.InverseTransformPoint(player.transform.position);
            //para ver se esta na esquerda ou direta, em cima ou baixo

            if ((script.direction == 0 && relativePoint.x < 0.0)
                || (script.direction == 1 && relativePoint.x > 0.0))
            {
                Vector3 diff = transform.position - player.transform.position;
                diff.y = 0; // ignore Y
                transform.position = player.transform.position + diff.normalized * distanceWantedX;
            }
            else if ((script.direction == 2 && relativePoint.y < 0.0)
                || (script.direction == 3 && relativePoint.y > 0.0))
            {
                Vector3 diff = transform.position - player.transform.position;
                diff.x = 0; // ignore X
                transform.position = player.transform.position + diff.normalized * distanceWantedY;
            }
        //}
    }

    public void EndMove()
    {
        print("ENDMOVE");
        script.playerState = Player.Actions.DEFAULT;
        script.animator.SetTrigger("changeDirection");
    }

    public void ChangeSortingLayer(string newLayer)
    {
        spriteRenderer.sortingLayerName = newLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            print(collision.gameObject.tag);
            blockedMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            print(collision.gameObject.tag);
            blockedMove = false;
        }
    }

}
