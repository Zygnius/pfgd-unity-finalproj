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
#pragma warning restore 0649

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int damageAmount, float knockbackAmount)
    {
        damage = damageAmount;
        knockback = knockbackAmount;

        rb.velocity = transform.up * moveSpeed;
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
        else if (gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
