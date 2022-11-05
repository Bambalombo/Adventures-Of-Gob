using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoPointsCheck : MonoBehaviour
{
    Inventory inventory;

    bool viheclePointsGiven;
    bool pointsGiven;

    void ChangePoints()
    {
        switch (gameObject.name) // switch tjekker for navnet på nuværende gameobject.
                                 // ... Dvs, dette script kan tilføjes til et object og under switchen tilføjes navnet på tilhørende gamobject,
                                 // ... og så kan der i den enkelte case bestemmes antal point for det givne valg
                                 // ... fungerer også som en liste over, hvilke gameobjects scripted er (/burde være) tilføjet til
        {
            case "treeFromAcorn": GameController._instance.AddEcoPoints(4); break;
            case "RoofWithSolarPanel": GameController._instance.AddEcoPoints(7); break;
            case "potteplante_Plante": GameController._instance.AddEcoPoints(6); break;

            default: break;
        }
    }

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        GivePointsForVihecleChoice();
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && pointsGiven == false) // hvis gameobjected er aktivt og ikke har kørt endnu køres denne kode
        {
            ChangePoints();
            pointsGiven = true;
        }
    }

    void GivePointsForVihecleChoice()
    {
        if (GameController._instance.bikeDriven && !viheclePointsGiven)
        {
            GameController._instance.AddEcoPoints(10);
            viheclePointsGiven = true;
        }
        else if (GameController._instance.carDriven && !viheclePointsGiven)
        {
            GameController._instance.SubtractEcoPoints(5);
            inventory.moneyInWallet -= 20f;
            viheclePointsGiven = true;
        }
    }
}