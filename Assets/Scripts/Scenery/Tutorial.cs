using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    int repeatTime = 2;
    bool exit = false;
    public AudioClip click;
    bool invoked = false;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    void Start()
    {
        source.playOnAwake = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1)
        {
            if (!source.isPlaying && !invoked)
            {
                Invoke("Show", repeatTime);
                invoked = true;
            }
        }
        if (exit)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1)
        {
            if (!source.isPlaying && !invoked)
            {
                Invoke("Show", repeatTime);
                invoked = true;
            }
        }
        if (exit)
        {
             this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && MissionManager.instance.currentMission == 1)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            exit = true;
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
