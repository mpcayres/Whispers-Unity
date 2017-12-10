using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSpirit : MonoBehaviour {
    float health = 3;
    public int number;
  
    void Update () {
    if(health <= 0)
        {
            if (this.gameObject.scene.name.Equals("Jardim"))
            {
                SpiritManager.goodSpiritGardenKilled++;
            }
        }
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.gameObject.activeSelf && collision.gameObject.tag.Equals("Flashlight")) 
        {
            health -= Time.deltaTime;
        }

    }
}
