using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private int _currHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _isDead;
    [SerializeField] private float invincibilityTime;
#pragma warning restore 0649

    public int currHealth => _currHealth;
    public int maxHealth => _maxHealth;
    public bool isDead => _isDead;

    private bool invincible = false;

    private void Start()
    {
        _isDead = false;
        _currHealth = _maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (invincible) return;

        _currHealth -= amount;
        if (amount >= 0) OnHit(amount);
        else OnHeal(-amount);
        if (_currHealth <= 0)
        {
            _isDead = true;
            _currHealth = 0;
            OnDeath();
        }
        else if (_currHealth > _maxHealth) _currHealth = _maxHealth;

        StartCoroutine(invincibilityAfterDamage());
    }

    private IEnumerator invincibilityAfterDamage()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    public float healthPercent()
    {
        return _currHealth / (float)_maxHealth;
    }

    abstract public void OnDeath();
    abstract public void OnHit(int amount);
    abstract public void OnHeal(int amount);
    abstract public void OnKnockback(float amount, Vector2 direction);
}
