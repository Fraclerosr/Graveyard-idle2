using System;

public interface ITutorialAnalyticsEventSource
{
    event Action<string> EventSended;
}
