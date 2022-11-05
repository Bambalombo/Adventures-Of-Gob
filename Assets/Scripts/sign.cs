using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : MonoBehaviour
{
    public Transform ferrieobject;
    public bool ferriemove = false;
    // bool moveright = false;

    private void OnTriggerStay2D(Collider2D collider2D){
        if (Input.GetKey(KeyCode.E)){
            Debug.Log("button press");
            ferriemove=true;
        }
    }
}