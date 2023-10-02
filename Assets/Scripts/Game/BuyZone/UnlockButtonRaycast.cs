using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnlockButtonRaycast : MonoBehaviour
{
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;

    private void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            _eventSystem.RaycastAll(_pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out UnlockWorldSpaceButton unlockZoneButton))
                {
                    if (unlockZoneButton.CanClick)
                    {
                        unlockZoneButton.Click();
                    }
                }
            }
        }
    }
}
