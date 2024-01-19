using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb; // reference to the rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get the rigidbody component
    }

    // Update is called once per frame
    private void Update()
    {

        // allow the player to move left and right
        float dirX = Input.GetAxisRaw("Horizontal"); // get the horizontal input
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y); // move the player in the x direction
        // allow the player to jump
        if (Input.GetButtonDown("Jump"))
        {
            // rb.velocity = new Vector2(rb.velocity.x, 10f); //only jump in y direction
            rb.AddForce(new Vector2(rb.velocity.x, 10f), ForceMode2D.Impulse); // jump in both x and y direction
        }
    }
}
