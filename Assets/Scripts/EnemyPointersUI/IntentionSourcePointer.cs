using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.UI.EnemyPointersUI
{
    public class IntentionSourcePointer : MonoBehaviour
    {
        [RequireInterface(typeof(IIntentionSource))]
        [SerializeField] private Object _intentionSourceObject;
        [SerializeField] private Transform _pointPosition;
        [SerializeField] private RectTransform _boundaries;
        [SerializeField] private Transform _player;

        private PointerBehaviour[] _pointers;
        private Camera _camera;
        private IntentionType _currentIntention;
        private IIntentionSource _intentionSource => (IIntentionSource) _intentionSourceObject;

        private void OnEnable() => 
            _intentionSource.IntentionUpdated += OnIntentionUpdated;

        private void Awake()
        {
            _camera = Camera.main;
            _pointers = GetComponentsInChildren<PointerBehaviour>();

            foreach (PointerBehaviour pointer in _pointers)
                pointer.Construct(_boundaries, _camera);
            
            SetPointersActive(false);
        }

        public void LateUpdate()
        {
            if(_currentIntention != IntentionType.NullIntention)
                UpdatePointers();
        }

        private void OnDisable() => 
            _intentionSource.IntentionUpdated -= OnIntentionUpdated;

        private void UpdatePointers()
        {
            _pointers[0].gameObject.SetActive(true);
            _pointers[0].Point(_player.position, _pointPosition.position);
        }

        private void OnIntentionUpdated(IntentionType type)
        {
            _currentIntention = type;
            SetPointersActive(type != IntentionType.NullIntention);
        }

        private void SetPointersActive(bool active)
        {
            foreach (PointerBehaviour pointer in _pointers)
                pointer.gameObject.SetActive(active);
        }
    }
}