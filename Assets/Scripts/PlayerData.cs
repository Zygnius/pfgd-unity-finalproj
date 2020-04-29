using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public Vector2 position;
    public int layer;
}
