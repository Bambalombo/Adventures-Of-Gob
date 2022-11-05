using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrøPlatform : MonoBehaviour
{
    public GameObject froe;
    public GameObject froetwo;

    void SpawnFroe()
    {
        //twoPos = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
        if (Input.GetKeyDown(KeyCode.T))
            {
            Instantiate(froe);
        }
        Instantiate(froetwo, new Vector3(2f, 0, 0), Quaternion.identity);
    }
}