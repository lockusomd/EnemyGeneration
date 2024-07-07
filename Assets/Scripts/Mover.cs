using UnityEngine;

public class Mover : MonoBehaviour
{
    private int _speed = 1;

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
}
