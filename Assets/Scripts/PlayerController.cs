using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb; // reference to the rigidbody component
    private BoxCollider2D bc; // reference to the box collider component
    private SpriteRenderer sr; // reference to the sprite renderer component
    private Animator anim; // reference to the animator component

    [SerializeField] private LayerMask groundJumpable; // layer mask for the ground

    private float dirX = 0f; // direction of the player
    [SerializeField] private float speed = 7f; // speed of the player
    [SerializeField] private float jumpForce = 10f; // force of the player's jump

    private enum MovementState { idle, running, jumping, falling } // states of the player

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get the rigidbody component
        bc = GetComponent<BoxCollider2D>(); // get the box collider component
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
        if (Input.GetButtonDown("Jump") && IsGrounded()) // if the player presses the jump button and is on the ground
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse); // jump in both x and y direction
        }

        UpdateSprite(); // update the animation state

    }

    private void UpdateSprite()
    {

        MovementState state; // create a variable to store the state of the player
        if (dirX > 0f)
        {
            state = MovementState.running; // set the state to running
            // anim.SetBool("isRunning", true); // set the isRunning parameter to true
            sr.flipX = false; // don't flip the sprite
        }
        else if (dirX < 0f) //moving left
        {
            state = MovementState.running; // set the state to running
            sr.flipX = true; // flip the sprite
        }
        else
        {
            state = MovementState.idle; // set the state to idle
            // anim.SetBool("isRunning", false); // set the isRunning parameter to false        }
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping; // set the state to jumping
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling; // set the state to falling
        }

        anim.SetInteger("state", (int)state); // set the state parameter to the state variable

    }

    private bool IsGrounded()
    {
        // check if the player is on the ground
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundJumpable); // cast a box down to the ground
    }

}
