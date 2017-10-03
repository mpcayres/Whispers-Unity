using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public GameObject player;
    public bool colliding = false;
    Player script;
    SpriteRenderer spriteRenderer;
    float distanceWantedX = 0.6f;
    float distanceWantedY = 0.6f;
    enum EastWest { NONE, EAST, WEST };
    enum NorthSouth { NONE, NORTH, SOUTH };
    EastWest blockEW = EastWest.NONE;
    NorthSouth blockNS = NorthSouth.NONE;

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

            if ((script.direction == 0 && relativePoint.x < 0.0 && blockEW != EastWest.EAST)
                || (script.direction == 1 && relativePoint.x > 0.0 && blockEW != EastWest.WEST)
                || (script.direction == 0 && relativePoint.x < 0.0 && script.wantedDirection == 1)
                || (script.direction == 1 && relativePoint.x > 0.0 && script.wantedDirection == 0)
                )
        {
                Vector3 diff = transform.position - player.transform.position;
                diff.y = 0; // ignore Y
                transform.position = player.transform.position + diff.normalized * distanceWantedX;
            }
            else if ((script.direction == 2 && relativePoint.y < 0.0 && blockNS != NorthSouth.NORTH)
                || (script.direction == 3 && relativePoint.y > 0.0 && blockNS != NorthSouth.SOUTH)
                || (script.direction == 2 && relativePoint.y < 0.0 && script.wantedDirection == 3)
                || (script.direction == 3 && relativePoint.y > 0.0 && script.wantedDirection == 2))
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
            //print(collision.gameObject.tag);
            if (script.direction == 0)
            {
                blockEW = EastWest.EAST;
            }
            else if (script.direction == 1)
            {
                blockEW = EastWest.WEST;
            }
            else if (script.direction == 2)
            {
                blockNS = NorthSouth.NORTH;
            }
            else if (script.direction == 3)
            {
                blockNS = NorthSouth.SOUTH;
            }

            print(blockEW);
            print(blockNS);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            //print(collision.gameObject.tag);
            blockEW = EastWest.NONE;
            blockNS = NorthSouth.NONE;
        }
    }

}
