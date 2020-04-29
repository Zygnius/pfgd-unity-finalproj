using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Entity entity;
#pragma warning restore 0649

    public void TakeDamage(int amount)
    {
        entity.TakeDamage(amount);
    }

    public void TakeKnockback(float amount, Vector2 direction)
    {
        entity.OnKnockback(amount, direction);
    }
}
