using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterBalloon : MonoBehaviour
{
    private Interactable interactable;
    public Sprite froSprite;
    public GameObject objectToSetActive;

    public float speed;
    public bool playerOnBoard;
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable.isBeingHighlighted)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            player.transform.position = transform.position + new Vector3(0,1.5f,0);
            player.transform.parent = transform;
            player.GetComponent<Player_Movement>().enabled = false;
            player.GetComponent<Player_Animations>().enabled = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            Invoke(nameof(ChangeScene), 7);
            playerOnBoard = true;
            GetComponent<SpriteRenderer>().sortingOrder = 20;

            interactable.isInteractable = false;
            interactable.isBeingHighlighted = false;
            interactable.UnHighlight();
            Destroy(interactable);
        }
        if (playerOnBoard)
        {
            speed += Time.deltaTime/60;
            transform.position += new Vector3(speed/5, speed, 0);

        }
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(5);
    }
}
