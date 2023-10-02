#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class UnlockZoneRule : MonoBehaviour
{
    [SerializeField] private List<Pair> _pairs;
    [SerializeField] private bool _drawGizmos;

    private void OnEnable()
    {
        foreach (var pair in _pairs)
        {
            foreach (var zone in pair.Zones)
            {
                zone.transform.localScale = Vector3.zero;
                zone.enabled = false;

                if (pair.UnlockRule.CanUnlock)
                {
                    zone.enabled = true;
                    zone.transform.DOScale(1f, 1f).OnComplete(zone.PlayNewText);
                }
            }

            pair.UnlockRule.AddUpdateListener(OnUnlockRuleUpdate);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in _pairs)
            pair.UnlockRule.RemoveUpdateListener(OnUnlockRuleUpdate);
    }

    private void OnUnlockRuleUpdate()
    {
        foreach (var pair in _pairs)
        {
            if (pair.UnlockRule.CanUnlock)
            {
                foreach (var zone in pair.Zones)
                {
                    zone.enabled = true;
                    zone.transform.DOScale(1f, 1f).OnComplete(zone.PlayNewText);
                }
            }
        }
    }

    [Serializable]
    public class Pair
    {
        [SerializeField] private List<BuyZonePresenter> _zones;
        [SerializeField] private UnlockRule _unlockRule;

        public IEnumerable<BuyZonePresenter> Zones => _zones;
        public UnlockRule UnlockRule => _unlockRule;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_drawGizmos == false)
            return;

        var style = new GUIStyle();
        style.normal.textColor = Color.red;

        var sceneCamera = SceneView.currentDrawingSceneView.camera;
        foreach (var pair in _pairs)
        {
            foreach (var zone in pair.Zones)
            {
                var distance = Vector3.Distance(sceneCamera.transform.position, zone.transform.position);
                style.fontSize = (int)Mathf.Clamp(300f / distance, 5f, 30f);

                var text = $"Target: {pair.UnlockRule.TargetProgress}";
                Handles.Label(zone.transform.position + Vector3.up * 2, text, style);
            }
        }
    }
#endif
}
