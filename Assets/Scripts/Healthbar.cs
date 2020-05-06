using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Entity entity;
#pragma warning restore 0649

    private Image bar;

    private void Awake()
    {
        bar = GetComponent<Image>();
    }

    private void Update()
    {
        bar.fillAmount = entity.healthPercent();
    }
}
