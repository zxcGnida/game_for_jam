using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEnemy : Enemy
{
    [SerializeField] public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = -(transform.position - target.position).normalized * _moveSpeed;
    }
}
