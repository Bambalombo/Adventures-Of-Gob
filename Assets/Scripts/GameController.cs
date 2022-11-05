using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _instance;

    public Interactable.PickUpItem heldItem;
    public GameObject heldItemGameObject;

    public GameObject inventoryUiElements;
    public GameObject hoverItemGO;
    public TextMeshProUGUI hoverItemText;

    //for moving the player of things if teleported under it.
    public bool justTeleported;
    private float teleportedResetTime = 0.05f, teleportedResetTimeOriginal = 0.05f;

    //Thought bubbles
    public GameObject thougtBubble;

    public int ecoPoints, maxPointsObtainable = 48;

    public bool gotKeys;
    public bool varerBetalt;
    public bool lightsOff, plantedTree, bikeDriven, carDriven, solarPanelPlanted, potplantPlanted;

    public bool
         batteryGood,
         batteryBad,
         bulbGood,
         bulbMedium,
         bulbBad,
         foodGood,
         foodMedium,
         foodBad,
         plantSeed;

    [Header("current monolog ID")]
    public int curMonologID = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //ødelæg ikke dette object. Derved får man én GameController
            DontDestroyOnLoad(gameObject);
        }
        else//hvis der findes en _instance allerede, ødelæg den. Derved forbliver den første // lidt usikker på flowet af koden though..
            Destroy(gameObject);
    }

    private void Start()
    {
        hoverItemGO = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        hoverItemText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            if (inventoryUiElements != null)
                inventoryUiElements.SetActive(false);
        }
        else
        {
            if (inventoryUiElements != null)
                inventoryUiElements.SetActive(true);
        }
    }

    private void Update()
    {
        if(justTeleported)
        {
            teleportedResetTime -= Time.deltaTime;
            if (teleportedResetTime <= 0)
            {
                justTeleported = false;
                teleportedResetTime = teleportedResetTimeOriginal; 
            }
        }
    }

    public void SubtractEcoPoints(int numToSubstact)
    {
        if (_instance.ecoPoints < 0) // hvis mindre end 0 s?ttes lig med 0
            _instance.ecoPoints = 0;

        if (_instance.ecoPoints > 0) // man skal ikke kunne g? i minus point.
        {
            _instance.ecoPoints -= numToSubstact;
            Debug.Log("Subtracted: " + numToSubstact + ". Total: " + _instance.ecoPoints + " out of " + _instance.maxPointsObtainable + " points.");
        }
        else
        {
            Debug.Log("Can't go below 0 EcoPoints.");
        }
    }

    public void AddEcoPoints(int numToAdd)
    {
        if (_instance.ecoPoints < _instance.maxPointsObtainable) // kan ikke g? over max. Det fucker med baggrundsfarven. Der kan kun tilf?jes hvis den er under max
        {
            _instance.ecoPoints += numToAdd;
            Debug.Log("Added: " + numToAdd + ". Total: " + _instance.ecoPoints);

            if (_instance.ecoPoints > _instance.maxPointsObtainable) // failsafe, hvis den g?r over max alligevel
            {
                _instance.ecoPoints = _instance.maxPointsObtainable;
                Debug.Log("EcoPoints went above max. Set to max instead. Beware.");
            }
        }
        else
        {
            Debug.Log("Max EcoPoints reached.");
        }
    }
}