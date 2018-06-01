using UnityEngine;

namespace CrowShadowPlayer
{
    public class HUDPlayer : MonoBehaviour
    {

        public static HUDPlayer instance;

        public void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}