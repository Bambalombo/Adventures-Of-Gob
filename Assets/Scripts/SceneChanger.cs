using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public bool isOnTrigger;
    public enum ObjectType { Bike, Car, None }

    [Header("What is this?")]
    public ObjectType objectType;

    GameObject centerScreenItemText;

    [Header("Scene ID to change to:")]
    public int sceneID;

    [Header("FreeChange")]
    public bool justChangeIt;

    void Start()
    {
        centerScreenItemText = GameObject.Find("itemText");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnTrigger)
        {
            CheckConditions();
        }
    }

    private void CheckConditions()
    {
        switch (objectType)
        {
            case ObjectType.Bike:
                if (GameController._instance.gotKeys)
                {
                    GameController._instance.bikeDriven = true;
                    GameController._instance.carDriven = false;
                    ChangeScene();
                }
                else
                {
                    TryThoughtBubble("Jeg mangler\n en n?gle.");
                }
                break;

            case ObjectType.Car:
                if (GameController._instance.gotKeys)
                {
                    GameController._instance.bikeDriven = false;
                    GameController._instance.carDriven = true;
                    ChangeScene();
                }
                else
                {
                    TryThoughtBubble("Jeg mangler\n en n?gle.");
                }
                break;
        }

        if (justChangeIt)
            ChangeScene();

        if (SceneManager.GetActiveScene().buildIndex != 3)
            return;

        if (GameController._instance.varerBetalt)
        {
            ChangeScene();
        }
        else
        {
            centerScreenItemText.GetComponent<TextMeshProUGUI>().text = "";

            if (GameController._instance.batteryBad || GameController._instance.batteryGood || GameController._instance.foodBad || GameController._instance.foodMedium || GameController._instance.foodGood || GameController._instance.bulbBad || GameController._instance.bulbMedium || GameController._instance.bulbGood || GameController._instance.plantSeed)
            {
                TryThoughtBubble("Jeg mangler\n at betale.");
            }
            else
            {
                TryThoughtBubble("Jeg mangler\n at handle ind.");
            }
        }
    }

    private void TryThoughtBubble(string msg)
    {
        if (GameController._instance.thougtBubble == null)
        {
            GameObject ThoughtBubble = Instantiate(Resources.Load("ThoughtBubble"), GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity) as GameObject;
            ThoughtBubble.GetComponent<ThoughtBubble>().message = msg;
            GameController._instance.thougtBubble = ThoughtBubble;
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isOnTrigger = false;
    }
}