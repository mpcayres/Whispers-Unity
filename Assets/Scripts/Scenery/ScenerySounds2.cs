using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScenerySounds2 : MonoBehaviour

{
    public AudioClip doorclose1, dooropen1, dooropen2, doorclosed;
    public AudioClip window_open, window_close;
    public AudioClip gas_long, gas_short;
    public AudioClip slide1, slide2;
    public AudioClip paper1, paper2, paper3;



    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    

    // Use this for initialization
    void Start()

    {
        source.playOnAwake = false;

    }
    public void StopSound()
    {
        source.Stop();
    }
    public void PlayDoorOpen(int number=1)
    {
        switch (number)
        {
            case 1:
                source.clip = dooropen1;
                source.PlayOneShot(dooropen1);
                break;
            case 2:
                source.clip = dooropen2;
                source.PlayOneShot(dooropen2);
                break;


        }
    }
    
    public void PlayDorClose()
    {
        source.clip = doorclose1;
        source.PlayOneShot(doorclose1);

    }
    public void PlayDoorClosed()
    {

        source.clip = doorclosed;
        source.PlayOneShot(doorclosed);

    }

    public void PlayWindow(int number)
    {
        switch (number)
        {
            case 1:
                source.clip = window_open;
                source.PlayOneShot(window_open);
                break;
            case 2:
                source.clip = window_close;
                source.PlayOneShot(window_close);
                break;
        }

    }
    public void PlayGas(int number =1)
    {
        switch (number)
        {
            case 1:
                source.clip = gas_short;
                source.PlayOneShot(gas_short);
                break;
            case 2:
                source.clip = gas_long;
                source.PlayOneShot(gas_long);
                break;

        }
    }
    public void PlaySlide(int number=1)
    {

        switch (number)
        {
            case 1:
                source.clip = slide1;
                source.PlayOneShot(slide1);
                break;
            case 2:
                source.clip = slide2;
                source.PlayOneShot(slide2);
                break;
        }

    }
    public void PlayPaper(int number = 1)
    {

        switch (number)
        {
            case 1:
                source.clip = paper1;
                source.PlayOneShot(paper1);
                break;
            case 2:
                source.clip = paper2;
                source.PlayOneShot(paper2);
                break;
            case 3:
                source.clip = paper3;
                source.PlayOneShot(paper3);
                break;
        }

    }


}