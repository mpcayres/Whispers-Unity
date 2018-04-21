using UnityEngine;

public class Follower : Patroller {
    public bool followWhenClose = false;
    public bool followingPlayer = false;

    protected int fixOrder = 0;
    protected float distFollow = 0.6f;
    protected bool moveTowards = false;
    protected GameObject player;
    
    protected new void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	protected new void Update () {

        spriteRenderer.sortingOrder = fixOrder + Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > distFollow)
            {

                if (Mathf.Abs(player.transform.position.x - transform.position.x) >
                    Mathf.Abs(player.transform.position.y - transform.position.y))
                {
                    if (player.transform.position.x > transform.position.x)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction = 1;
                    }
                }
                else
                {
                    if (player.transform.position.y > transform.position.y)
                    {
                        direction = 2;
                    }
                    else
                    {
                        direction = 3;
                    }
                }

                if (moveTowards)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                
            }
            else
            {
                if (player.transform.position.y < transform.position.y)
                {
                    direction = 4;
                }
                else
                {
                    direction = 5;
                }
            }

            ChangeDirectionAnimation();

        }
        else if (isPatroller)
        {
            GotoNextPoint();
        }
        SetActionPatrollerDirection();
    }

    public void ChangePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void FollowPlayer()
    {
        isPatroller = false;
        followingPlayer = true;
    }

    public bool IsFollowing()
    {
        return followingPlayer;
    }

    public void Patrol()
    {
        followingPlayer = false;
        isPatroller = true;
    }

    public bool IsPatroller()
    {
        return isPatroller;
    }

    public new void Stop()
    {
        followingPlayer = false;
        base.Stop();
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerCalled(collision);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerCalled(collision);
    }

    protected void OnTriggerCalled(Collider2D collision)
    {
        if (hasActionPatroller)
        {
            print("ActionFollower: " + collision.tag);
            if (collision.gameObject.tag.Equals("PlayerAction"))
            {
                if (followWhenClose && !followingPlayer)
                {
                    FollowPlayer();
                }
                else if (!followWhenClose)
                {
                    MissionManager.instance.GameOver();
                }
            }
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("FixedObject") || coll.gameObject.tag.Equals("SceneObject")
            || coll.gameObject.tag.Equals("MovingObject") || coll.gameObject.tag.Equals("Player"))
        {
            var thisX = coll.collider.bounds.center.x;
            var thisY = coll.collider.bounds.center.y;
            var otherX = coll.otherCollider.bounds.center.x;
            var otherY = coll.otherCollider.bounds.center.y;

            float thisObjX = this.gameObject.transform.position.x;
            float thisObjY = this.gameObject.transform.position.y;
            float thisObjZ = this.gameObject.transform.position.z;


            if (thisX < otherX && thisY > otherY){
                // subir um pouco
                // um pouco pra direita
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3 ( thisObjX + 1, thisObjY-1, thisObjZ - 1),speed * Time.deltaTime);


            }
            if (thisX < otherX && thisY < otherY)
            {
                // descer um pouco
                // um pouco pra direita
            }
            if (thisX > otherX && thisY > otherY)
            {
                // subir um pouco
                // um pouco pra esquerda
            }
            if (thisX > otherX && thisY < otherY)
            {
                // descer um pouco
                // um pouco pra esquerda
            }
        }
    }

}
