using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    public Transform target;
    private float moveSpeed = 6;

    public bool camInBadWorld;
    public float yPos, yposOriginal;

    public float newX;

    private float StartCamSize = 4.7f;
    public float CamSizeChangeValue;

    void Start()
    {
        camera = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        yPos = transform.position.y;
        yposOriginal = yPos;

        if (SceneManager.GetActiveScene().buildIndex == 3 || (SceneManager.GetActiveScene().buildIndex == 4)) // ny linus
        {
            DecideWorld();
        }
        camera.orthographicSize = StartCamSize + CamSizeChangeValue;

    }

    void Update()
    {
        newX = Mathf.Lerp(transform.position.x, target.position.x, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(newX, yPos, -1);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            DecideWorld();
        }
    }

    void DecideWorld()
    {
        if (camInBadWorld)
        {
            yPos = yposOriginal;
            camInBadWorld = false;
        }
        else
        {
            yPos = yposOriginal + 15;

            camInBadWorld = true;
        }
    }
}