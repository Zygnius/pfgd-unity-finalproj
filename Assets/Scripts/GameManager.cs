using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

#pragma warning disable 0649
    [SerializeField] private Camera camPhysical;
    [SerializeField] private Camera camEthereal;
    [SerializeField] private World worldA;
    [SerializeField] private World worldB;
    [SerializeField] private EventRef OnWorldToggle;
#pragma warning restore 0649

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ToggleWorlds()
    {
        //TODO animate world shift
        worldA.ToggleWorld();
        worldB.ToggleWorld();
        OnWorldToggle.refEvent.Invoke();
    }
}
