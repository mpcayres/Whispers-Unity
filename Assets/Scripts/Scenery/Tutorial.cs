using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Tutorial : MonoBehaviour
{
    bool again;
    public string keyName1, keyName2;
    int repeatTime = 2;
    bool exit = false;
    public AudioClip click;
    bool invoked = false;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    void Start()
    {
        again = true;
        source.playOnAwake = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (exit)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1 && again)
        {
            if (!source.isPlaying && !invoked)
            {
                Invoke("Show", repeatTime);
                invoked = true;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown(keyName1) && CrossPlatformInputManager.GetButtonDown(keyName2))
        {
            exit = true;
            again = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (exit)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1 && again)
        {
            if (!source.isPlaying && !invoked)
            {
                Invoke("Show", repeatTime);
                invoked = true;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown(keyName1) && CrossPlatformInputManager.GetButtonDown(keyName2)){
            exit = true;
            again = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            exit = true;
        }
        if (CrossPlatformInputManager.GetButtonDown(keyName1) && CrossPlatformInputManager.GetButtonDown(keyName2)){
            exit = true;
            again = false;
            invoked = true;
        }
    }

    public void Show() {
        if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy) {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        source.PlayOneShot(click);
        invoked = false;
    }




}
