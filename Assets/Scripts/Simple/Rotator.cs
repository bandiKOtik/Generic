using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotateDirection;
    private Transform _transform;

    private void Awake() => _transform = GetComponent<Transform>();

    private void FixedUpdate()
    {
        _transform.Rotate( _rotateDirection );
    }
}
