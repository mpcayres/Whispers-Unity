using UnityEngine;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowScenery
{
    public class Gloom : MonoBehaviour
    {
        private int maxRoar = 5;

        private void Start()
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }

        void Update()
        {
            if (Flashlight.GetState() && !gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
                maxRoar--;
                if (maxRoar < 0)
                {
                    GameManager.instance.GameOver();
                }
            }
        }
    }
}