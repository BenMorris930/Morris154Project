using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    bool holding = false;
    public float speed = 5;
    public float baseSpeed = 100;
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bc1;
    BoxCollider2D bc2;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc1 = gameObject.GetComponent<BoxCollider2D>();
        bc2 = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            holding = false;
            rb.gravityScale = 1;
            rb.drag = 2;
            //speed = baseSpeed;
        }
        if (holding == true)
        {
            //transform.position = Vector2.MoveTowards(gameObject.transform.position, Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), 10 * Time.deltaTime);
            //speed = speed *0.5f* Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), gameObject.transform.position);
            rb.AddForce((Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)) - gameObject.transform.position) * speed);

            //speed = baseSpeed;
        }
    }

    private void OnMouseOver()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            holding = true;
            rb.gravityScale = 0;
            rb.drag = 15;
            rb.AddTorque(-1f);
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && rb.velocity.magnitude > 0.1f)
        {
            collision.gameObject.SetActive(false);
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            explosion.SetActive(true);
            sr.enabled = false;
            bc1.enabled = false;
            bc2.enabled = false;
            StartCoroutine(ExecuteAfterTime(1));
        }
    }
}
