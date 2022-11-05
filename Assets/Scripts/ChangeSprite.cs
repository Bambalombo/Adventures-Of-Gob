using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    private Sprite oldSprite;
    public Sprite spriteToChangeTo;
    bool oldSpriteActive = true;

    private void Start()
    {
        oldSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Change()
    {
        GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
    }

    public void ChangeBack()
    {
        GetComponent<SpriteRenderer>().sprite = oldSprite;
    }

    public void ToggleSprite()
    {
        if (oldSpriteActive)
        {
            GetComponent<SpriteRenderer>().sprite = spriteToChangeTo;
            oldSpriteActive = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = oldSprite;
            oldSpriteActive = true;
        }
    }
}