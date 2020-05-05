using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoor : MonoBehaviour
{
    private Arena arena;
    private Collider2D coll;
    private SpriteRenderer sr;

    private void Awake()
    {
        arena = GetComponentInParent<Arena>();
        coll = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        arena.onArenaStart.AddListener(OnArenaStart);
        arena.onArenaEnd.AddListener(OnArenaEnd);
        coll.isTrigger = true;
        sr.enabled = false;
    }

    private void OnArenaStart()
    {
        coll.isTrigger = false;
        coll.offset = Vector2.zero;
        sr.enabled = true;
    }

    private void OnArenaEnd()
    {
        coll.isTrigger = true;
        coll.enabled = false;
        sr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        arena.ActivateArena();
    }
}
