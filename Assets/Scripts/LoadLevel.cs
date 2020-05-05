using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string level;

    public void LoadScene(string scene)
    {
        LoadManager.instance.LoadScene(scene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadScene(level);
    }
}
