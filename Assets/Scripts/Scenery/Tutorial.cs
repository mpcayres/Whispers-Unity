using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Tutorial : MonoBehaviour
{
    public string keyName1, keyName2;
    public int mission = 1;
    public AudioClip click;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }

    int repeatTime = 2;
    bool exit = false, end = false, invoked = false;

    void Awake()
    {
        source.playOnAwake = false;
    }

    void Update()
    {
        if (!end && !exit)
        {
            KeyPressed();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == mission && !end)
        {
            exit = false;
            InvokeShow();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == mission && !end)
        {
            InvokeShow();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == mission)
        {
            exit = true;
            invoked = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void KeyPressed()
    {
        if (CrossPlatformInputManager.GetButtonDown(keyName1) && CrossPlatformInputManager.GetButtonDown(keyName2))
        {
            end = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void InvokeShow()
    {
        if (!source.isPlaying && !invoked)
        {
            Invoke("Show", repeatTime);
            invoked = true;
        }
    }

    void Show() {
        if (!end && !exit) {
            if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            source.PlayOneShot(click);
            invoked = false;
        }
    }

}
