using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class IntentionView : MonoBehaviour
{
    private const string Disappearance = nameof(Disappearance);
    private const string Appearance = nameof(Appearance);

    [RequireInterface(typeof(IIntentionSource))] 
    [SerializeField] private Object _intentionSourceObject;
    [SerializeField] private IntentionTypeViewsContainer _intentionTypeViewsContainer;
    [SerializeField] private Image _image;
    [SerializeField] private Animation _animation;

    private IIntentionSource _intentionSource;
    private IntentionType _currentIntentionType = IntentionType.NullIntention;

    private void Awake() => 
        _intentionSource = _intentionSourceObject as IIntentionSource;

    private void OnEnable() => 
        _intentionSource.IntentionUpdated += OnIntentionUpdated;

    private void OnDisable() => 
        _intentionSource.IntentionUpdated -= OnIntentionUpdated;

    private void OnIntentionUpdated(IntentionType intentionType)
    {
        IntentionType lastIntentionType = _currentIntentionType;
        _currentIntentionType = intentionType;

        if (lastIntentionType != IntentionType.NullIntention)
            _animation.PlayQueued(Disappearance);
        else
            OnDisappearedAnimationEvent();
        
        if (intentionType != IntentionType.NullIntention)
            _animation.PlayQueued(Appearance);
    }

    private void OnDisappearedAnimationEvent()
    {
        _image.sprite = _intentionTypeViewsContainer.GetForType(_currentIntentionType).Sprite;
    }
}