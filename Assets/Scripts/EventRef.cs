using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New EventRef", menuName = "EventRef")]
public class EventRef : ScriptableObject
{
    public UnityEvent refEvent;
}
