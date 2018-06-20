﻿using UnityEngine;
using CrowShadowManager;

namespace CrowShadowNPCs
{
    public class Crow : Follower
    {
        public static Crow instance;
        public float timeBirdsFollow = 1f;

        protected GameObject birdEmitter;

        protected new void Start()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
                animator = GetComponent<Animator>();
                player = GameManager.instance.gameObject;
                spriteRenderer = GetComponent<SpriteRenderer>();
                birdEmitter = transform.Find("BirdEmitterCollider").gameObject;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected new void Update()
        {

            base.Update();

            if (birdEmitter.activeSelf /*&& birdEmitter.GetComponent<ParticleSystem>().time <= timeBirdsFollow*/)
            {
                //birdEmitter.transform.LookAt(player.transform);
                //da forma comentada ele só segue o player por um período de tempo
                //porém assim acontecia de pássaros já existentes do nada mudarem a posição
                Vector3 dir = player.transform.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(dir);
                birdEmitter.transform.rotation = Quaternion.Lerp(birdEmitter.transform.rotation, rot, timeBirdsFollow / 2 * Time.deltaTime);
            }

        }

        public void LookAtPlayer()
        {
            if (birdEmitter != null && player != null)
            {
                birdEmitter.transform.LookAt(player.transform);
            }
        }

        public void DestroyRaven()
        {
            Destroy(gameObject);
        }

        protected new void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasActionPatroller)
            {
                print("CorvoFollower: " + collision.tag);
                if (collision.gameObject.tag.Equals("Player"))
                {
                    GameManager.instance.GameOver();
                }
            }
        }
    }
}