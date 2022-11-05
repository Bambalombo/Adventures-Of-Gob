using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGame : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }
}