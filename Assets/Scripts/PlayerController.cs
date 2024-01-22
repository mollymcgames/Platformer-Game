using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private LayerMask groundJumpable;
    [SerializeField] private float wallCheckDistance = 0.2f; // Adjust the distance as needed

    private float dirX = 0f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private AudioSource jumpSound;

    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        PhysicsMaterial2D frictionlessMaterial = new PhysicsMaterial2D();
        frictionlessMaterial.friction = 0f;
        bc.sharedMaterial = frictionlessMaterial;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        // Check for wall collisions
        if (IsTouchingWall() && Mathf.Abs(dirX) > 0)
        {
            dirX = 0f; // Prevent horizontal movement if touching a wall
        }

        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            jumpSound.Play();
        }

        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            Attack();
        }

        UpdateSprite();
    }

    private void Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        anim.SetTrigger("attack");
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        float attackAnimationDuration = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(attackAnimationDuration);

        anim.ResetTrigger("attack");
        isAttacking = false;
    }

    private void UpdateSprite()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sr.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sr.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundJumpable);
    }

    private bool IsTouchingWall()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, groundJumpable);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, groundJumpable);

        return hitRight.collider != null || hitLeft.collider != null;
    }

    private enum MovementState { idle, running, jumping, falling }
}
