using UnityEngine;

public class Minion : Follower {

    public int healthLight = 200; //decrementa 1 por colisão
    public int healthMelee = 300;
    public int decrementFaca = 30, decrementBastao = 25, decrementPedra = 20;
    public float addPath = 0.5f; // quanto vai ser adicionado ao somatório das escolhas
    public float timeMaxPower = 3f; // tempo máximo que pode ficar colidindo com o minion para não ativar próximo poder
    public float timeMaxChangeVelocity = 6f, factorDivideSpeed = 1.8f; // tempo máximo com velocidade menor e fator para dividi-la
    public float timeInvertControls = 6f; // tempo adicional para ficar com o controle invertido

    float timeLeftAttack = 0, timePower = 0, timeChangeVelocity = 0;
    int power = 0; // 1 - diminui velocidade, 2 - inverte controles, 3 - morre
    bool onCollision = false, changeVelocity = false;

    protected new void Start()
    {
        base.Start();
        distFollow = 0.1f;
        moveTowards = true;
    }

    protected new void Update()
    {
        if (onCollision)
        {
            if (timePower > 0)
            {
                timePower -= Time.deltaTime;
            }
            else
            {
                timePower = timeMaxPower;
                ActivatePower();
            }
        }

        if (changeVelocity)
        {
            if (timeChangeVelocity > 0)
            {
                timeChangeVelocity -= Time.deltaTime;
            }
            else
            {
                player.GetComponent<Player>().movespeed = player.GetComponent<Player>().movespeed * factorDivideSpeed;
                changeVelocity = false;
            }
        }

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

    private new void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerCalled(collision);
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
        else if (collision.tag.Equals("Papel") && collision.GetComponent<FarAttackMiniGameObject>().attacking)
        {
            collision.GetComponent<FarAttackMiniGameObject>().hitSuccess = true;
            if (collision.GetComponent<FarAttackMiniGameObject>().achievedGoal)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ActivatePower()
    {
        print("ACTIVATE " + power);
        switch (power)
        {
            case 0:
                timeChangeVelocity = timeMaxChangeVelocity;
                changeVelocity = true;
                player.GetComponent<Player>().movespeed = player.GetComponent<Player>().movespeed / factorDivideSpeed;
                power++;
                break;
            case 1:
                player.GetComponent<Player>().invertControlsTime += timeInvertControls;
                power++;
                break;
            case 2:
                power = 0;
                MissionManager.instance.GameOver();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (timePower <= 0)
            {
                timePower = timeMaxPower;
            }
            onCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            onCollision = false;
        }
    }
}