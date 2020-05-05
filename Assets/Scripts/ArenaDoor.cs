using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoor : MonoBehaviour
{
    private Arena arena;
    private GameObject door;
    private Collider2D coll;

    private void Awake()
    {
        arena = GetComponentInParent<Arena>();
        door = transform.GetChild(0).gameObject;
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        arena.onArenaStart.AddListener(OnArenaStart);
        arena.onArenaEnd.AddListener(OnArenaEnd);
        door.SetActive(false);
    }

    private void OnArenaStart()
    {
        door.SetActive(true);
        coll.enabled = false;
    }

    private void OnArenaEnd()
    {
        door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            arena.ActivateArena();
    }
}
