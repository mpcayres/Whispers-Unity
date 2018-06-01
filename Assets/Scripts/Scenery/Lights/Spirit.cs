using UnityEngine;
using CrowShadowManager;

public class Spirit : MonoBehaviour {
    public SpiritManager spiritManager;
    public float health = 3;
    public bool isEvil = false, canKill = false;
    public bool isAlive = true;
    public int number;

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    void Start() {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }

    void Update () {
        if (health <= 0)
        {
            if (isEvil)
            {
                spiritManager.DestroyEvilSpirit();
            }
            else if (canKill)
            {
                spiritManager.DestroyKillerSpirit();
            }
            else
            {
                spiritManager.DestroyGoodSpirit();
            }
            isAlive = false;
            gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canKill && collision.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("TRIGGER_SPIRIT: " + collision.gameObject.name);
        if (gameObject.activeSelf && collision.gameObject.tag.Equals("Flashlight"))
        {
            health -= Time.deltaTime;
            if (!source.isPlaying)
                source.PlayOneShot(sound);
        }
    }
}
