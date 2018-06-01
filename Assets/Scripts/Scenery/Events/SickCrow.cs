using UnityEngine;
using CrowShadowManager;
using CrowShadowObjects;

namespace CrowShadowScenery
{
    public class SickCrow : MonoBehaviour
    {
        public bool fly = false;
        public GameObject armario;

        public Animation animationSick { get { return GetComponent<Animation>(); } }

        void Start()
        {
            if (GameManager.instance.currentMission > 1 || GameManager.instance.mission1MaeQuarto)
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
}