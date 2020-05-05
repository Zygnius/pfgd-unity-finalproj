using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arena : MonoBehaviour
{
#pragma warning disable 0649
#pragma warning restore 0649

    public List<Enemy> enemies;

    public UnityEvent onArenaStart;
    public UnityEvent onArenaEnd;

    private void Start()
    {
        foreach (Enemy item in enemies)
        {
            EnemyOnDestroy temp = item.gameObject.AddComponent<EnemyOnDestroy>();
            temp.SetArena(this);
        }
    }

    public void ActivateArena()
    {
        onArenaStart.Invoke();
        StartCoroutine(arenaActive());
    }

    private IEnumerator arenaActive()
    {
        while(enemies.Count > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        DeactivateArena();
    }

    public void DeactivateArena()
    {
        onArenaEnd.Invoke();
    }
}
