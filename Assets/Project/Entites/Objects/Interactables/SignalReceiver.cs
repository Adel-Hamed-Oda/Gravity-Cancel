using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour
{
    [Tooltip("When you use this ya Adel law sama7t, everything in the array is combined with an AND, if you want to do an OR combine different SignalReceivers together")]
    [SerializeField] private List<SignalProvider> signalProviders;

    public void AddProvider(SignalProvider provider)
    {
        if (!signalProviders.Contains(provider))
        {
            signalProviders.Add(provider);
        }
    }
    public void RemoveProvider(SignalProvider provider)
    {
        if (signalProviders.Contains(provider))
        {
            signalProviders.Remove(provider);
        }
    }

    public bool CheckIfAllSignalsActive()
    {
        foreach (SignalProvider provider in signalProviders)
        {
            if (!provider.isSignalActive)
            {
                return false;
            }
        }
        return true;
    }
}