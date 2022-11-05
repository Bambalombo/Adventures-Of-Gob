using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndGameController : MonoBehaviour
{
    public Image photoPrefab;
    [Header("position to spawn at:")]
    public Transform photoPos;

    public Sprite[] photoSprites;

    public List<GameObject> spawnedPhotos;
    public TextMeshProUGUI textBobleText;

    public int SituationID = 0;
    public int points;

    public bool EndDone;
    public Slider ProgressSlider;

    public CanvasGroup buttonsGroup;

    private void Start()
    {
        ProgressSlider.value = 0;
        buttonsGroup.alpha = 0;
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;

        Debug.Log("Time.deltaTime = " + Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!EndDone)
                NextSituation();
            else
                SetEndScreen();
        }
        Debug.Log("Time.deltaTime = " + Time.deltaTime);

    }

    private void UpdateSlider()
    {
        float value = (float)points / 44f;
        Debug.Log("value is" + value + " points is" + points);
        ProgressSlider.value = value;
    }

    private void NextSituation()
    {
        SituationID++;
        ShowScenarioChoice();
    }

    private void SpawnPhoto(int photoID)
    {
        InstantiatePhoto(photoSprites[photoID]);
    }

    public void InstantiatePhoto(Sprite sprite)
    {
        GameObject Photo = Instantiate(Resources.Load("PhotoPrefab"), photoPos.position + new Vector3(Random.Range(-45, 45), Random.Range(-45, 45), 0), Quaternion.identity * Quaternion.Euler(0, 0, Random.Range(-15, 15)), photoPos) as GameObject;
        Photo.GetComponent<Image>().sprite = sprite;
        spawnedPhotos.Add(Photo.gameObject);
    }

    private void SetEndScreen()
    {
        textBobleText.text = "Godt gået!\n";
        textBobleText.text += "Du gennemførte spillet med " + points + " ud af " + 44 + " point!\n";

        buttonsGroup.alpha = 1;
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ShowScenarioChoice()
    {
        textBobleText.text = "Du endte med ";

        switch (SituationID)
        {
            case 1:
                if (GameController._instance.lightsOff)
                {
                    textBobleText.text += "at slukke lyset!\n";
                    textBobleText.text += "+5 point";

                    points += 5;

                    SpawnPhoto(0);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else
                {
                    textBobleText.text += "ikke at slukke lyset.";

                    points += 0;

                    SpawnPhoto(1);
                }
                break;

            case 2:
                if (GameController._instance.plantedTree)
                {
                    textBobleText.text += "at plante træet!\n";
                    textBobleText.text += "+4 point";
                    points += 4;

                    SpawnPhoto(3);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else
                {
                    textBobleText.text += "ikke at plante træet.\n";
                    textBobleText.text += "0 point";

                    points += 0;

                    SpawnPhoto(2);
                }
                break;

            case 3:
                if (GameController._instance.solarPanelPlanted)
                {
                    textBobleText.text += "sætte et solpanel på taget\n";
                    textBobleText.text += "+7 point";

                    points += 7;

                    SpawnPhoto(5);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else
                {
                    textBobleText.text += "ikke at sætte et solpanel på taget.";
                    textBobleText.text += "0 point";

                    points += 0;

                    SpawnPhoto(4);
                }
                break;

            case 4:
                if (GameController._instance.bikeDriven)
                {
                    textBobleText.text += "at køre på cyklen\n";
                    textBobleText.text += "+10 point";

                    points += 10;
                    SpawnPhoto(6);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else if (GameController._instance.carDriven)
                {
                    textBobleText.text += "at køre i bilen.\n";
                    textBobleText.text += "0 point";

                    SpawnPhoto(7);
                }
                break;

            case 5:
                if (GameController._instance.batteryGood)
                {
                    textBobleText.text += "at købe et genopladeligt batteri!\n";
                    textBobleText.text += "+3 point";

                    points += 3;

                    SpawnPhoto(9);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else if (GameController._instance.batteryBad)
                {
                    textBobleText.text += "at købe et almindeligt batteri.\n";
                    textBobleText.text += "-2 point";

                    points -= 2;

                    SpawnPhoto(8);
                }
                break;

            case 6:
                if (GameController._instance.bulbGood)
                {
                    textBobleText.text += "at købe en LED pære!\n";
                    textBobleText.text += "+3 point";

                    points += 3;

                    SpawnPhoto(11);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else if (GameController._instance.bulbMedium)
                {
                    textBobleText.text += "at købe en sparepære\n";
                    textBobleText.text += "+1 point";

                    points += 1;

                    SpawnPhoto(12);
                }
                else if (GameController._instance.bulbBad)
                {
                    textBobleText.text += "at købe en glødepære.\n";
                    textBobleText.text += "-2 point";

                    points -= 2;

                    SpawnPhoto(10);
                }
                break;

            case 7:
                if (GameController._instance.foodGood)
                {
                    textBobleText.text += "at købe frugt!\n";
                    textBobleText.text += "+3 point";

                    points += 3;

                    SpawnPhoto(13);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else if (GameController._instance.foodMedium)
                {
                    textBobleText.text += "at købe en kylling\n";
                    textBobleText.text += "+1 point";

                    points += 1;

                    SpawnPhoto(14);

                }
                else if (GameController._instance.foodBad)
                {
                    textBobleText.text += "at købe en øksebøf.\n";
                    textBobleText.text += "-2 point";

                    points -= 2;

                    SpawnPhoto(15);
                }
                break;

            case 8:
                if (GameController._instance.plantSeed)
                {
                    textBobleText.text += "at købe en pose plantefrø!\n";
                    textBobleText.text += "+3 point";

                    points += 3;

                    SpawnPhoto(16);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                break;

            case 9:
                if (GameController._instance.potplantPlanted)
                {
                    textBobleText.text += "plante frøene\n";
                    textBobleText.text += "+6 point";

                    points += 6;

                    SpawnPhoto(18);

                    SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
                }
                else
                {
                    textBobleText.text += "ikke at plante frøene\n";
                    textBobleText.text += "+0 point";

                    SpawnPhoto(17);
                }

                break;

            case 10:
                EndDone = true;
                SetEndScreen();

                //textBobleText.text += "...\n!";

                break;
        }

        UpdateSlider();
    }
}