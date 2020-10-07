using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorScript : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorClosed;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerMovement.turnedLeft == false && collision.gameObject.CompareTag("Weapon"))
        {
            boxCollider.enabled = false;
            doorRight.SetActive(true);
            doorClosed.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            boxCollider.enabled = false;
            doorLeft.SetActive(true);
            doorClosed.SetActive(false);
        }    
    {
        
        }
    }
}
