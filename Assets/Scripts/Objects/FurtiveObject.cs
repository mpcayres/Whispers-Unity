using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowObjects
{
    public class FurtiveObject : MonoBehaviour
    {
        public bool colliding = false;
        public float timeMax = 6f;

        GameObject player;
        SpriteRenderer spriteRenderer;
        Renderer playerRenderer;
        Rigidbody2D playerRB;
        
        float timeLeft;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            player = GameManager.instance.gameObject;
            playerRenderer = player.GetComponent<Renderer>();
            playerRB = player.GetComponent<Rigidbody2D>();
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

                if (playerRenderer.enabled && CrossPlatformInputManager.GetButtonDown("keyInteract")) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
                {
                    playerRenderer.enabled = false;
                    playerRB.bodyType = RigidbodyType2D.Kinematic;
                    player.layer = LayerMask.NameToLayer("PlayerHidden");
                    GameManager.instance.pausedObject = true;
                    player.GetComponent<Player>().hidden = true;
                    timeLeft = timeMax;
                }
                else if (!playerRenderer.enabled && (CrossPlatformInputManager.GetButtonDown("keyInteract") || timeLeft <= 0))
                {
                    playerRenderer.enabled = true;
                    playerRB.bodyType = RigidbodyType2D.Dynamic;
                    player.layer = LayerMask.NameToLayer("Player");
                    GameManager.instance.pausedObject = false;
                    player.GetComponent<Player>().hidden = false;
                    timeLeft = 0;
                }
            }

        }
    }
}