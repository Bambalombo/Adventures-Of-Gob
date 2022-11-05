using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ferriemove : MonoBehaviour
{
    /*public float speed = 1f;
    public bool moveright = false;
    public Vector3 left;
    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
      //  //GameObject sign = GameObject.Find("sign");
        //int direction = sign.Getcomponent<direction>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var step = speed * Time.deltaTime;
        bool ferriemove = GameObject.Find("sign").GetComponent<sign>().ferriemove;
        if (ferriemove)
        {
            Debug.Log("it works");
            if (transform.position==left)
            {
                moveright = true;
            }
            if (transform.position==start)
            {
                moveright = false;
            }
            if (moveright)
            {
                transform.position = Vector3.MoveTowards(transform.position, start, step);
            }
            if(!moveright)
            {
                transform.position = Vector3.MoveTowards(transform.position, left, step);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag=="Player") 
        {
            collider2D.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag=="Player")
        {
            collider2D.gameObject.transform.SetParent(null);
        }
    }*/
}

