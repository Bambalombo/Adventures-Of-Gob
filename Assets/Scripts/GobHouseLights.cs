using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobHouseLights : MonoBehaviour
{
    public GameObject containerLightsOn;
    public GameObject containerLightsOff;

    bool colliding = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliding = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colliding == true && GameController._instance.lightsOff == true)
        {
            containerLightsOff.SetActive(false);
            containerLightsOn.SetActive(true);
            GameController._instance.lightsOff = false;
            GameController._instance.SubtractEcoPoints(5);
        }
        else if (Input.GetKeyDown(KeyCode.E) && colliding == true && GameController._instance.lightsOff == false)
        {
            containerLightsOff.SetActive(true);
            containerLightsOn.SetActive(false);
            GameController._instance.lightsOff = true;
            GameController._instance.AddEcoPoints(5);
        }
    }
}