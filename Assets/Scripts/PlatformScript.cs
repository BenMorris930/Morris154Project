using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformScript : MonoBehaviour
{
    private static TilemapCollider2D platforms;
    // Start is called before the first frame update
    void Start()
    {
        platforms = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.goingUp == true) platforms.enabled = false;
        else platforms.enabled = true;

    }
}
