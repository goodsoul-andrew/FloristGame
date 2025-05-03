using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeStrength = 0.1f;

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-shakeStrength, shakeStrength);
            float yOffset = Random.Range(-shakeStrength, shakeStrength);

            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
