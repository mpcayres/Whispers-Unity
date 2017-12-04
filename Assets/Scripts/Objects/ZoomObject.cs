using UnityEngine;

public class ZoomObject : MonoBehaviour {

    public bool colliding = false;
    public float scaleX, scaleY;
    bool showImage = false;
    GameObject objectInstance;

    void Start()
    {

    }

    void Update()
    {
        if (colliding && Input.GetKeyDown(KeyCode.Z) && !MissionManager.instance.blocked && !MissionManager.instance.paused)
        {
            if (!showImage) {
                showImage = true;
                GameObject camera = GameObject.Find("MainCamera");
                Vector3 pos = new Vector3(
                    camera.transform.position.x, 
                    camera.transform.position.y, 
                    -1);
                objectInstance = Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
                objectInstance.transform.localScale = new Vector3(scaleX, scaleY, 1);
                objectInstance.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                showImage = false;
                Destroy(objectInstance);
            }
            MissionManager.instance.pausedObject = showImage;
        }
    }
}
