using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    private Rigidbody2D rb; // reference to the rigidbody component
    private Animator anim; // reference to the animator component
    private SpriteRenderer spriteRenderer; // reference to the sprite renderer component
    private bool isAlive = true; // flag to track player's life status

    [SerializeField] private AudioSource deathSound; // sound to play when the player dies

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>(); // get the animator component
        rb = GetComponent<Rigidbody2D>(); // get the rigidbody component
        spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spikes"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSound.Play(); // play the death sound
        // Set the flag to indicate the player is no longer alive
        isAlive = false;

        // Optionally, play death animation
        anim.SetTrigger("death");

        // Disable the sprite renderer after a delay
        StartCoroutine(DisableSpriteRendererWithDelay(1f));

        // Freeze the position by setting constraints
        rb.constraints = RigidbodyConstraints2D.FreezePosition;        

        StartCoroutine(ReloadLevelWithDelay(2f));
    }

    private IEnumerator DisableSpriteRendererWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.enabled = false;
    }

    private IEnumerator ReloadLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the Rigidbody properties before reloading the level
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        ReloadLevel();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
