using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

#pragma warning disable 0649
    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float detectionRange;
    [SerializeField] private bool maintainDistance;
    [SerializeField] private float distanceFromPlayer;

    [Header("References")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EventRef worldToggleEvent;
    [SerializeField] private GameObject shadow;
#pragma warning restore 0649

    private float stunned = 0;
    private float knockedback = 0;
    private Vector2 knockedbackDirection = Vector2.zero;

    private bool alerted = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        worldToggleEvent.refEvent.AddListener(OnWorldToggle);
    }

    private void Update()
    {
        if((playerData.position - (Vector2) transform.position).magnitude < detectionRange && playerData.layer == gameObject.layer - 2 && stunned <= 0 && knockedback <= 0)
        {
            rb.velocity = (playerData.position - (Vector2)transform.position).normalized * moveSpeed;
            if(Vector2.Distance(transform.position, playerData.position) < distanceFromPlayer)
            {
                if (maintainDistance)
                {
                    rb.velocity = (playerData.position - (Vector2)transform.position).normalized * moveSpeed * -1;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            alerted = true;
        }
        else if (knockedback > 0)
        {
            rb.velocity = knockedbackDirection * knockedback;
            knockedback -= Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
            alerted = false;
        }
        if (stunned > 0) stunned -= Time.deltaTime;
    }

    public bool IsAlerted()
    {
        return alerted;
    }

    public bool IsHitstunned()
    {
        return stunned > 0 || knockedback > 0;
    }

    private void OnWorldToggle()
    {
        sr.enabled = !sr.enabled;
        shadow.SetActive(!shadow.activeSelf);
        stunned += 0.5f;
    }

    private IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    public override void OnHeal(int amount)
    {
        throw new System.NotImplementedException();
    }

    public override void OnHit(int amount)
    {
        stunned += Mathf.Clamp(amount / 2, 0, 2);
        StartCoroutine(FlashRed());
    }

    public override void OnKnockback(float amount, Vector2 direction)
    {
        knockedback += amount;
        knockedbackDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && damage > 0)
        {
            knockedback += 0.5f;
            knockedbackDirection = transform.position - collision.collider.transform.position;
            stunned += 1;
        }
    }
}
