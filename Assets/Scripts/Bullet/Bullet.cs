using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;

    [SerializeField] float _speed;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * _speed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage();
            Destroy(gameObject);
        }
    }
}
