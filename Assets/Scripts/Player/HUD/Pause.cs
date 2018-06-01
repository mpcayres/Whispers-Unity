using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowPlayer
{
    public class Pause : MonoBehaviour
    {
        public AudioClip sound;
        private AudioSource source { get { return GetComponent<AudioSource>(); } }
        public GameObject pauseMenu;
        GameManager gameManager;

        private void Awake()
        {
            pauseMenu = GameObject.Find("HUDCanvas").transform.Find("PauseMenu").gameObject;
            pauseMenu.SetActive(false);
            gameManager = GameObject.Find("Player").GetComponent<GameManager>();
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
                    gameManager.paused = false;
                    //gameManager.blocked = false;
                    pauseMenu.SetActive(false);
                }
                else
                {
                    gameManager.paused = true;
                    //gameManager.blocked = true;
                    pauseMenu.SetActive(true);
                }
            }
        }

    }
}