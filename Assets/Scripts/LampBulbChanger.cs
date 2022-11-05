using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBulbChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite off, good, medium, bad;
    public LightMaskFlicker lightMaskFlicker;

    public bool is2050Bulb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lightMaskFlicker = GetComponentInChildren<LightMaskFlicker>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetBulb_OFF();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetBulb_Good();
        }
    }

    public void SetBulb_OFF()
    {
        spriteRenderer.sprite = off;
        lightMaskFlicker.currentLightStrength = LightMaskFlicker.LightStrength.Off;
    }

    public void SetBulb_Good()
    {
        spriteRenderer.sprite = good;
        lightMaskFlicker.currentLightStrength = LightMaskFlicker.LightStrength.Good;
    }

    public void SetBulb_Medium()
    {
        spriteRenderer.sprite = medium;
        lightMaskFlicker.currentLightStrength = LightMaskFlicker.LightStrength.Medium;
    }

    public void SetBulb_Bad()
    {
        spriteRenderer.sprite = bad;
        lightMaskFlicker.currentLightStrength = LightMaskFlicker.LightStrength.Bad;
    }

}
