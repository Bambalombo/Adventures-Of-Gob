using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideWalls : MonoBehaviour
{
    public GameObject mallForeground;
    GameObject player;
    public float xValue = 2.5f;

    private void Awake()
    {
        player = gameObject;
    }

    void Update()
    {
        if (player.transform.position.x > xValue)
        {
            mallForeground.SetActive(false);
        }
        else
        {
            mallForeground.SetActive(true);
        }
    }
}