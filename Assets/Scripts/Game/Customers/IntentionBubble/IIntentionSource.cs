using System;

public interface IIntentionSource
{
    public event Action<IntentionType> IntentionUpdated;
}