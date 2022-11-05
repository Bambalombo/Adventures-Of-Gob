using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterkill : MonoBehaviour
{
    public Transform TeleportTarget;

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            collider2D.transform.position = TeleportTarget.transform.position;
            SoundController.instance.PlaySound(SoundController.instance.changeTimeSound);
        }
    }
}