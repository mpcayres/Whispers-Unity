using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {
    float health = 3;
    public bool isEvil = false, canKill = false;
    public int number;
  
    void Update () {
        if(health <= 0)
        {
            if (gameObject.scene.name.Equals("Jardim") && !isEvil)
            {
                SpiritManager.DestroyGoodSpirit(number);
                Destroy(gameObject);
            }
            else if (gameObject.scene.name.Equals("Jardim") && isEvil)
            {
                SpiritManager.DestroyEvilSpirit(number);
                Destroy(gameObject);
            }else if (canKill && gameObject.scene.name.Equals("Sala"))
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("TRIGGER: " + collision.gameObject.name);
        if (gameObject.activeSelf && collision.gameObject.tag.Equals("Flashlight"))
        {
            health -= Time.deltaTime;
        }
        if(canKill && collision.gameObject.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
