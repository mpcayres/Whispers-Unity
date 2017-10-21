using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    Player script;
    float rotationSpeed = 1f;
    Light light;

    void Start ()
    {
        script = GetComponentInParent<Player>();
        light = GetComponent<Light>();
    }
	
	void Update ()
    {
        //0 = east, 1 = west, 2 = north, 3 = south
        if (Input.GetKeyDown(KeyCode.F))
        {
            light.enabled = !light.enabled;
            if (light.enabled)
            {
                transform.rotation = Quaternion.Euler((float)0.0, (float)0.0, (float)0.0);
            }
        }

        if (light.enabled)
        {
            switch (script.direction)
            {
                case 0:
                    Quaternion targetRotationE = Quaternion.Euler((float)180.0, (float)230.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationE, rotationSpeed * Time.deltaTime);
                    break;
                case 1:
                    Quaternion targetRotationW = Quaternion.Euler((float)180.0, (float)130.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationW, rotationSpeed * Time.deltaTime);
                    break;
                case 2:
                    Quaternion targetRotationN = Quaternion.Euler((float)-45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationN, rotationSpeed * Time.deltaTime);
                    break;
                case 3:
                    Quaternion targetRotationS = Quaternion.Euler((float)45.0, (float)0.0, (float)0.0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationS, rotationSpeed * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }
        
	}
}
