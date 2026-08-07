public class LockedBlock : SignalReceiver
{
    // LOL that's it
    private void Update()
    {
        if (CheckIfAllSignalsActive())
        {
            Destroy(gameObject);
        }
    }
}