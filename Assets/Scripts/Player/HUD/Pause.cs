using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowPlayer
{
    public class Pause : MonoBehaviour
    {
        public GameObject pauseMenu;
        public AudioClip sound;
        
        private AudioSource source { get { return GetComponent<AudioSource>(); } }

        private void Awake()
        {
            pauseMenu = GameObject.Find("HUDCanvas").transform.Find("PauseMenu").gameObject;
            pauseMenu.SetActive(false);
        }

        void Start()
        {
            gameObject.AddComponent<AudioSource>();
            source.clip = sound;
            source.playOnAwake = false;
        }

        void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("keyPause") && !GameManager.instance.blocked &&
                !GameManager.instance.showMissionStart && !GameManager.instance.pausedObject && !Book.show)
            {
                if (!source.isPlaying)
                    source.PlayOneShot(sound);

                if (pauseMenu.activeSelf)
                {
                    GameManager.instance.paused = false;
                    //gameManager.blocked = false;
                    pauseMenu.SetActive(false);
                }
                else
                {
                    GameManager.instance.paused = true;
                    //gameManager.blocked = true;
                    pauseMenu.SetActive(true);
                }
            }
        }

    }
}