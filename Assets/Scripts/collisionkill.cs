using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionkill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("kill");
            playercontroller.instance.deathlogic();
            Invoke("playerdeath", 0.5f);   
        }
    }

    private void playerdeath()
    {
        Destroy(GameObject.Find("Player"));
        // manage scene
    }
}
