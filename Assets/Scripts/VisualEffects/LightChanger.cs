using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightChanger : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color color;
    [SerializeField][Range(0, 3)] private int daytime;
    [SerializeField] private float duration;
    private Daytime globalDaytime;

    void Start()
    {
        globalDaytime = FindFirstObjectByType<Daytime>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ChangeLightColor());
        }
    }

    private IEnumerator ChangeLightColor()
    {
        if (globalLight != null)
        {
            Color startColor = globalLight.color;
            float timeElapsed = 0f;
            while (timeElapsed < duration)
            {
                globalLight.color = Color.Lerp(startColor, color, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            globalLight.color = color;
            globalDaytime.Time = daytime;
        }
    }
}