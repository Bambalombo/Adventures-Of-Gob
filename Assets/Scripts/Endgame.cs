using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    bool cando = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cando = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cando = false;
    }

    private void Update()
    {
        if (cando)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}