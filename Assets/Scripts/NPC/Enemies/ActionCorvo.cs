using UnityEngine;

public class ActionCorvo : MonoBehaviour {
    GameObject target;
    public static ActionCorvo instance;
    public string tagAction = "Corvo";

    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            target = GameObject.FindGameObjectWithTag(tagAction);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.time);
    }

    public void DestroyAction()
    {
        Destroy(gameObject);
    }

    //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Action: " + collision.tag);
        if (collision.gameObject.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }

    }
}
