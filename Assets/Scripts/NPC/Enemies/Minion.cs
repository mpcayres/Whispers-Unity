using UnityEngine;

public class Minion : Follower {

    public int healthLight = 200; //decrementa 1 por colisão
    public int healthMelee = 300;
    public int decrementFaca = 30, decrementBastao = 25, decrementPedra = 20;
    public float addPath = 0.5f;

    float timeLeftAttack = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (timeLeftAttack > 0)
        {
            timeLeftAttack -= Time.deltaTime;
        }

        if (healthLight <= 0)
        {
            MissionManager.instance.pathCat += addPath;
            Destroy(gameObject);
            // animação + som
        }
        else if (healthMelee <= 0)
        {
            MissionManager.instance.pathBird += addPath;
            Destroy(gameObject);
            // animação + som
        }

        if (followingPlayer)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 0.6f)
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
                    if (player.transform.position.y < transform.position.y)
                    {
                        direction = 2;
                    }
                    else
                    {
                        direction = 3;
                    }
                }

                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Minion: " + collision.tag);
        if (collision.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("Minion: " + collision.tag);
        if (collision.tag.Equals("Flashlight") && Flashlight.GetState())
        {
            healthLight--;
        }
        else if (collision.tag.Equals("Faca") && collision.GetComponent<AttackObject>().attacking && timeLeftAttack <= 0)
        {
            timeLeftAttack = AttackObject.timeAttack;
            healthMelee -= decrementFaca;
        }
        else if (collision.tag.Equals("Bastao") && collision.GetComponent<AttackObject>().attacking && timeLeftAttack <= 0)
        {
            timeLeftAttack = AttackObject.timeAttack;
            healthMelee -= decrementBastao;
        }
    }
}