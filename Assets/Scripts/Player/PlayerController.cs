using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    Vector2 _moveVector;

    [SerializeField] float _moveSpeed = 1.0f;

    [SerializeField] float _fuelForJetPack = 1000f;
    [SerializeField] float _jetPackForce = 100f;
    float _timer;

    Vector2 _mousePos;

    Camera _cam;

    bool _canMove = true;
    public bool isInShip = false;

    void Start()
    {
        _cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        if (isInShip) return;
        SetMovementParameters();

        Move();

        FlyOnJetpack();
    }

    void SetMovementParameters()
    {
        _moveVector.x = Input.GetAxis("Horizontal");

        _moveVector.y = rb.velocity.y;

        _mousePos = Input.mousePosition;
    }

    void Move()
    {
        if (!_canMove) return;
        float speedCoefficient = 1f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedCoefficient = 1.3f;
        }

        rb.velocity = new Vector2(_moveVector.x * _moveSpeed * speedCoefficient, _moveVector.y);
    }

    void FlyOnJetpack()
    {
        if (Input.GetKey(KeyCode.Space) && _fuelForJetPack > 0)
        {
            rb.AddForce(Vector2.up * _jetPackForce);
            _fuelForJetPack -= 12;
            _timer = 1;
            return;
        }

        if(_timer > 0)   _timer -= Time.deltaTime;


        if (_fuelForJetPack < 1000f && _timer < 0)
        {
            _fuelForJetPack += 10;
        }
    }

    void InetractWithShip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInShip)
            {
                Inventory.fuelInShip += Inventory.fuelInHands;
                Inventory.metalInShip += Inventory.metalInHands;
                Inventory.fuelInHands = 0;
                Inventory.metalInHands = 0;

                EventManager.onValueChanged.Invoke();

                isInShip = true;
                gameObject.SetActive(false);
                return;
            }
        }
    }

    void FlyToNextPlanet()
    {
        if (isInShip && Input.GetKeyDown(KeyCode.Space) && Inventory.fuelInShip > PlanetGameManager.instance.fuelRequiredToLeavePlanet)
        {
            Inventory.fuelInShip -= PlanetGameManager.instance.fuelRequiredToLeavePlanet;
            EventManager.onValueChanged.Invoke();
            Debug.Log("You left this planet for another one");
            SceneManager.LoadScene("SpaceLevel");
        }
    }

    void CollectResources(Resource res)
    {
        if (Inventory.fuelInHands >= 5 && res.TryGetComponent<FuelResource>(out _))
        {
            //nenene
            return;
        }
        if (Inventory.metalInHands >= 15 && res.TryGetComponent<MetalResource>(out _))
        {
            //nenene
            return;
        }
        res.Collect();
    }

    void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D coll in colliders)
        {
            if (coll.TryGetComponent<Resource>(out Resource res))
            {
                CollectResources(res);
            }

            if (coll.TryGetComponent<SpaceshipOnPlanet>(out SpaceshipOnPlanet ship))
            {
                InetractWithShip();
            }
        }
    }
}
