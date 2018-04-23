using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickCrow : MonoBehaviour
{
    public bool fly = false;
    public GameObject armario;

    public Animation animationSick { get { return GetComponent<Animation>(); } }

    void Start()
    {
         if(MissionManager.instance.currentMission >1)
         {
             this.gameObject.SetActive(false);
         }
         else
         {
            armario = GameObject.Find("Armario").gameObject;
            armario.GetComponent<SceneObject>().isCrowSick = true;
            armario.GetComponent<SceneObject>().isUp = true;
        }
    }
}
