using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMaskFlicker : MonoBehaviour
{
    public enum LightStrength { Good, Medium, Bad, Off }

    [Header("Set Light Strengh")]
    public LightStrength currentLightStrength;
    Vector3 startValue;

    public float sineWave;
    private float newScaler = 35;
    private float newSpeed = 15;
    public float sineValue;

    private SpriteMask[] spriteMasks;

    void Start()
    {
        spriteMasks = GetComponentsInChildren<SpriteMask>();
        startValue = transform.localScale;
        newSpeed += Random.Range(-5, 5);
    }

    void Update()
    {
        switch (currentLightStrength)
        {
            case LightStrength.Good:
                DoFlicker(1);
                break;

            case LightStrength.Medium:
                DoFlicker(0.5f);
                break;

            case LightStrength.Bad:
                DoFlicker(0.3f);
                break;

            case LightStrength.Off:
                TurnOff();
                break;
        }
    }

    private void DoFlicker(float sizeMultiplier)
    {
        sineValue += Time.deltaTime * newSpeed;

        foreach (SpriteMask spriteMask in spriteMasks)
        {
            sineWave = Mathf.Sin(sineValue);
            sineWave /= newScaler;

            spriteMask.transform.localScale = (startValue * sizeMultiplier) + new Vector3(sineWave, sineWave, 0);
        }
    }

    private void TurnOff()
    {
        foreach (SpriteMask spriteMask in spriteMasks)
        {
            spriteMask.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}