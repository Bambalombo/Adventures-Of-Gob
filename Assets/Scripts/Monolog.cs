using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class Monolog : MonoBehaviour
{
    GameController gameController;

    private TextMeshProUGUI textBox;
    private Camera camera;
    public GameObject pressEImageGM;

    public TextAsset[] textFiles;

    [Header("current text file:")]
    public TextAsset monolog;

    [Header("Seperated Lines:")]
    public string[] listOfLines;

    [Header("Text Box placement correction:")]
    public Vector3 plusVector = new Vector3(0, 50, 0);
    [Header("Current line ID:")]
    public int lineID;
    [Header("TextIsVisible:")]
    public bool textVisible;

    [Header("Things to happen after end of monologue")]
    public bool getKeys;

    public float textJigglePower = 40;
    private Vector3 textOriginalPosition;

    void Start()
    {
        gameController = GameController._instance;

        camera = Camera.main;
        GameObject instantiatedTextBox = Instantiate(Resources.Load("UI_TextBox"), GameObject.FindObjectOfType<Canvas>().transform) as GameObject;
        textBox = instantiatedTextBox.GetComponent<TextMeshProUGUI>();

        pressEImageGM = Instantiate(Resources.Load("pressEImageGM"), transform.position + new Vector3(2.5f, 2.5f, 0), Quaternion.identity) as GameObject;

        pressEImageGM.SetActive(false);

        SetCurrentTextFile(GameController._instance.curMonologID);
        textOriginalPosition = textBox.transform.localPosition;
        HideTextBox();
    }

    private void OnEnable()
    {
        SetCurrentTextFile(gameController.curMonologID);
    }

    void Update()
    {
        if (textBox != null)
            textBox.rectTransform.position = camera.WorldToScreenPoint(transform.position) + plusVector;

        if (Input.GetKeyDown(KeyCode.E) && textVisible)
        {
            DisplayNextLine();
        }

        if (textJigglePower > 0 && textVisible)
        {
            textJigglePower -= Time.deltaTime * 3 * textJigglePower / 2;

            if(textJigglePower>9)
                if (textBox != null)
                {
                    textBox.rectTransform.position = camera.WorldToScreenPoint(transform.position) + plusVector + new Vector3(0, (float)Math.Sin(textJigglePower) * (1 * textJigglePower / 12), 0);
                }
                else
                {
                    GameObject instantiatedTextBox = Instantiate(Resources.Load("UI_TextBox"), GameObject.FindObjectOfType<Canvas>().transform) as GameObject;
                    textBox = instantiatedTextBox.GetComponent<TextMeshProUGUI>();
                    textBox.rectTransform.position = camera.WorldToScreenPoint(transform.position) + plusVector + new Vector3(0, (float)Math.Sin(textJigglePower) * (1 * textJigglePower / 12), 0);
                }
        }
    }

    private void ShowTextBox()
    {
        textBox.text = listOfLines[lineID];
        pressEImageGM.SetActive(true);
    }

    private void HideTextBox()
    {
        textBox.text = "";
        pressEImageGM.SetActive(false);
    }
    private void DisplayNextLine()
    {
        lineID++;

        // lineID = lineID >= listOfLines.Length ? lineID = 0 : lineID = lineID;
        //Value=  condition                   ?   true          : false

        //^ ternary operator - pretty cool for single value checks and assignments
        textJigglePower = 20;

        if (lineID >= listOfLines.Length)
        {
            lineID = 0;
            HideTextBox();
            SetConditions();
            CheckForLights();
        }

        if (lineID > 0)
            textBox.text = listOfLines[lineID];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CancelInvoke();
        ShowTextBox();
        textVisible = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke(nameof(HideTextBox), 1);
        textVisible = false;
    }

    private void CheckForLights()
    {
        if (GameController._instance.varerBetalt)
        {
            GameObject.FindObjectOfType<LampSwitch>().SetLamps();
            SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
            GameObject Torch = Instantiate(Resources.Load("Torch"), GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.3f, 0.3f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform) as GameObject;
            Torch.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }

    private void SetConditions()
    {
        if (getKeys && !GameController._instance.gotKeys)
        {
            GameController._instance.gotKeys = true;
            GameObject spawnedKeys = Instantiate(Resources.Load("Keys"), GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.7f, 0.3f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform) as GameObject;
            spawnedKeys.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
        }
    }

    public void SetCurrentTextFile(int ID)
    {
        monolog = textFiles[ID];
        listOfLines = monolog.text.Split(';');
    }
}