using System;
using UnityEngine;

public class Daytime : MonoBehaviour
{
    private int _time;
    public Action OnDaytimeChange;
    public int Time
    {
        get => _time;
        set
        {
            _time = value;
            OnDaytimeChange?.Invoke();
        }
    }
}
