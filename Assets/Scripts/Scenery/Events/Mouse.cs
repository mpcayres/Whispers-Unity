using UnityEngine;
using CrowShadowManager;

namespace CrowShadowScenery
{
    public class Mouse : MonoBehaviour
    {
        public string animationName = "Mouse";
        public bool always = true; //after all this time? always
        public Animation animationMouse { get { return GetComponent<Animation>(); } }
        public AudioClip squeak;

        private AudioSource source { get { return GetComponent<AudioSource>(); } }

        bool playedAlready = false;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player") && !playedAlready)
            {
                if (!always)
                {
                    if (GameManager.instance.currentMission % 2 == 0)
                    {
                        source.clip = squeak;
                        source.PlayDelayed(0.5f);
                        animationMouse.Play(animationName);
                        playedAlready = true;
                    }
                }
                else
                {
                    source.clip = squeak;
                    source.PlayDelayed(0.5f);
                    animationMouse.Play(animationName);
                    playedAlready = true;
                }
            }
        }
    }
}
