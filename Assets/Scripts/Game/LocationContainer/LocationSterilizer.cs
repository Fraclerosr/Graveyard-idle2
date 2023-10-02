using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LocationSterilizer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private PlayerStackPresenter _playerStackPresenter;
    [SerializeField] private PlayerCurrentLocation _currentLocation;
    [SerializeField] private List<LocationItemsContainer> _containers;

    private void OnEnable()
    {
        _currentLocation.EnteredLocation += OnEnteredLocation;
    }

    private void OnDisable()
    {
        _currentLocation.EnteredLocation -= OnEnteredLocation;
    }

    private void OnEnteredLocation(Location location)
    {
        var items = _playerStackPresenter.RemoveAll();
        var container = _containers.FirstOrDefault(container => container.Location == location);

        if (items.Count() > 0)
            Instantiate(_effect, _playerStackPresenter.transform.position, Quaternion.identity);

        if (container == null)
        {
            foreach (var item in items)
            {
                Destroy(item.gameObject);
            }
        }
        else
        {
            container.Add(items);
        }
    }
}
