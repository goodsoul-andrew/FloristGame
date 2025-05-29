using System.Collections;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    [SerializeField] private float existenceTime;

    void Start()
    {
        StartCoroutine(RemoveAfterDelay());
    }


    void Update()
    {
        
    }
    
    private IEnumerator RemoveAfterDelay()
    {
        yield return new WaitForSeconds(existenceTime);
        Destroy(gameObject);
    }
}
