using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : MonoBehaviour
{
    public LampBulbChanger[] lampBulbChangers;

    void Start()
    {
        lampBulbChangers = GameObject.FindObjectsOfType<LampBulbChanger>();
    }

    public void SetLamps()
    {
        foreach (LampBulbChanger lampBulbChanger in lampBulbChangers)
        {
            if (GameController._instance.bulbBad)
            {
                lampBulbChanger.SetBulb_Bad();

                if (lampBulbChanger.is2050Bulb)
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Off;
                else
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Bad;
            }
            else if (GameController._instance.bulbMedium)
            {
                lampBulbChanger.SetBulb_Medium();

                if (lampBulbChanger.is2050Bulb)
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Bad;
                else
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Medium;
            }
            else if (GameController._instance.bulbGood)
            {
                lampBulbChanger.SetBulb_Good();

                if (lampBulbChanger.is2050Bulb)
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Good;
                else
                    lampBulbChanger.GetComponentInChildren<LightMaskFlicker>().currentLightStrength = LightMaskFlicker.LightStrength.Good;
            }
        }
    }
}