using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ThoughtBubble : MonoBehaviour
{
    public string message;

    void Start()
    {
        Invoke(nameof(SetBubbleText), 0.4f);
        SetBubbleText(message);
        Invoke(nameof(DestroyMe), 1.6f);

        //instantiated object not running animations!! waat
        GetComponentInChildren<Animator>().Rebind();

        GetComponentInChildren<Animator>().SetTrigger("play");

        string startAnimationState = "StartState";
        GetComponentInChildren<Animator>().Play(startAnimationState, -1, 0);
    }

    void Update()
    {
        transform.position += new Vector3(0, 0.001f, 0);
        transform.localScale += new Vector3(0.002f, 0.002f, 0);
    }

    public void SetBubbleText(string message)
    {
        GetComponentInChildren<TextMeshPro>().text = message;
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}