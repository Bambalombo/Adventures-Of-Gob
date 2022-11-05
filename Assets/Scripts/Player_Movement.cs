using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public bool goingLeft;
    public bool standingStill;

    [Min(0)]
    [Header("Player Settings:")]
    public float movementSpeed = 5;
    [Min(0)]
    public float jumpStrength = 10;

    private float oldY, newY;
    public float differenceY;
    public float distToGround;
    public bool isJumping, isGrounded = true;
    public Vector3 movevector;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerBody.freezeRotation = true;
        playerBody.gravityScale = 3;
    }

    void Update()
    {
        movevector = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * movementSpeed * Time.deltaTime;

        if (movevector.x < 0)
        {
            goingLeft = true;
            standingStill = false;
        }
        else if (movevector.x > 0)
        {
            goingLeft = false;
            standingStill = false;
        }
        else
            standingStill = true;

        transform.Translate(movevector);
        {
            if (!isJumping && isGrounded && Input.GetKeyDown(KeyCode.W) || !isJumping && isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            DetectGround();
        }

        newY = transform.position.y;

        differenceY = (oldY - newY) / 2;
    }

    private void FixedUpdate()
    {
        oldY = transform.position.y;
    }

    private void Jump()
    {
        isJumping = true;
        isGrounded = false;
        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpStrength);
        SoundController.instance.PlaySound(SoundController.instance.jump);
    }

    private void DetectGround()
    {
        // Bit shift the index of the layer (8) (terrain) to get a bit mask // whut
        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            distToGround = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
        else
        {
            distToGround = 100;
        }

        if (Mathf.Abs(playerBody.velocity.y) < 0.1f && !isGrounded && distToGround < 1.1f)//Extra check..
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}