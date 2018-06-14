using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowObjects
{
    public class Lamp : MonoBehaviour
    {
        public string prefName = ""; // Padrão: identificador do objeto (L) + _ + nome da cena + _ + identificador
        public bool colliding = false;

        SpriteRenderer spriteRenderer;
        Light lightComponent;
        CircleCollider2D circleCollider;

        private bool change = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            lightComponent = GetComponent<Light>();
            circleCollider = GetComponent<CircleCollider2D>();
        }

        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

            if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding &&
                !GameManager.instance.paused && !GameManager.instance.blocked && !GameManager.instance.pausedObject)
            {
               lightComponent.enabled = !lightComponent.enabled;
                circleCollider.enabled = lightComponent.enabled;
                change = true;
            }
        }

        public bool Changed()
        {
            return change;
        }
    }
}