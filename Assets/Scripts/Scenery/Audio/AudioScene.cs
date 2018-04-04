using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScene : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start()
    {
        source.clip = sound;
        source.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player")){ 
            source.PlayOneShot(sound);
        }
    }
}
