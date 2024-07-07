using UnityEngine;

public class Mover : MonoBehaviour
{
    private int _speed = 1;
    private Vector3 _direction;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}