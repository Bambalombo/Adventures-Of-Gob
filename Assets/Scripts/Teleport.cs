using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    BackgroundColorAndSpriteChange bgChange;

    private bool tyik = true;
    private TimeTravelEffect timeTravelEffect;
    private GameObject BG_Bad, BG_Good, MG_Good, MG_Bad;

    // tyik er sand n?r man er i "paradis/ovenp?/nutid". Hvis den er sand n?r man trykker "E", s? -
    //tp'er man ned og s?tter tyik til false. Hvis tyik er false og man trykker "E", s? -
    // er man nedenunder og kommer op og s?tter tyik til true igen
    // En mulig ?ndring er at g?re det ud fra bestemt koordinat.
    // Feks hvis y > -20 s? tp ned & hvis y < -20 s? tp op

    private void Start()
    {
        bgChange = GameObject.FindObjectOfType<BackgroundColorAndSpriteChange>();
        timeTravelEffect = GameObject.FindObjectOfType<TimeTravelEffect>();

        BG_Good = GameObject.Find("Baggrund2021");
        BG_Bad = GameObject.Find("Baggrund2050");
        MG_Good = GameObject.Find("MellemGrund2021");
        MG_Bad = GameObject.Find("MellemGrund2050");

        if (SceneManager.GetActiveScene().buildIndex == 3 || (SceneManager.GetActiveScene().buildIndex == 4)) // ny linus
        {
            ChangeTime();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ChangeTime();
        }
    }

    void ChangeTime()
    {
        if (tyik)
        {
            transform.position += new Vector3(0, 15, 0);

            SetBGGood();
            timeTravelEffect.GoTo2021();
            tyik = false;
        }
        else
        {
            transform.position += new Vector3(0, -15, 0);

            bgChange.UpdateBackground();
            SetBGBad();
            timeTravelEffect.GoTo2050();
            tyik = true;
        }

        SoundController.instance.PlaySound(SoundController.instance.changeTimeSound);
        SpawnSmokeClouds();
        GameController._instance.justTeleported = true;
    }

    public void SetBGGood()
    {
        if (BG_Bad == null || MG_Bad == null || BG_Good == null || MG_Good == null)
        {
            BG_Good = GameObject.Find("Baggrund2021");
            BG_Bad = GameObject.Find("Baggrund2050");
            MG_Good = GameObject.Find("MellemGrund2021");
            MG_Bad = GameObject.Find("MellemGrund2050");

            BG_Bad.SetActive(false);
            MG_Bad.SetActive(false);

            BG_Good.SetActive(true);
            MG_Good.SetActive(true);
        }
        else
        {
            BG_Bad.SetActive(false);
            MG_Bad.SetActive(false);

            BG_Good.SetActive(true);
            MG_Good.SetActive(true);
        }
    }

    private void SpawnSmokeClouds()
    {
        GameObject Smokey = Instantiate(Resources.Load("SmokeCloud"), transform.position, Quaternion.identity) as GameObject;
        Smokey.transform.parent = transform;
        Destroy(Smokey, 5);
        GameObject Smokey2 = Instantiate(Resources.Load("SmokeCloud"), transform.position, Quaternion.identity) as GameObject;
        Smokey2.transform.localScale = new Vector3(2, 2, 1);
        Destroy(Smokey2, 5);
    }
    public void SetBGBad()
    {
        if (BG_Bad == null || MG_Bad == null || BG_Good == null || MG_Good == null)
        {
            BG_Good = GameObject.Find("Baggrund2021");
            BG_Bad = GameObject.Find("Baggrund2050");
            MG_Good = GameObject.Find("MellemGrund2021");
            MG_Bad = GameObject.Find("MellemGrund2050");

            BG_Bad.SetActive(true);
            MG_Bad.SetActive(true);

            BG_Good.SetActive(false);
            MG_Good.SetActive(false);
        }
        else
        {
            BG_Bad.SetActive(true);
            MG_Bad.SetActive(true);

            BG_Good.SetActive(false);
            MG_Good.SetActive(false);
        }
    }
}