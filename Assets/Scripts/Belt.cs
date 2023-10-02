using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] private Material _beltMaterial;
    [SerializeField] private float _speed;

    private float _currentOffsetY;

    private void Update()
    {
        _currentOffsetY += _speed * Time.deltaTime;
        _beltMaterial.SetTextureOffset("_MainTex", new Vector2(_currentOffsetY, 0));
    }
}
