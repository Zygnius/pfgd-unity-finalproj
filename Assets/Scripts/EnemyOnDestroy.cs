using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDestroy : MonoBehaviour
{
    private Arena arena;

    public void SetArena(Arena a)
    {
        arena = a;
    }

    private void OnDestroy()
    {
        arena.enemies.Remove(GetComponent<Enemy>());
    }
}
