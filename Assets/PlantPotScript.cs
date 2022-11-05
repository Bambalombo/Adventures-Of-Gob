using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPotScript : MonoBehaviour
{
    private Interactable interactable;
    public Sprite froSprite;
    public GameObject objectToSetActive;
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable.isBeingHighlighted && GameController._instance.plantSeed)
        {
            GetComponent<SpriteRenderer>().sprite = froSprite;
            objectToSetActive.SetActive(true);
            GameController._instance.potplantPlanted = true;
        }
    }
}
