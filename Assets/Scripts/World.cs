using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Tilemap[] tilemaps;
#pragma warning restore 0649

    public void ToggleWorld()
    {
        foreach (Tilemap item in tilemaps)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
    }
}
