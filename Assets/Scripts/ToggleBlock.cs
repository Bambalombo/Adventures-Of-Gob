using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBlock : MonoBehaviour
{
    bool deactivated;

    private void Update()
    {
        CheckIfEnabled();
    }

    void CheckIfEnabled()
    {
        if (GameController._instance.varerBetalt == true && deactivated == false)
        {
            gameObject.SetActive(false);
            deactivated = true;
        }
    }
}