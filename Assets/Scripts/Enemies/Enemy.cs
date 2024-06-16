using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    public float collisionDamage;

    float health = 2;

    [SerializeField] protected float _moveSpeed;

    public void TakeDamage()
    {
        health--;
        if (health == 0)
            Die();
    }

    protected void Die()
    {
        EventManager.onEnemyDied.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.TryGetComponent<SpaceshipOnPlanet>(out SpaceshipOnPlanet pship))
        {
            pship.TakeDamage();
            Destroy(gameObject);
        }

        if (coll.TryGetComponent<SpaceshipInSpace>(out SpaceshipInSpace sship))
        {
            sship.TakeDamage();
            Destroy(gameObject);
        }
    }
}
