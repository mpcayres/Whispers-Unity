using UnityEngine;

namespace CrowShadowScenery
{
    public class GardenSounds : MonoBehaviour

    {
        public AudioClip bat1, bat2, crow, owl;
        public AudioClip wolf1, wolf2;

        public bool EnableSound = true;
        public int NightLoop = 0;

        private AudioSource source { get { return GetComponent<AudioSource>(); } }

        void Start()
        {
            source.playOnAwake = false;
        }

        private void Update()
        {
            if (EnableSound == true)
            {
                EnableSound = false;
                Invoke("PlayNightLoop", 30);
                Invoke("EnableSoundLoop", 30);
            }
        }

        void PlayNightLoop()
        {
            PlayNight(NightLoop);
            NightLoop++;
            if (NightLoop > 5)
            {
                NightLoop = 0;
            }
        }

        void EnableSoundLoop()
        {
            if (EnableSound == true)
            {
                EnableSound = false;
            }
            else
            {
                EnableSound = true;

            }
        }

        public void PlayNight(int number)
        {
            switch (number)
            {
                case 1:
                    source.clip = bat1;
                    source.PlayOneShot(bat1);
                    break;
                case 2:
                    source.clip = wolf2;
                    source.PlayOneShot(wolf2);
                    break;
                case 3:
                    source.clip = crow;
                    source.PlayOneShot(crow);
                    break;
                case 4:
                    source.clip = owl;
                    source.PlayOneShot(owl);
                    break;
                case 5:
                    source.clip = wolf1;
                    source.PlayOneShot(wolf1);
                    break;
            }
        }
    }
}