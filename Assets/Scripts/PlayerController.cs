﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        player.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {
            player.DodgeRoll(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //player.Attack(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            player.Attack(direction);
        }
    }
}
