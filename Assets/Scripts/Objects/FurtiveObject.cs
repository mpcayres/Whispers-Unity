using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowObjects
{
    public class FurtiveObject : MonoBehaviour
    {
        GameObject player;
        public bool colliding = false;
        public float timeMax = 6f;
        SpriteRenderer spriteRenderer;
        float timeLeft;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

            if (colliding && !GameManager.instance.paused && !GameManager.instance.blocked)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }

                if (player.GetComponent<Renderer>().enabled && CrossPlatformInputManager.GetButtonDown("keyInteract")) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
                {
                    player.GetComponent<Renderer>().enabled = false;
                    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    player.layer = LayerMask.NameToLayer("PlayerHidden");
                    GameManager.instance.pausedObject = true;
                    player.GetComponent<Player>().hidden = true;
                    timeLeft = timeMax;
                }
                else if (!player.GetComponent<Renderer>().enabled && (CrossPlatformInputManager.GetButtonDown("keyInteract") || timeLeft <= 0))
                {
                    player.GetComponent<Renderer>().enabled = true;
                    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    player.layer = LayerMask.NameToLayer("Player");
                    GameManager.instance.pausedObject = false;
                    player.GetComponent<Player>().hidden = false;
                    timeLeft = 0;
                }
            }

        }
    }
}