using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    private PlayerControls Player;
    private Collider2D PlayerCollider2D;
    private Rigidbody2D PlayerRb2D;

    private bool isLevelComplete = false;

    [SerializeField] private GameObject EndPanel;
    [SerializeField] private Text EndText;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerCollider2D = player.GetComponent<Collider2D>();
        PlayerRb2D = player.GetComponent <Rigidbody2D>();
        Player = player.GetComponent<PlayerControls>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isLevelComplete = false;
        EndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isLevelComplete && collision.collider == PlayerCollider2D && PlayerRb2D.velocity.magnitude < 0.01f)
        {
            Debug.Log("END");
            EndLevel(true);
        }
    }

    public void EndLevel(bool victory)
    {
        PlayerRb2D.velocity = Vector3.zero;
        PlayerRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        isLevelComplete = true;
        EndPanel.SetActive(true);
        if (victory)
        {
            EndText.text = "You Win!\r\n\r\nHealth Remaining: " + Player.health + "\r\n\r\nPress Space to Play Again";
        }
        else
        {
            Destroy(Player.gameObject);
            EndText.text = "Game Over!\r\n\r\nPress Space to Play Again";

        }
    }
}
