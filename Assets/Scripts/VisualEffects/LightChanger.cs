using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightChanger : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeLightColor();
        }
    }

    private void ChangeLightColor()
    {
        if (globalLight != null)
        {
            globalLight.color = color;
        }
    }
}