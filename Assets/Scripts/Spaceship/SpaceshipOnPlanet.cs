using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipOnPlanet : MonoBehaviour
{
    [SerializeField]Player _player;

    [SerializeField] GameObject _weapon;

    [SerializeField] GameObject _bullet;

    [SerializeField] Transform _shootPosition;

    Vector2 _shootDirection;

    Camera _cam;

    float _timeBetweenShots;

    void Start()
    {
        _cam = Camera.main;
    }


    void Update()
    {
        if (!_player.GetComponent<PlayerController>().isInShip) return;
        
        RotateWeapon();

        Shoot();
        
        LeaveShip();
    }

    void RotateWeapon()
    {
        _weapon.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(-GetWeaponDirection().x, GetWeaponDirection().y) * Mathf.Rad2Deg;
    }

    void Shoot()
    {
        if (Inventory.metalInShip <= 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_timeBetweenShots <= 0)
            {
                GameObject bullet = Instantiate(_bullet, _shootPosition.position, Quaternion.LookRotation(Vector3.up, GetWeaponDirection()));
                bullet.GetComponent<Bullet>().direction = GetWeaponDirection();
                EventManager.onValueChanged.Invoke();
                _timeBetweenShots = 1f;
                Inventory.metalInShip--;
            }
        }

        _timeBetweenShots -= Time.deltaTime;
    }

    Vector2 GetWeaponDirection()
    {
        Vector2 direction = _cam.ScreenToWorldPoint(Input.mousePosition) - _weapon.transform.position;
        return direction;
    }

    void LeaveShip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.gameObject.SetActive(true);
            _player.GetComponent<PlayerController>().isInShip = false;
        }
    }

    public void TakeDamage()
    {
        Inventory.health--;
    }
}
