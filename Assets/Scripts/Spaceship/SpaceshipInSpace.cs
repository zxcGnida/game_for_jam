using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInSpace : MonoBehaviour
{
    [SerializeField] Camera cam;

    float _maxHp;
    float _currentHp;

    Player _player;
    [SerializeField] GameObject _bullet;

    [SerializeField] Transform _shootPoint;

    float _attackCD = 0;

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((cam.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x) * 2, 2 * (cam.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y));
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (_attackCD > 0)
        {
            _attackCD -= Time.deltaTime;
            return;
        }


        if (Inventory.metalInShip <= 0)
        {
            Debug.Log("NO AMMO");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject bulletClone = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
            bulletClone.GetComponent<Bullet>().direction = Vector2.up;
            Inventory.metalInShip--;
            EventManager.onValueChanged.Invoke();
            _attackCD = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _currentHp -= enemy.collisionDamage;
        }
    }

    public void TakeDamage()
    {
        Inventory.health--;
    }
}
