using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject rightSword;
    public GameObject leftSword;
    Animator swordAnimator;
    BoxCollider2D swordHitbox;
    Animator leftswordAnimator;
    BoxCollider2D leftswordHitbox;
    SpriteRenderer spriteRenderer;
    public static bool turnedLeft = false;
    public GameObject winText;
    public GameObject restartText;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        swordAnimator = rightSword.GetComponent<Animator>();
        swordHitbox = rightSword.GetComponent<BoxCollider2D>();
        leftswordAnimator = leftSword.GetComponent<Animator>();
        leftswordHitbox = leftSword.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        UpdateSwitch();
        animator.SetInteger("animWalkSwitch", horInputSwitch);
        if (!gameOver) transform.Translate(horInput*Time.deltaTime, 0, 0);

        if (horInput < 0 && !gameOver)
        {
            spriteRenderer.flipX = true;
            leftSword.transform.eulerAngles = new Vector3(0, 0, 120);
            leftSword.SetActive(true);
            rightSword.SetActive(false);
            turnedLeft = true;
        }
        else if (horInput > 0 && !gameOver)
        {
            spriteRenderer.flipX = false;
            leftSword.SetActive(false);
            rightSword.transform.eulerAngles = new Vector3(0, 0, 60);
            rightSword.SetActive(true);
            turnedLeft = false;
        }
        else;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameOver)
        {
            SwingSword();
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("Level1");

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

        if (GameManager.canExit == true && Input.GetKeyDown(KeyCode.E) && GameManager.enemyCount == 0)
        {
            winText.SetActive(true);
            restartText.SetActive(true);
            gameOver = true;
        }
        
    }

    void SwingSword()
    {
        if (turnedLeft == false) swordAnimator.Play("SwordSwing");
        else leftswordAnimator.Play("SwordSwingLeft");

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
