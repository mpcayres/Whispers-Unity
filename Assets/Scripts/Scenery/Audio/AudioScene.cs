using UnityEngine;

namespace CrowShadowScenery
{
    public class AudioScene : MonoBehaviour
    {
        public AudioClip sound;
        public AudioSource source { get { return GetComponent<AudioSource>(); } }
        
        void Start()
        {
            source.clip = sound;
            source.playOnAwake = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                source.PlayOneShot(sound);
            }
        }
    }
}