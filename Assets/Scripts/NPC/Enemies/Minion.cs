using UnityEngine;

public class Minion : Follower {

    public int healthLight = 200; //decrementa 1 por colisão
    public int healthMelee = 300;
    public int decrementFaca = 30, decrementBastao = 25, decrementPedra = 20;
    public float addPath = 0.5f;

    float timeLeftAttack = 0;

    protected new void Start()
    {
        base.Start();
        distFollow = 0.1f;
        moveTowards = true;
    }

    protected new void Update()
    {
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
        
        base.Update();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("Minion: " + collision.tag);
        if ((collision.tag.Equals("Flashlight") && Flashlight.GetState()) || collision.tag.Equals("Lamp"))
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
        else if (collision.tag.Equals("Pedra") && collision.GetComponent<FarAttackObject>().attacking)
        {
            collision.GetComponent<FarAttackObject>().hitSuccess = true;
            healthMelee -= decrementPedra;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
             MissionManager.instance.GameOver();
        }
    }
}