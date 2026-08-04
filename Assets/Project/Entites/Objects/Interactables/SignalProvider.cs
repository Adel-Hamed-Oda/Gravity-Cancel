using System;
using UnityEngine;

public class SignalProvider : MonoBehaviour
{
    public event Action OnSignalActivated;
    public event Action OnSignalDeactivated;

    [HideInInspector] public bool isSignalActive = false;

    public void Activate()
    {
        isSignalActive = true;
        OnSignalActivated?.Invoke();
    }

    public void Deactivate()
    {
        isSignalActive = false;
        OnSignalDeactivated?.Invoke();
    }
}