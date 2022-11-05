using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    void Start()
    {
        Debug.Log("This scene has been loaded.");
    }

    void Update()
    {
        bool restart = Input.GetKeyDown(KeyCode.R);

        if (restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}