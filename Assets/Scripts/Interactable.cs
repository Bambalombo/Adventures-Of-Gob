using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    public Inventory inventory;
    public Interactions interactions;
    public ChangeSprite[] changeSprites;
    private Color startColor;
    public enum PickUpItem { None, Acorn, Ladder, solarpanel }
    public Interactable.PickUpItem pickUpItem;
    public enum InteractItem { None, DirtMould, WaterEdge, roof }
    public Interactable.InteractItem interactItem;

    public enum StoreItemType { None, Battery, Bulb, Food, Plantseed }
    [Header("Supermarket Categories")]
    public Interactable.StoreItemType storeItemType;
    public enum BadNessLevel { None, Good, Medium, Bad }
    public Interactable.BadNessLevel badNessLevel;

    public bool pickUpAllowed, isAttachedToPlayer, isInteractable = true;
    public bool isBeingHighlighted;

    [Header("Check if shop item")]
    public bool CanAddToShoppingList;

    [Header("Objects to Toggle on/off on interact")]
    public GameObject[] objectArray;

    public GameObject thisItem;
    public string itemName;

    private void Start()
    {
        Invoke(nameof(InvokedStart), 0.2f);
    }

    private void InvokedStart()
    {
        thisItem = gameObject;
        itemName = thisItem.name;

        GameController._instance.hoverItemText = GameController._instance.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        GameController._instance.hoverItemText.text = "";
        startColor = GetComponent<SpriteRenderer>().color;

        interactions = GetComponent<Interactions>();
        if (changeSprites.Length > 0)
            changeSprites[0] = GetComponent<ChangeSprite>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isBeingHighlighted && !isAttachedToPlayer && isInteractable && other.CompareTag("Player"))
            Highlight();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isBeingHighlighted && isInteractable)
        {
            if (CanAddToShoppingList)
            {
                inventory.CashierTextInteraction(); // Linus
                inventory.AddToShoppingList(); // også Linus
            }

            if (pickUpAllowed)
            {
                if (GameController._instance.heldItemGameObject == null)
                {
                    AttachToPlayer();

                    GameController._instance.heldItem = pickUpItem;
                    GameController._instance.heldItemGameObject = gameObject;
                    SoundController.instance.PlaySound(SoundController.instance.SFXGrabItem);
                }
                else
                {
                    DropItem();

                    AttachToPlayer();

                    GameController._instance.heldItem = pickUpItem;
                    GameController._instance.heldItemGameObject = gameObject;
                    SoundController.instance.PlaySound(SoundController.instance.SFXGrabItem);
                }
            }
            else
            {
                if (interactions != null)
                    interactions.DoInteraction(this, GameController._instance.heldItem, interactItem);
                else
                {
                    foreach (ChangeSprite changeSprite in changeSprites)
                        changeSprite.ToggleSprite();
                    for (int i = 0; i < objectArray.Length; i++)
                    {
                        if (objectArray[i].gameObject.activeSelf)
                        {
                            objectArray[i].SetActive(false);
                        }
                        else
                        {
                            objectArray[i].SetActive(true);
                        }

                        for (int j = 0; j < objectArray[i].transform.childCount; j++)
                        {
                            if (objectArray[i].transform.GetChild(j).gameObject.activeSelf)
                            {
                                objectArray[i].transform.GetChild(j).gameObject.SetActive(false);
                            }
                            else
                            {
                                objectArray[i].transform.GetChild(j).gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInteractable)
        {
            UnHighlight();
        }
    }
    private void AttachToPlayer()
    {
        isAttachedToPlayer = true;
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void DropItem()
    {
        GameController._instance.heldItemGameObject.transform.position = transform.position;
        GameController._instance.heldItemGameObject.transform.SetParent(null);
        GameController._instance.heldItemGameObject.GetComponent<Interactable>().isAttachedToPlayer = false;
    }
    private void Highlight()
    {
        isBeingHighlighted = true;
        GetComponent<SpriteRenderer>().material.color += new Color32(40, 40, 40, 255);
        //SoundController.instance.PlaySound(SoundController.instance.highlightObject);

        if (gameObject.name.Contains("*") != true)
        {
            GameController._instance.hoverItemText.text = gameObject.name;
            GameController._instance.hoverItemGO.SetActive(true);

            /*if (onScreenTextGO != null)
            {
                onScreenTextGO.SetActive(false);
            }
            else
            {
                onScreenTextGO = gameController.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
                Debug.Log("itemText was == null. Set item text and found the GameObject: " + onScreenTextGO);
                onScreenTextGO.SetActive(true);
            }*/

            if (tag == "StoreItem")
            {
                inventory.centerScreenItemCostTextGO.SetActive(true);
            }
        }
    }
    public void UnHighlight()
    {
        isBeingHighlighted = false;
        GetComponent<SpriteRenderer>().material.color = startColor;

        GameController._instance.hoverItemText.text = "";

        if (gameObject.tag == "StoreItem")
            inventory.centerScreenItemCostTextGO.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void SetNotInteractable()
    {
        isInteractable = false;
        UnHighlight();
    }
}