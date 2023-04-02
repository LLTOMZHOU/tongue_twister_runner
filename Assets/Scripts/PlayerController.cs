using System.Collections;
using UnityEngine;

/**
 * With the aid of chatGPT
 */
public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 5f;
    public float boundaryPadding = 0.5f;
    public int lives = 3;

    private Rigidbody2D rb2D;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public float damageFlashDuration = 0.1f;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.01f;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    
    private bool isShaking = false;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; // Disable gravity for the character

        // Get screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get object dimensions
        objectWidth = spriteRenderer.bounds.extents.x;
        objectHeight = spriteRenderer.bounds.extents.y;
    }

    public void LoseLife()
    {
        lives--;
        StartCoroutine(FlashRed());
        StartCoroutine(Shake());
        if(lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("You died!");
        Destroy(gameObject,0.3f);
    }
    void Update()
    {
        if (isShaking)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the new velocity based on input
        Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
        Vector2 newVelocity = rb2D.velocity + inputDirection * acceleration * Time.deltaTime;

        // Adjust the magnitude of the new velocity
        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = newVelocity.normalized * maxSpeed;
        }
        if(inputDirection.magnitude == 0)
        {
            newVelocity = new Vector2(0, 0);
        }
        // Set the new velocity
        rb2D.velocity = newVelocity;

        // Keep the character within the screen boundary
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenBounds.x + objectWidth + boundaryPadding, screenBounds.x - objectWidth - boundaryPadding),
            Mathf.Clamp(transform.position.y, -screenBounds.y + objectHeight + boundaryPadding, screenBounds.y - objectHeight - boundaryPadding),
            transform.position.z
        );
    }
    
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator Shake()
    {
        
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }
}
