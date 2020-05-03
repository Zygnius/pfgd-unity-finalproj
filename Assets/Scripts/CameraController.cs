using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float smoothTime;
#pragma warning restore 0649

    private Vector2 velocity = Vector2.zero;

    private void Update()
    {
        Vector2 movement = Vector2.SmoothDamp(transform.position, playerData.position, ref velocity, smoothTime);
        transform.position = new Vector3(movement.x, movement.y, transform.position.z);
    }
}
