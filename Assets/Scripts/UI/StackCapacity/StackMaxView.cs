using UnityEngine;

public class StackMaxView : StackUIView
{
    [SerializeField] private GameObject _maxText;
    [SerializeField] private float _offsetY;

    private void Awake()
    {
        _maxText.SetActive(false);
    }

    protected override void Render(int currentCount, int capacity, float topPositionY)
    {
        if (currentCount == capacity)
        {
            if (_maxText.activeSelf == false)
            {
                _maxText.SetActive(true);
                transform.position = new Vector3(transform.position.x, topPositionY + _offsetY, transform.position.z);
            }
        }
        else
        {
            if (_maxText.activeSelf)
            {
                _maxText.SetActive(false);
            }
        }
    }
}
