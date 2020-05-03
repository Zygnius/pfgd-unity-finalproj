using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Parameters")]
    [SerializeField] private float fireTime;
    [SerializeField] private int damage;
    [SerializeField] private float knockback;

    [Header("References")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Projectile projectile;
#pragma warning restore 0649

    private Enemy enemy;
    private float timer = 0;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.IsAlerted())
        {
            if (timer >= fireTime)
            {
                Vector2 direction = playerData.position - (Vector2)transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Projectile temp = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
                temp.gameObject.layer = gameObject.layer;
                temp.Initialize(damage, knockback);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
