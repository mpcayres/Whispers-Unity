using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {
    float health = 3;
    bool assimilatedHealth = false;
    public static float newHealth = 0;
    public bool isEvil = false, canKill = false;
    public int number;
    public static int maxEvilKilled = 4;

    public AudioClip sound;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    void Start() {

        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }

    void Update () {
        if (newHealth != 0 && assimilatedHealth == false)
        {
            health = newHealth;
            assimilatedHealth = true;
        }

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
            }else if (gameObject.scene.name.Equals("Sala"))
            {
                if (isEvil)
                {
                    maxEvilKilled--;
                    if(maxEvilKilled < 0)
                    {
                        //maxEvilKilled = 5;
                        MissionManager.instance.GameOver();
                    }
                }
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
            if (!source.isPlaying)
                source.PlayOneShot(sound);
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
