using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // behøves ikke når tekst til at vise hvad der foregår (point/true/false) slettes

public class BackgroundColorAndSpriteChange : MonoBehaviour
{
    int goodObjectsCount;
    int badObjectsCount;

    public GameObject goodObjectsContainer;
    public GameObject badObjectsContainer;
    public GameObject pointText;

    GameObject[] badObjectsArray;
    GameObject[] goodObjectsArray;

    GameObject[] badBG;
    GameObject[] medBG;

    private void Start()
    {
        goodObjectsCount = goodObjectsContainer.transform.childCount;   // der tælles hvor mange gameobjects der ligger i goodContainer
        badObjectsCount = badObjectsContainer.transform.childCount;     // der tælles hvor mange gameobjects der ligger i badContainer

        badBG = GameObject.FindGameObjectsWithTag("BadBG");
        medBG = GameObject.FindGameObjectsWithTag("MediumBG");

        CreateContainerArrays();
    }

    public void UpdateMiddleGroundColor()
    {
        Debug.Log($"EcoPoints: {GameController._instance.ecoPoints}. MaxEcoPoints: {GameController._instance.maxPointsObtainable}");
        byte backgroundColorChange = (byte)(255 - GameController._instance.ecoPoints * (255 / (GameController._instance.maxPointsObtainable / 2)));
        Debug.Log(backgroundColorChange);

        if(GameController._instance.ecoPoints < GameController._instance.maxPointsObtainable /2)
        {
            foreach (var image in badBG)
            {
            image.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, backgroundColorChange);
            }
        }

        if(GameController._instance.ecoPoints > GameController._instance.maxPointsObtainable /2)
        {
            byte mediumBGchange = (byte)(255 - GameController._instance.ecoPoints * (255 / (GameController._instance.maxPointsObtainable / 2)));

            foreach (var image in badBG) // hvis ecopoints er over halvdelen, sættes opacity til 0 for øverste lag.
            {
                image.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
            }

            foreach (var image in medBG)
            {
            image.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, mediumBGchange);
            }
        }
    }

    void CreateContainerArrays()
    {
        goodObjectsArray = new GameObject[goodObjectsCount];    // array for goodObjects oprettes med det antal der er i goodContainer, talt i start
        badObjectsArray = new GameObject[badObjectsCount];      // array for badObjects oprettes med det antal der er i badContainer, talt i start

        for (int i = 0; i < goodObjectsCount; i++) // good
        {
            goodObjectsArray[i] = goodObjectsContainer.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < badObjectsCount; i++) // bad
        {
            badObjectsArray[i] = badObjectsContainer.transform.GetChild(i).gameObject;
        }
    }

    public void DecideActiveBackgroundObjects()
    {
        for (int i = 0; i < goodObjectsCount; i++)
        {
            if (i * (GameController._instance.maxPointsObtainable / goodObjectsCount) > GameController._instance.ecoPoints)
            {
                goodObjectsContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                goodObjectsContainer.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < badObjectsCount; i++)
        {
            if (i * (GameController._instance.maxPointsObtainable / badObjectsCount) > GameController._instance.ecoPoints)
            {
                badObjectsContainer.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                badObjectsContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateBackground()
    {
        UpdateMiddleGroundColor();
        DecideActiveBackgroundObjects();
    }
}