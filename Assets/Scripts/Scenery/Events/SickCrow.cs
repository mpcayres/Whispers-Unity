using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowScenery
{
    public class SickCrow : MonoBehaviour
    {
        public Animation animationSick { get { return GetComponent<Animation>(); } }

        public bool fly = false;
        public bool colliding = false;

        Player player;

        void Start()
        {
            player = GameManager.instance.gameObject.GetComponent<Player>();
        }

        void Update()
        {
            if (player.playerAction == Player.Actions.ON_OBJECT)
            {
                if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding &&
                !GameManager.instance.paused && !GameManager.instance.blocked &&
                !GameManager.instance.pausedObject)
                {
                    fly = true;
                }
            }
        }
    }
}