using UnityEngine;

public class Page : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Book.AddPage();
            MissionManager.instance.UpdateSave();
            Destroy(gameObject);
        }
    }

}
