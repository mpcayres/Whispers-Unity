﻿using UnityEngine;
using UnityEngine.UI;

namespace CrowShadowMenu
{
    [RequireComponent(typeof(Button))]
    public class ClickSound : MonoBehaviour
    {
        public AudioClip sound;
        private Button button { get { return GetComponent<Button>(); } }
        private AudioSource source { get { return GetComponent<AudioSource>(); } }

        void Start()
        {
            gameObject.AddComponent<AudioSource>();
            source.clip = sound;
            source.playOnAwake = false;
            button.onClick.AddListener(() => PlaySoud());
        }

        void PlaySoud()
        {
            source.PlayOneShot(sound);
        }

    }
}