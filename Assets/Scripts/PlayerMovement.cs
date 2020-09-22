using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    float horInput = 0;
    Rigidbody2D playerRB;
    int horInputSwitch = 0;
    public int jumpHeight = 50;
    bool canJump = true;
    bool dblJump = true;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");

        if (isGrounded == true) UpdateSwitch();
        animator.SetInteger("animWalkSwitch", horInputSwitch);
        transform.Translate(horInputSwitch*Time.deltaTime, 0, 0);
        if (Input.GetKeyDown(KeyCode.Space) && canJump) Jump();
        else if (Input.GetKeyDown(KeyCode.Space) && dblJump)
        {
            Jump();
            dblJump = false;
        }
        else;
        
    }

    void UpdateSwitch()
    {
        if (horInput < 0) horInputSwitch = -1;
        if (horInput == 0) horInputSwitch = 0;
        if (horInput > 0) horInputSwitch = 1;
    }

    private void FixedUpdate()
    {
        
    }

    void Jump()
    {
        UpdateSwitch();
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        playerRB.AddForce(new Vector2(horInputSwitch, jumpHeight));
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        dblJump = true;
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
