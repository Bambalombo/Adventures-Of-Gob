using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChanger : MonoBehaviour
{
    private bool badWorldActive;
    public GameObject badWorld, goodWorld;

    public List<GameObject> badWorldObjects, goodWorldObjects;

    [Header("Background:")]
    public SpriteRenderer backgroundSpriteRenderer;
    public Sprite backgroundSpriteBad, backgroundSpriteGood;

    void Start()
    {
        for (int i = 0; i < badWorld.transform.childCount; i++)
        {
            badWorldObjects.Add(badWorld.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < goodWorld.transform.childCount; i++)
        {
            goodWorldObjects.Add(goodWorld.transform.GetChild(i).gameObject);
        }

        ToggleWorld();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWorld();
        }
    }

    private void ToggleWorld()
    {
        if (badWorldActive)
        {
            for (int i = 0; i < goodWorldObjects.Count; i++)
            {
                goodWorldObjects[i].SetActive(true);
            }
            for (int i = 0; i < badWorldObjects.Count; i++)
            {
                badWorldObjects[i].SetActive(false);
            }
            badWorldActive = false;
            backgroundSpriteRenderer.sprite= backgroundSpriteGood;
        }
        else
        {
            for (int i = 0; i < goodWorldObjects.Count; i++)
            {
                goodWorldObjects[i].SetActive(false);
            }
            for (int i = 0; i < badWorldObjects.Count; i++)
            {
                badWorldObjects[i].SetActive(true);
            }
            badWorldActive = true;
            backgroundSpriteRenderer.sprite = backgroundSpriteBad;
        }
    }
}