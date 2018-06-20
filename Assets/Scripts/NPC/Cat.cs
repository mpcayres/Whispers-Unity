using UnityEngine;
using CrowShadowManager;

namespace CrowShadowNPCs
{
    public class Cat : Follower
    {
        public static Cat instance;

        protected new void Start()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
                animator = GetComponent<Animator>();
                player = GameManager.instance.gameObject;
                spriteRenderer = GetComponent<SpriteRenderer>();
                hasActionPatroller = true;
                fixOrder = -12;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected new void Update()
        {
            base.Update();

        }

        public void DestroyCat()
        {
            Destroy(gameObject);
        }

        protected new void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerCalled(collision);
        }

        protected new void OnTriggerStay2D(Collider2D collision)
        {
            OnTriggerCalled(collision);
        }

        protected new void OnTriggerCalled(Collider2D collision)
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
                }
            }
        }


    }
}