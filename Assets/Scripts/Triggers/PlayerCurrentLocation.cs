using System;
using UnityEngine;

public class PlayerCurrentLocation : MonoBehaviour
{
    [SerializeField] private Location _location;
    
    public event Action<Location> EnteredLocation;

    public Location Location => _location;

    public bool CanTransit(Location targetLocation)
    {
        return targetLocation != _location;
    }

    public void Transit(Location targetLocation)
    {
        if (!CanTransit(targetLocation))
            throw new InvalidOperationException("Location transition");

        EnterLocation(targetLocation);
    }

    private void EnterLocation(Location location)
    {
        _location = location;
        EnteredLocation?.Invoke(_location);
    }
}