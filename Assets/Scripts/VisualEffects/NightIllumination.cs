using UnityEngine;
using UnityEngine.Rendering.Universal;

class NightIllumination : MonoBehaviour
{
    private Daytime daytime;
    private Light2D light2D;
    [SerializeField] private int numberWhenActivate = 3;
    
    void Start()
    {
        light2D = GetComponent<Light2D>();
        daytime = FindFirstObjectByType<Daytime>();
        if (light2D != null)
        {
            light2D.enabled = false;
            if (daytime.Time >= numberWhenActivate)
            {
                light2D.enabled = true;
            }
            daytime.OnDaytimeChange += () => { if (daytime.Time >= numberWhenActivate && light2D != null) light2D.enabled = true; };
        }
    }   
}