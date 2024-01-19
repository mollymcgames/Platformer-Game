using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb; // reference to the rigidbody component
    private SpriteRenderer sr; // reference to the sprite renderer component
    private Animator anim; // reference to the animator component

    private float dirX = 0f; // direction of the player
    [SerializeField] private float speed = 7f; // speed of the player
    [SerializeField] private float jumpForce = 10f; // force of the player's jump

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get the rigidbody component
        sr = GetComponent<SpriteRenderer>(); // get the sprite renderer component
        anim = GetComponent<Animator>(); // get the animator component
    }

    // Update is called once per frame
    private void Update()
    {

        // allow the player to move left and right
        dirX = Input.GetAxisRaw("Horizontal"); // get the horizontal input
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y); // move the player in the x direction
        // allow the player to jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse); // jump in both x and y direction
        }

        UpdateSprite(); // update the animation state

    }

    private void UpdateSprite()
    {
        if (dirX > 0f)
        {
            anim.SetBool("isRunning", true); // set the isRunning parameter to true
            sr.flipX = false; // don't flip the sprite
        }
        else if (dirX < 0f) //moving left
        {
            anim.SetBool("isRunning", true); // set the isRunning parameter to true
            sr.flipX = true; // flip the sprite
        }
        else
        {
            anim.SetBool("isRunning", false); // set the isRunning parameter to false
        }
    }
}
