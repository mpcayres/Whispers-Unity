using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public bool followPlayer;
    public float offsetX, offsetY;
    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (followPlayer)
        {
            Vector3 newPosition = new Vector3(target.transform.position.x + offsetX, 
                target.transform.position.y + offsetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.time);
        }
    }
}
