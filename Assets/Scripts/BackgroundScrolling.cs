using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Camera cam;
    public float leftPadding = 10;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = new Vector3((cam.transform.position.x/2) + leftPadding, transform.position.y, 0);
    }
}