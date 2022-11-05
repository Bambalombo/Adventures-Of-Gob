using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player_Movement player_Movement;
    private Animator animator;

    private bool isPlayingLandAnim;
    private float landAnimTimer = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player_Movement = GetComponent<Player_Movement>();
    }

    void Update()
    {
        if (player_Movement.goingLeft) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

        animator.SetFloat("speed", Mathf.Abs(player_Movement.movevector.x));

        //if (player_Movement.isJumping && !player_Movement.isGrounded && player_Movement.distToGround < 1 && player_Movement.differenceY > 0 && !isPlayingLandAnim)
        //{

        //    isPlayingLandAnim = true;
        //}

        if (player_Movement.isJumping)
        {
            animator.SetBool("jumping", true);
        }
        else
            animator.SetBool("jumping", false);

        if (isPlayingLandAnim)
        {
            landAnimTimer -= Time.deltaTime;
            if (landAnimTimer < 0)
            {
                landAnimTimer = 0.5f;
                isPlayingLandAnim = false;
            }
        }
    }
}
