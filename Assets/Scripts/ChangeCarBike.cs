using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarBike : MonoBehaviour
{
    public Sprite carSprite, bikeCSprite;
       void Start()
    {
        if (GameController._instance.bikeDriven) GetComponent<SpriteRenderer>().sprite = bikeCSprite;
        else GetComponent<SpriteRenderer>().sprite = carSprite;
    }
}