using UnityEngine;

public class ConveyorTransformedEffects : MonoBehaviour
{
    [SerializeField] private Conveyor _conveyor;
    [SerializeField] private ParticleSystem _particles;

    private void OnEnable() => 
        _conveyor.TransformedItem += OnTransformedItem;

    private void OnDisable() => 
        _conveyor.TransformedItem -= OnTransformedItem;

    private void OnTransformedItem() => 
        _particles.Play();
}
