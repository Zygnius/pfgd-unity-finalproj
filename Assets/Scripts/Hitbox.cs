using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
#pragma warning restore 0649

    private BoxCollider2D coll;
    private ContactFilter2D filter;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();

        gameObject.layer = transform.parent.gameObject.layer;
    }

    public void Initialize(int damageAmount, float knockbackAmount, LayerMask mask)
    {
        damage = damageAmount;
        knockback = knockbackAmount;
        filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
    }

    public void SetHitboxSize(Vector2 size)
    {
        coll.size = size;
    }

    private void Start()
    {
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll, filter, results);

        foreach(Collider2D item in results)
        {
            Hurtbox temp = item.GetComponent<Hurtbox>();
            if (temp != null)
            {
                temp.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox temp = collision.GetComponent<Hurtbox>();
        if (temp != null)
        {
            temp.TakeDamage(damage);
            temp.TakeKnockback(knockback, transform.up);
        }
    }
}
