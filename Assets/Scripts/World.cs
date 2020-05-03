using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Tilemap[] tilemaps;
    [SerializeField] private float fadeTime;
#pragma warning restore 0649

    private bool trackWorld;
    private Coroutine routine;

    private Color clearWhite = new Color(1, 1, 1, 0);

    private void Awake()
    {
        trackWorld = tilemaps[0].gameObject.activeSelf;
    }

    public void ToggleWorld()
    {
        if(routine == null)
        {
            routine = StartCoroutine(FadeWorld());
        }
    }

    private IEnumerator FadeWorld()
    {
        if (!trackWorld)
        {
            foreach (Tilemap item in tilemaps)
            {
                item.gameObject.SetActive(!trackWorld);
            }
        }

        float timer = 0;
        while(timer < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

            foreach (Tilemap item in tilemaps)
            {
                if (trackWorld) item.color = Color.Lerp(Color.white, clearWhite, timer / fadeTime);
                else item.color = Color.Lerp(clearWhite, Color.white, timer / fadeTime);
            }
        }

        if (trackWorld)
        {
            foreach (Tilemap item in tilemaps)
            {
                item.gameObject.SetActive(!trackWorld);
            }
        }
        trackWorld = !trackWorld;
        routine = null;
    }
}
