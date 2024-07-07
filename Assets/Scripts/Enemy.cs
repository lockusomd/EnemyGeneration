using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action<GameObject> Died;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<Limiter>(out Limiter component))
        {
            Died?.Invoke(gameObject);
        }
    }
}
