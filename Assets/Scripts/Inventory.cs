using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public GameObject itemsToBuyContainerBeforePurchase;
    public GameObject itemsToBuyContainerAfterPurchase;
    GameObject currentItemGameObject;

    public GameObject cashierText;

    public GameObject goodBatteryGO,
                        badBatteryGO,
                        goodBulbGO,
                        mediumBulbGO,
                        badBulbGO,
                        goodFoodGO,
                        mediumFoodGO,
                        badFoodGO,
                        plantSeedGO;

    public TextMeshProUGUI moneyLeft;
    public TextMeshProUGUI invItemCost;
    public TextMeshProUGUI invItems;
    public TextMeshProUGUI invMoney;

    public GameObject centerScreenItemCostTextGO;
    TextMeshProUGUI centerScreenItemCostText;

    public CanvasGroup InventoryGroup;

    public float moneyInWallet = 100f;

    public float goodBatteryCost,
                    badBatteryCost,
                    goodBulbCost,
                    mediumBulbCost,
                    badBulbCost,
                    goodFoodCost,
                    mediumFoodCost,
                    badFoodCost,
                    plantSeedCost;

    float batteryCost = 0f,
            bulbCost = 0f,
            foodCost = 0f,
            seedCost = 0f;

    string hoverItemText = "";
    string hoverItemTag;
    string goodnessLevel = "";

    string battName = "";
    string foodName = "";
    string bulbName = "";
    string seedName = "";

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            invItems.text = "";
            moneyLeft.text = $"{moneyInWallet} kr.";

            centerScreenItemCostText = centerScreenItemCostTextGO.GetComponent<TextMeshProUGUI>();
            centerScreenItemCostText.text = "";
            UpdateInventoryText();

            cashierText.SetActive(false);

            itemsToBuyContainerAfterPurchase.SetActive(false);

            InventoryGroup.alpha = 0;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;

        ShowInventory(hoverItemTag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;

        GameController._instance.hoverItemText.text = "";
        cashierText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;

        hoverItemText = collision.gameObject.name;
        hoverItemTag = collision.gameObject.tag;
        currentItemGameObject = collision.gameObject;
        centerScreenItemCostText.text = ItemCostScreenText();
    }

    public void ShowInventory()
    {
        InventoryGroup.alpha = 1;
    }

    public void HideInventory()
    {
        InventoryGroup.alpha = 0;
    }
    string ItemCostScreenText()
    {
        string screenText = "";
        switch (currentItemGameObject.GetComponent<Interactable>().storeItemType)
        {
            case Interactable.StoreItemType.None:
                break;

            case Interactable.StoreItemType.Battery:

                switch (AssignGoodnessValue())
                {
                    case "Good": screenText = goodBatteryCost.ToString() + " kr."; break;
                    case "Bad": screenText = badBatteryCost.ToString() + " kr."; break;
                }
                break;

            case Interactable.StoreItemType.Bulb: // Når typen er ELPÆRE

                switch (AssignGoodnessValue())
                {
                    case "Good": screenText = goodBulbCost.ToString() + " kr."; break;
                    case "Medium": screenText = mediumBulbCost.ToString() + " kr."; break;
                    case "Bad": screenText = badBulbCost.ToString() + " kr."; break;
                }
                break;

            case Interactable.StoreItemType.Food: // Når typen er MAD

                switch (AssignGoodnessValue())
                {
                    case "Good": screenText = goodFoodCost.ToString() + " kr."; break;
                    case "Medium": screenText = mediumFoodCost.ToString() + " kr."; break;
                    case "Bad": screenText = badFoodCost.ToString() + " kr."; break;
                }
                break;

            case Interactable.StoreItemType.Plantseed:  // Når typen er PLANTEFRØ
                screenText = plantSeedCost.ToString() + " kr.";
                break;

            default:
                break;
        }

        return screenText;
    }

    public void AddToShoppingList()
    {
        SoundController.instance.PlaySound(SoundController.instance.SFXGrabItem);

        switch (currentItemGameObject.GetComponent<Interactable>().storeItemType)
        {
            case Interactable.StoreItemType.None:
                break;

            case Interactable.StoreItemType.Battery:

                battName = hoverItemText;

                switch (AssignGoodnessValue())
                {
                    case "Good":    // Når det GODE BATTERI vælges
                        GameController._instance.batteryGood = true; //bools ændres
                        GameController._instance.batteryBad = false;

                        goodBatteryGO.SetActive(false); //gameObjects aktiveres/deaktiveres
                        badBatteryGO.SetActive(true);

                        batteryCost = goodBatteryCost;
                        break;

                    case "Bad":     // Når det DÅRLIGE BATTERI vælges
                        GameController._instance.batteryGood = false;
                        GameController._instance.batteryBad = true;

                        goodBatteryGO.SetActive(true);
                        badBatteryGO.SetActive(false);

                        batteryCost = badBatteryCost;
                        break;
                }

                UpdateInventoryText();
                break;

            case Interactable.StoreItemType.Bulb:

                bulbName = hoverItemText;

                switch (AssignGoodnessValue())
                {
                    case "Good":    // Når den GODE ELPÆRE vælges
                        GameController._instance.bulbGood = true;
                        GameController._instance.bulbMedium = false;
                        GameController._instance.bulbBad = false;

                        goodBulbGO.SetActive(false);
                        mediumBulbGO.SetActive(true);
                        badBulbGO.SetActive(true);

                        bulbCost = goodBulbCost;
                        break;

                    case "Medium":  // Når den MELLEMSTE ELPÆRE vælges
                        GameController._instance.bulbGood = false;
                        GameController._instance.bulbMedium = true;
                        GameController._instance.bulbBad = false;

                        goodBulbGO.SetActive(true);
                        mediumBulbGO.SetActive(false);
                        badBulbGO.SetActive(true);

                        bulbCost = mediumBulbCost;
                        break;

                    case "Bad":     // Når den DÅRLIGE ELPÆRE vælges
                        GameController._instance.bulbGood = false;
                        GameController._instance.bulbMedium = false;
                        GameController._instance.bulbBad = true;

                        goodBulbGO.SetActive(true);
                        mediumBulbGO.SetActive(true);
                        badBulbGO.SetActive(false);

                        bulbCost = badBulbCost;
                        break;
                }

                UpdateInventoryText();
                break;

            case Interactable.StoreItemType.Food:

                foodName = hoverItemText;

                switch (AssignGoodnessValue())
                {
                    case "Good":    // Når den GODE MAD vælges
                        GameController._instance.foodGood = true;
                        GameController._instance.foodMedium = false;
                        GameController._instance.foodBad = false;

                        goodFoodGO.SetActive(false);
                        mediumFoodGO.SetActive(true);
                        badFoodGO.SetActive(true);

                        foodCost = goodFoodCost;
                        break;

                    case "Medium":  // når den MEDIUM MAD vælges
                        GameController._instance.foodGood = false;
                        GameController._instance.foodMedium = true;
                        GameController._instance.foodBad = false;

                        goodFoodGO.SetActive(true);
                        mediumFoodGO.SetActive(false);
                        badFoodGO.SetActive(true);

                        foodCost = mediumFoodCost;
                        break;

                    case "Bad":     // Når den DÅRLIGE MAD vælges
                        GameController._instance.foodGood = false;
                        GameController._instance.foodMedium = false;
                        GameController._instance.foodBad = true;

                        goodFoodGO.SetActive(true);
                        badFoodGO.SetActive(false);
                        mediumFoodGO.SetActive(true);

                        foodCost = badFoodCost;
                        break;
                }

                UpdateInventoryText();
                break;

            case Interactable.StoreItemType.Plantseed:  // Når PLANTEFRØENE vælges
                GameController._instance.plantSeed = true;

                seedName = hoverItemText;
                seedCost = plantSeedCost;

                plantSeedGO.SetActive(false);

                UpdateInventoryText();
                break;

            default:
                break;
        }
    }

    string ShopItemList()
    {
        string shoppingList = invItems.text = "";

        if (battName != "")
            shoppingList += $"{battName}\n";

        if (bulbName != "")
            shoppingList += $"{bulbName}\n";

        if (foodName != "")
            shoppingList += $"{foodName}\n";

        if (seedName != "")
            shoppingList += $"{seedName}\n";

        return shoppingList;
    }

    public void UpdateInventoryText()
    {
        invItems.text = ShopItemList();
        invItemCost.text = CalculateItemCost();
        GameController._instance.hoverItemText.text = "";
        moneyLeft.text = $"{moneyInWallet} kr";
    }

    string CalculateItemCost()
    {
        if (bulbCost + foodCost + seedCost + batteryCost > 0)
        {
            return $"-{bulbCost + foodCost + seedCost + batteryCost} kr";
        }
        else
        {
            return "";
        }
    }

    void ShowInventory(string collisionTag)
    {
        if (collisionTag == "ShowInventory")
        {
            invMoney.text = moneyInWallet.ToString() + " kr";

            if (gameObject.transform.position.x > 4.2f)
            {
                InventoryGroup.alpha = 1;
            }
            else
            {
                InventoryGroup.alpha = 0;
            }
        }
    }

    string AssignGoodnessValue()
    {
        goodnessLevel = "";

        switch (currentItemGameObject.GetComponent<Interactable>().badNessLevel)
        {
            case Interactable.BadNessLevel.None: break;
            case Interactable.BadNessLevel.Good: goodnessLevel = "Good"; break;
            case Interactable.BadNessLevel.Medium: goodnessLevel = "Medium"; break;
            case Interactable.BadNessLevel.Bad: goodnessLevel = "Bad"; break;
            default:
                break;
        }
        return goodnessLevel;
    }

    void CalculateEcoPoints()
    {
        int tempEcoPoints = 0;

        if (GameController._instance.batteryGood == true) tempEcoPoints += 3;
        if (GameController._instance.batteryBad == true) tempEcoPoints -= 2;

        if (GameController._instance.bulbGood == true) tempEcoPoints += 3;
        if (GameController._instance.bulbMedium == true) tempEcoPoints += 1;
        if (GameController._instance.bulbBad == true) tempEcoPoints -= 2;

        if (GameController._instance.foodGood == true) tempEcoPoints += 3;
        if (GameController._instance.foodMedium == true) tempEcoPoints += 1;
        if (GameController._instance.foodBad == true) tempEcoPoints -= 2;

        if (GameController._instance.plantSeed == true) tempEcoPoints += 3;

        if (tempEcoPoints > 0)
        {
            GameController._instance.AddEcoPoints(tempEcoPoints);
        }
        else if (tempEcoPoints < 0)
        {
            GameController._instance.SubtractEcoPoints(tempEcoPoints);
        }
        else
        {
            Debug.Log("No change in EcoPoints.");
        }
    }

    public void CashierTextInteraction()
    {
        if (GameController._instance.varerBetalt == true)
        {
            cashierText.GetComponent<TextMeshPro>().text = "Ka' du så smutte med dig!";
            cashierText.SetActive(true);

            SoundController.instance.PlaySound(SoundController.instance.SFXGrabItem);

        }
        else if (hoverItemTag == "Cashier" &&                                                                                // hvis 'tag' er Cahsier, altså hvis det er kassedamen der interageres med
                 (GameController._instance.batteryBad == true || GameController._instance.batteryGood == true) &&                                // OG hvis mindst EN af batterierne er true, altså lagt i kurven
                 (GameController._instance.foodGood == true || GameController._instance.foodMedium == true || GameController._instance.foodBad == true) && // OG mindst EN af food'sne er true
                 (GameController._instance.bulbGood == true || GameController._instance.bulbMedium == true || GameController._instance.bulbBad == true)    // OG mindst EN af bulbsne er true
                )                                                                                                            // dvs man skal have valgt mindst en af hver type vare (minus plantefrø) for at kunne betale
        {
            moneyInWallet -= bulbCost + foodCost + seedCost + batteryCost;
            UpdateInventoryText();      // efter at have regnet en ny mængde penge opdateres inventory text
            invItemCost.text = "";      // samlede pris for varer fjernes, da der nu er betalt

            CalculateEcoPoints();       // efter at have betalt regnes der point ift valg af varer

            GameController._instance.varerBetalt = true;         // bool sættes true, så der ikke kan betales uendeligt
            GameController._instance.curMonologID = 2; //Hermit monolog frem

            cashierText.GetComponent<TextMeshPro>().text = "Hav en god dag!";
            cashierText.SetActive(true);

            itemsToBuyContainerBeforePurchase.SetActive(false); // items der kan interageres med, deaktivers
            itemsToBuyContainerAfterPurchase.SetActive(true);   // items der ikke kan interageres med, aktiveres

            SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
        }
        else
        {
            cashierText.GetComponent<TextMeshPro>().text = CashierForgotLine();
            cashierText.SetActive(true);
            SoundController.instance.PlaySound(SoundController.instance.SFXGrabItem);
        }
    }

    string CashierForgotLine()
    {
        bool forgotFood = false;
        bool forgotBulb = false;
        bool forgotBatt = false;

        if (GameController._instance.batteryBad == false && GameController._instance.batteryGood == false)
            forgotBatt = true;

        if (GameController._instance.foodGood == false && GameController._instance.foodMedium == false && GameController._instance.foodBad == false)
            forgotFood = true;

        if (GameController._instance.bulbGood == false && GameController._instance.bulbMedium == false && GameController._instance.bulbBad == false)
            forgotBulb = true;

        if (forgotBatt && forgotBulb && forgotFood)
        {
            return "Hej! Du kan betale ved mig, når du har valgt alle dine varer!";
        }
        else if (forgotBatt && forgotBulb)
        {
            return "Du mangler batterier og elpærer!";
        }
        else if (forgotBatt && forgotFood)
        {
            return "Du mangler batterier og mad!";
        }
        else if (forgotBulb && forgotFood)
        {
            return "Du mangler elpærer og mad!";
        }
        else if (forgotBulb)
        {
            return "Du mangler elpærer!";
        }
        else if (forgotFood)
        {
            return "Du mangler mad!";
        }
        else if (forgotBatt)
        {
            return "Du mangler batterier!";
        }
        else
        {
            return "Wat.";
        }
    }
}