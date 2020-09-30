using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public static bool goingUp = true;
    bool inPlatform = false;
    public GameObject swordObject;
    Animator swordAnimator;
    BoxCollider2D swordHitbox;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        swordAnimator = swordObject.GetComponent<Animator>();
        swordHitbox = swordObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        UpdateSwitch();
        animator.SetInteger("animWalkSwitch", horInputSwitch);
        transform.Translate(horInput*Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwingSword();
        }

        if (inPlatform == false) UpdateGoingUp();


        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Space))
        {
            goingUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            goingUp = true;
            Jump();  
        }
        else if (Input.GetKeyDown(KeyCode.Space) && dblJump)
        {
            goingUp = true;
            Jump();
            dblJump = false;
        }
        else;

        if (GameManager.canExit == true && Input.GetKeyDown(KeyCode.E) && GameManager.enemyCount == 0) Debug.Log("YOU WIN");
        
    }

    void SwingSword()
    {
        swordAnimator.Play("SwordSwing");

    }
    void UpdateGoingUp()
    {
        if (playerRB.velocity.y <= 0) goingUp = false;
        else goingUp = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal")) GameManager.canExit = true;
        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal")) GameManager.canExit = false;
        if (collision.gameObject.CompareTag("Platform"))
        {
            inPlatform = false;

        }
    }



}
