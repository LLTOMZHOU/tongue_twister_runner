using UnityEngine;

/**
 * With aid of chatGPT
 */
public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool isHarmful;
    [SerializeField] private float speed = 5.0f;
    
    private Rigidbody2D rb2D;


    public void SetMoveSpeed(float speed)
    {
        this.speed = speed;
    }
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; // Disable gravity for the character
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isHarmful)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.LoseLife();
                Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        MoveObstacle();
    }
    private void MoveObstacle()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 0.2f);
    }
    
}