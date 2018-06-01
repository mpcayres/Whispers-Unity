using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Pause: MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    public GameObject pauseMenu;
    MissionManager missionManager;

    private void Awake()
    {
        pauseMenu = GameObject.Find("HUDCanvas").transform.Find("PauseMenu").gameObject;
        pauseMenu.SetActive(false);
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("keyPause") && !MissionManager.instance.blocked && !MissionManager.instance.showMissionStart && !MissionManager.instance.pausedObject && !Book.show)
        {
            if (!source.isPlaying)
                source.PlayOneShot(sound);

            if (pauseMenu.activeSelf)
            {
                missionManager.paused = false;
                //missionManager.blocked = false;
                pauseMenu.SetActive(false);
            }
            else
            {
                missionManager.paused = true;
                //missionManager.blocked = true;
                pauseMenu.SetActive(true);
            }
        }
    }

}
