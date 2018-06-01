using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowObjects
{
    public class ZoomObject : MonoBehaviour
    {

        public bool colliding = false;
        public float scaleX, scaleY;
        bool showImage = false, opened = false;
        GameObject objectInstance;
        SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

            if (colliding && CrossPlatformInputManager.GetButtonDown("keyInteract") && !GameManager.instance.blocked && !GameManager.instance.paused)
            {
                if (!showImage)
                {
                    if (!GameManager.instance.pausedObject)
                    {
                        showImage = true;
                        GameObject camera = GameObject.Find("MainCamera");
                        Vector3 pos = new Vector3(
                            camera.transform.position.x,
                            camera.transform.position.y,
                            -1);
                        objectInstance = Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
                        objectInstance.transform.localScale = new Vector3(scaleX, scaleY, 1);
                        objectInstance.GetComponent<BoxCollider2D>().enabled = false;
                        objectInstance.layer = LayerMask.NameToLayer("UI");
                        objectInstance.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                        GameManager.instance.pausedObject = showImage;
                    }
                }
                else
                {
                    opened = true;
                    showImage = false;
                    Destroy(objectInstance);
                    GameManager.instance.pausedObject = showImage;
                }
            }
        }

        public bool IsImageShown()
        {
            return showImage;
        }

        public bool ObjectOpened()
        {
            return opened;
        }

    }
}