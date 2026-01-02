using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxSpeed;

    private float moveInput;
    private int turnInput;

    //ENCAPSULATION
    public int health { get; private set; } = 5;

    [SerializeField] private SpriteRenderer FlameSR;
    [SerializeField] private Text HealthText;
    private float HealthFlashTimer = 0f;

    private Rigidbody2D rb2D;

    [SerializeField] private LevelEnd EndLevel;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FlameSR.enabled = false;
        SetHealthText(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            FlameSR.enabled = true;
            moveInput = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            FlameSR.enabled = false;
            moveInput = -0.2f;
        }
        else
        {
            FlameSR.enabled = false;
            moveInput = 0;
        }

        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            turnInput = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            turnInput = -1; 
        }
        else
        {
            turnInput = 0;
        }


        if (HealthFlashTimer > 0)
        {
            HealthFlashTimer -= Time.deltaTime;
            if (HealthFlashTimer <= 0) 
            {
                HealthFlashTimer = 0;
                HealthText.color = Color.white;
            }
        }
    }

    void FixedUpdate()
    {
        float angle = rb2D.rotation + turnInput * turnSpeed * Time.fixedDeltaTime;
        rb2D.MoveRotation(angle);

        Vector2 forward = transform.up;
        rb2D.AddForce(forward * moveInput * thrust, ForceMode2D.Force);

        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        SetHealthText(false);
    }

    public void ModifyHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            //ABSTRACTION
            EndLevel.EndLevel(false);
        }
        SetHealthText(false);
    }
    private void SetHealthText(bool isSceneStart)
    {
        HealthText.text = "Health: " + health;
        if (isSceneStart)
        {
            return;
        }

        HealthText.color = Color.red;
        HealthFlashTimer = 0.2f;
    }

}
