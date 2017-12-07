using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class FixMenu : MonoBehaviour
{

    public void DisablePlayer()
	{

       //this.gameObject.transform.Find("Player").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //GameObject player = GameObject.Find("Player");
        //Transform RPGTalkHolder = player.transform.GetChild(0);
      //  this.gameObject.transform.Find("Player").gameObject.transform.Find("RPGTalk Holder").gameObject.SetActive(false);
    }

    public void DisableParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

}
