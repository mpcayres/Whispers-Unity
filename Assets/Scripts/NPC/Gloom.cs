using UnityEngine;
using System.Collections;

public class Gloom : MonoBehaviour
{
    private int maxRoar = 5;
    private void Start()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (Flashlight.GetState() && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
            maxRoar--;
            if(maxRoar < 0)
            {
                MissionManager.instance.GameOver();
            }
        }
    }
}
