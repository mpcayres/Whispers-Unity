using UnityEngine;

public class Cat : Follower {
    public static Cat instance;
    
    protected new void Start () {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            spriteRenderer = GetComponent<SpriteRenderer>();
            fixOrder = -12;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	protected new void Update () {
        base.Update();
    }

    public void DestroyCat()
    {
        Destroy(gameObject);
    }
}
