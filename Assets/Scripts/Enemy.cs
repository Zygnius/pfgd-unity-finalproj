﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

#pragma warning disable 0649
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float detectionRange;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EventRef worldToggleEvent;
    [SerializeField] private GameObject shadow;
#pragma warning restore 0649

    private float stunned = 0;
    private float knockedback = 0;
    private Vector2 knockedbackDirection = Vector2.zero;

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
        if((playerData.position - (Vector2) transform.position).magnitude < detectionRange && playerData.layer == gameObject.layer && stunned <= 0 && knockedback <= 0)
        {
            rb.velocity = (playerData.position - (Vector2)transform.position).normalized * moveSpeed;
        }
        else if (knockedback > 0)
        {
            rb.velocity = knockedbackDirection * knockedback;
            knockedback -= Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if (stunned > 0) stunned -= Time.deltaTime;
    }

    private void OnWorldToggle()
    {
        sr.enabled = !sr.enabled;
        shadow.SetActive(!shadow.activeSelf);
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
    }

    public override void OnKnockback(float amount, Vector2 direction)
    {
        knockedback = amount;
        knockedbackDirection = direction;
    }
}