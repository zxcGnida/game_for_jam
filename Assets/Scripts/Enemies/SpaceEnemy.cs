using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemy : Enemy
{
    void Start()
    {

    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down;
    }
}
