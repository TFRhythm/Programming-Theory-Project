using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHazard : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private int DamageDealt = 1;
    protected Rigidbody2D PlayerRb2D;
    protected Collider2D PlayerCollider2D;
    protected PlayerControls Player;

    protected Vector2 RecoilDirection;

    private SpriteRenderer sr;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerCollider2D = player.GetComponent<Collider2D>();
        PlayerRb2D = player.GetComponent<Rigidbody2D>();
        Player = player.GetComponent<PlayerControls>();

        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Mathf.Clamp(DamageDealt, 1, 3);

        switch (DamageDealt)
        {
            case 1: sr.color = Color.white; break;
            case 2: sr.color = Color.yellow; break;
            case 3: sr.color = Color.red; break;

        }
    }

    protected virtual Vector2 RecoilDirectionCalculator()
    {
        Vector2 direction = (PlayerRb2D.position - (Vector2)this.transform.position).normalized;
        return direction;
    }

    protected virtual void DealDamageToPlayer()
    {
        Player.ModifyHealth(DamageDealt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == PlayerCollider2D)
        {
            Vector2 direction = RecoilDirectionCalculator();
            PlayerRb2D.position = PlayerRb2D.position + direction;

            DealDamageToPlayer();
        }
    }
}
