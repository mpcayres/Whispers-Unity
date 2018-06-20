﻿using UnityEngine;
using CrowShadowManager;
using CrowShadowObjects;
using CrowShadowScenery;

namespace CrowShadowPlayer
{
    public class PlayerAction : MonoBehaviour
    {
        GameObject target;

        void Start()
        {
            target = GameManager.instance.gameObject;
        }

        void Update()
        {
            transform.position = target.transform.position;
        }

        //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
        private void OnTriggerStay2D(Collider2D collision)
        {
            //print("PlayerAction");
            if (collision.gameObject.tag.Equals("SceneObject"))
            {
                collision.gameObject.GetComponent<SceneObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("MovingObject"))
            {
                collision.gameObject.GetComponent<MovingObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("FurtiveObject"))
            {
                collision.gameObject.GetComponent<FurtiveObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("PickUpObject"))
            {
                collision.gameObject.GetComponent<PickUpObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("ZoomObject"))
            {
                collision.gameObject.GetComponent<ZoomObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("ScenePickUpObject"))
            {
                collision.gameObject.GetComponent<ScenePickUpObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("SceneMultipleObject"))
            {
                collision.gameObject.GetComponent<SceneMultipleObject>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("Lamp"))
            {
                collision.gameObject.GetComponent<Lamp>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("SickCrow"))
            {
                collision.gameObject.GetComponent<SickCrow>().colliding = true;
            }
            else if (collision.gameObject.tag.Equals("WindowTrigger"))
            {
                collision.gameObject.GetComponent<WindowTrigger>().colliding = true;
                collision.gameObject.GetComponent<WindowTrigger>().ScareTrigger();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("SceneObject"))
            {
                collision.gameObject.GetComponent<SceneObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("MovingObject"))
            {
                collision.gameObject.GetComponent<MovingObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("FurtiveObject"))
            {
                collision.gameObject.GetComponent<FurtiveObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("PickUpObject"))
            {
                collision.gameObject.GetComponent<PickUpObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("ZoomObject"))
            {
                collision.gameObject.GetComponent<ZoomObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("ScenePickUpObject"))
            {
                collision.gameObject.GetComponent<ScenePickUpObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("SceneMultipleObject"))
            {
                collision.gameObject.GetComponent<SceneMultipleObject>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("Lamp"))
            {
                collision.gameObject.GetComponent<Lamp>().colliding = false;
            }
            else if (collision.gameObject.tag.Equals("WindowTrigger"))
            {
                collision.gameObject.GetComponent<WindowTrigger>().colliding = false;
            }
        }

    }
}