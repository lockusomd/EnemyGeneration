using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<Limiter>(out Limiter component))
        {
            Died?.Invoke(GetComponent<Enemy>());
        }
    }
}
