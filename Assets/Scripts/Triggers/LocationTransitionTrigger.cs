using UnityEngine;

public class LocationTransitionTrigger : Trigger<PlayerCurrentLocation>
{
    [SerializeField] private Location _location;

    protected override void OnEnter(PlayerCurrentLocation playerCurrentLocation)
    {
        if (playerCurrentLocation.CanTransit(_location))
            playerCurrentLocation.Transit(_location);
    }
}