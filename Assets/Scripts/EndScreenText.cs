using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenText : MonoBehaviour
{
    TextMeshPro endScreenText;

    private void Start()
    {
        endScreenText = gameObject.GetComponent<TextMeshPro>();
        endScreenText.text = $"Godt klaret!\n\n{EndScoreComment()}\n\n{EndScreenRecommendations()}";
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0.005f, 0);
    }

    float EndScore()
    {
        return (Mathf.Round(GameController._instance.ecoPoints/ GameController._instance.maxPointsObtainable*100));
    }

    void EndText()
    {
        string endText = "";

        endText += "\nLightsOff: " + GameController._instance.lightsOff.ToString();
        endText += "\nPlantedTree: " + GameController._instance.plantedTree.ToString();
        endText += "\nBikeDriven: " + GameController._instance.bikeDriven.ToString();
        endText += "\nCarDriven " + GameController._instance.carDriven.ToString();
        endText += "\nBatteryGood: " + GameController._instance.batteryGood.ToString();
        endText += "\nBatteryBad: " + GameController._instance.batteryBad.ToString();
        endText += "\nBulbGood: " + GameController._instance.bulbGood.ToString();
        endText += "\nBulbMedium: " + GameController._instance.bulbMedium.ToString();
        endText += "\nBulbBad: " + GameController._instance.bulbBad.ToString();
        endText += "\nFoodGood: " + GameController._instance.foodGood.ToString();
        endText += "\nFoodMedium: " + GameController._instance.foodMedium.ToString();
        endText += "\nFoodBad " + GameController._instance.foodBad.ToString();
        endText += "\nPlantSeed: " + GameController._instance.plantSeed.ToString();

        // gob house lights on/off
        // ladder/plant tree
        // solar panel on roof
        // bil/cykel
        // batterier
        // bulb
        // mad
        // plantefrø

        // evt:
        //  tøj i butik
        //  donér penge

    }

    string EndScoreComment()
    {
        if (EndScore() > 80)
        {
            return "";
        }
        else if (EndScore() > 60)
        {
            return "";
        }
        else if (EndScore() > 40)
        {
            return "";
        }
        else if (EndScore() > 20)
        {
            return "";
        }
        else
        {
            return "Det var ik et synderligt godt forsøg. Prøv igen!";
        }
    }

    string EndScreenRecommendations()
    {
        return "";
    }
}