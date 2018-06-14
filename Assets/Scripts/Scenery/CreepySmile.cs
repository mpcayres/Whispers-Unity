using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

namespace CrowShadowScenery
{
    public class CreepySmile : MonoBehaviour
    {
        public float blinkTime;

        private GameObject target;

        private float count = 0;
        private int status = 0;
        private float smooth = 2.0F;
        private float tiltAngle = 20.0F;

        void Start()
        {
            target = GameManager.instance.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            count += Time.deltaTime;

            float tiltAroundZ = CrossPlatformInputManager.GetAxis("Horizontal") * tiltAngle;
            Quaternion aux = Quaternion.Euler(0, 0, tiltAroundZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, aux, Time.deltaTime * smooth);

            if (count >= blinkTime)
            {
                if (status == 0)
                {
                    transform.localScale += new Vector3(0.1F, 0.1F, 0);
                    status = 1;
                }
                else if (status == 1)
                {
                    transform.localScale += new Vector3(0.1F, 0.1F, 0);
                    status = 2;
                }
                else if (status == 2)
                {
                    transform.localScale -= new Vector3(0.1F, 0.1F, 0);
                    status = 3;
                }
                else
                {
                    transform.localScale -= new Vector3(0.1F, 0.1F, 0);
                    status = 0;
                }
                count = 0;
            }

            Vector3 newPosition = new Vector3(target.transform.position.x,
                    target.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.time);
        }
    }
}