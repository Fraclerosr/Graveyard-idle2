using UnityEngine;

public class SquareZone : WorldZone
{
    [SerializeField] private Vector2 _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 1, _size.y));
    }

    public override Vector3 GetPoint()
    {
        var x = Random.Range(-_size.x / 2, _size.x / 2);
        var z = Random.Range(-_size.y / 2, _size.y / 2);

        return transform.position + new Vector3(x, 0, z);
    }
}
