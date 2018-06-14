using UnityEngine;
using System.Collections;

namespace CrowShadowScenery
{
    public class BlinkLight : MonoBehaviour
    {
        public float minWaitTime;
        public float maxWaitTime;

        Light testLight;

        void Start()
        {
            testLight = GetComponent<Light>();
            StartCoroutine(Flashing());
        }

        IEnumerator Flashing()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
                testLight.enabled = !testLight.enabled;
            }
        }
    }
}