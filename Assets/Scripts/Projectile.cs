using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] private EventRef worldToggleEvent;
#pragma warning restore 0649

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool world = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        worldToggleEvent.refEvent.AddListener(ToggleWorlds);
    }

    public void Initialize(int damageAmount, float knockbackAmount)
    {
        damage = damageAmount;
        knockback = knockbackAmount;

        rb.velocity = transform.up * moveSpeed;
    }

    public void ToggleWorlds()
    {
        if (world)
        {
            world = false;
            sr.color = Color.white;
        }
        else
        {
            world = true;
            sr.color = new Color(1, 1, 1, 0.4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox temp = collision.GetComponent<Hurtbox>();
        if (temp != null)
        {
            temp.TakeDamage(damage);
            temp.TakeKnockback(knockback, transform.up);
            Destroy(gameObject);
        }
        else if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
