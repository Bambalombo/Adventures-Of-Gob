using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Retry : MonoBehaviour
{
   public void RETRY()
    {
        GameController._instance.ecoPoints = 0;
        GameController._instance.batteryBad = false;
        GameController._instance.batteryGood = false;
        GameController._instance.bulbBad = false;
        GameController._instance.bulbGood = false;
        GameController._instance.bulbMedium = false;
        GameController._instance.foodBad = false;
        GameController._instance.foodGood = false;
        GameController._instance.foodMedium = false;
        GameController._instance.carDriven = false;
        GameController._instance.bikeDriven = false;
        GameController._instance.gotKeys = false;
        GameController._instance.lightsOff = false;
        GameController._instance.plantedTree = false;
        GameController._instance.plantSeed = false;
        GameController._instance.potplantPlanted = false;
        GameController._instance.solarPanelPlanted = false;
        GameController._instance.varerBetalt = false;
        GameController._instance.curMonologID = 0;
        SceneManager.LoadScene(1);






    }
}
