using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Player") && GameController._instance.justTeleported)
        {
            Debug.Log("ggoo");
            collision.transform.position += new Vector3(-2, 2, 0);

            GameObject Smokey = Instantiate(Resources.Load("SmokeCloud"), collision.gameObject.transform.position, Quaternion.identity) as GameObject;
            Smokey.transform.parent = collision.gameObject.transform;
            Destroy(Smokey, 5);
        }
    }
}