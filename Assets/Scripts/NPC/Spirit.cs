using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {
    float health = 3;
    public bool isEvil = false;
    public int number;
  
    void Update () {
    if(health <= 0)
        {
            if (this.gameObject.scene.name.Equals("Jardim") && !isEvil)
            {
                SpiritManager.DestroyGoodSpirit(number);
                Destroy(this.gameObject);
            }
            else if (this.gameObject.scene.name.Equals("Jardim") && isEvil)
            {
                SpiritManager.DestroyEvilSpirit(number);
                Destroy(this.gameObject);
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
