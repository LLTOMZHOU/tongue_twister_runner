using UnityEngine;

/**
 * With the aid of chatGPT
 */
public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 5f;
    public float boundaryPadding = 0.5f;

    private Rigidbody2D rb2D;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; // Disable gravity for the character

        // Get screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get object dimensions
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
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
}
