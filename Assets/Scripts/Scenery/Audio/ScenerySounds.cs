using UnityEngine;

namespace CrowShadowScenery
{
    public class ScenerySounds : MonoBehaviour
    {
        public AudioClip bat1, bat2, crow, owl, parrot, wings;
        public AudioClip cat1, cat2, cat3, cat4;
        public AudioClip wolf1, wolf2;
        public AudioClip demon1, demon2, demon3, demon4, demon5, demon6, demon7, demon8;
        public AudioClip heartbeat1, heartbeat2, heartbeat3, heartbeat4, heartbeat5;
        public AudioClip scare1, scare2, scare3, scare4, scare5;
        public AudioClip ambient1, ambient2;
        public AudioClip drop;

        public AudioSource source { get { return GetComponent<AudioSource>(); } }

        public bool Night = false;
        public bool EnableSound = true;

        public int NightLoop = 0;
        
        void Start()
        {
            // gameObject.AddComponent<AudioSource>();
            // source.clip = sound;
            source.playOnAwake = false;
            //button.onClick.AddListener(() => PlaySoud());

        }

        private void Update()
        {

            if (Night == true && EnableSound == true)
            {
                EnableSound = false;
                Invoke("PlayNightLoop", 60);
                Invoke("EnableSoundLoop", 60);
            }
        }

        void EnableSoundLoop()
        {
            if (EnableSound == true)
                EnableSound = false;
            else
            {
                EnableSound = true;

            }


        }

        void PlayNightLoop()
        {
            PlayNight(NightLoop);
            NightLoop++;
            if (NightLoop > 12)
                NightLoop = 0;
        }

        public void PlayDrop()
        {
            source.clip = drop;
            source.PlayOneShot(drop);
        }

        public void StopSound()
        {
            source.Stop();
        }

        public void PlayCat(int number)
        {
            switch (number)
            {
                case 1:
                    source.clip = cat1;
                    source.PlayOneShot(cat1);
                    break;
                case 2:
                    source.clip = cat2;
                    source.PlayOneShot(cat2);
                    break;
                case 3:
                    source.clip = cat3;
                    source.PlayOneShot(cat3);
                    break;
                case 4:
                    source.clip = cat4;
                    source.PlayOneShot(cat4);
                    break;

            }

        }

        public void PlayBird(int number)
        {
            switch (number)
            {
                case 1:
                    source.clip = crow;
                    source.PlayOneShot(crow);
                    break;
                case 2:
                    source.clip = owl;
                    source.PlayOneShot(owl);
                    break;
                case 3:
                    source.clip = parrot;
                    source.PlayOneShot(parrot);
                    break;
                case 4:
                    source.clip = wings;
                    source.PlayOneShot(wings);
                    break;

            }

        }
        public void PlayBat(int number)
        {
            switch (number)
            {
                case 1:
                    source.clip = bat1;
                    source.PlayOneShot(bat1);
                    break;
                case 2:
                    source.clip = bat2;
                    source.PlayOneShot(bat2);
                    break;

            }
        }

        public void PlayDemon(int number)
        {

            switch (number)
            {
                case 1:
                    source.clip = demon1;
                    source.PlayOneShot(demon1);
                    break;
                case 2:
                    source.clip = demon2;
                    source.PlayOneShot(demon2);
                    break;
                case 3:
                    source.clip = demon3;
                    source.PlayOneShot(demon3);
                    break;
                case 4:
                    source.clip = demon4;
                    source.PlayOneShot(demon4);
                    break;
                case 5:
                    source.clip = demon5;
                    source.PlayOneShot(demon5);
                    break;
                case 6:
                    source.clip = demon6;
                    source.PlayOneShot(demon6);
                    break;
                case 7:
                    source.clip = demon7;
                    source.PlayOneShot(demon7);
                    break;
                case 8:
                    source.clip = demon8;
                    source.PlayOneShot(demon8);
                    break;

            }

        }

        public void PlayScare(int number)
        {

            switch (number)
            {
                case 1:
                    source.clip = scare1;
                    source.PlayOneShot(scare1);
                    break;
                case 2:
                    source.clip = scare2;
                    source.PlayOneShot(scare2);
                    break;
                case 3:
                    source.clip = scare3;
                    source.PlayOneShot(scare3);
                    break;
                case 4:
                    source.clip = scare4;
                    source.PlayOneShot(scare4);
                    break;
                case 5:
                    source.clip = scare5;
                    source.PlayOneShot(scare5);
                    break;

            }

        }

        public void PlayAmbient(int number)
        {

            switch (number)
            {
                case 1:
                    source.clip = ambient1;
                    source.PlayOneShot(ambient1);
                    break;
                case 2:
                    source.clip = ambient2;
                    source.PlayOneShot(ambient2);
                    break;
            }

        }

        public void PlayWolf(int number)
        {

            switch (number)
            {
                case 1:
                    source.clip = wolf1;
                    source.PlayOneShot(wolf1);
                    break;
                case 2:
                    source.clip = wolf2;
                    source.PlayOneShot(wolf2);
                    break;
            }

        }
        public void PlayHeartbeat(int number)
        {

            switch (number)
            {
                case 1:
                    source.clip = heartbeat1;
                    source.PlayOneShot(heartbeat1);
                    break;
                case 2:
                    source.clip = heartbeat2;
                    source.PlayOneShot(heartbeat2);
                    break;
                case 3:
                    source.clip = heartbeat3;
                    source.PlayOneShot(heartbeat3);
                    break;
                case 4:
                    source.clip = heartbeat4;
                    source.PlayOneShot(heartbeat4);
                    break;
                case 5:
                    source.clip = heartbeat5;
                    source.PlayOneShot(heartbeat5);
                    break;

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
                    source.clip = demon2;
                    source.PlayOneShot(demon2);
                    break;
                case 5:
                    source.clip = ambient1;
                    source.PlayOneShot(ambient1);
                    break;
                case 6:
                    source.clip = heartbeat1;
                    source.PlayOneShot(heartbeat1);
                    break;
                case 7:
                    source.clip = demon1;
                    source.PlayOneShot(demon1);
                    break;
                case 8:
                    source.clip = wolf1;
                    source.PlayOneShot(wolf1);
                    break;
                case 9:
                    source.clip = demon3;
                    source.PlayOneShot(demon3);
                    break;
                case 10:
                    source.clip = bat2;
                    source.PlayOneShot(bat2);
                    break;
                case 11:
                    source.clip = ambient2;
                    source.PlayOneShot(ambient2);
                    break;
                case 12:
                    source.clip = demon8;
                    source.PlayOneShot(demon8);
                    break;

            }

        }

    }
}