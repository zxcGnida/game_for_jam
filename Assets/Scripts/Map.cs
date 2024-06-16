using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Player _player;

    [SerializeField] GameObject _locationPrefab;

    [SerializeField] Resource[] _resources;

    [SerializeField] Vector2 _leftestPointOfWorld;

    [SerializeField] Vector2 _rightestPointOfWorld;

    [SerializeField] float _sellSize;

    void Start()
    {
        _locationPrefab.transform.localScale = new Vector3(_sellSize, _sellSize, _sellSize);

        _rightestPointOfWorld.x -= _sellSize;
        _leftestPointOfWorld.x += _sellSize;
        _rightestPointOfWorld.y += _sellSize;
        _leftestPointOfWorld.y += _sellSize;

        LoadWorldToSide(true, 20);
        LoadWorldToSide(false, 20);
        _player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        if(_player.transform.position.x - 10*_sellSize < _leftestPointOfWorld.x)
        {
            LoadWorldToSide(false, 20f);
        }

        if(_player.transform.position.x + 10*_sellSize > _rightestPointOfWorld.x)
        {
            LoadWorldToSide(true, 20f);
        }
    }

    void LoadWorldToSide(bool loadToRight, float iterations)
    {
        for (int i = 0; i < iterations / _sellSize; i++)
        {
            float height = 0;
            int randomValue = Random.Range(1, 100);
            if (randomValue < 60)
            {
                height = 0;
            }
            else if (randomValue >= 60 && randomValue < 90)
            {
                if (Random.Range(1, 100) < 50)
                {
                    height = 1*_sellSize;
                }
                else
                {
                    height = -1*_sellSize;
                }
            }
            else
            {
                if (Random.Range(1, 100) < 50)
                {
                    height = 2 * _sellSize;
                }
                else
                {
                    height = -2 * _sellSize;
                }
            }

            Vector2 pointToSpawnTile = Vector2.zero;

            if (loadToRight)
            {
                _rightestPointOfWorld = new Vector2(_rightestPointOfWorld.x + 1f * _sellSize, _rightestPointOfWorld.y + height * _sellSize);

                if (Random.Range(1, 100) < 20f * _sellSize)
                {
                    GameObject resourceClone = Instantiate(_resources[Random.Range(0, _resources.Length)].gameObject, new Vector2(_rightestPointOfWorld.x, _rightestPointOfWorld.y + 1f), Quaternion.identity);
                }

                pointToSpawnTile = _rightestPointOfWorld;
            }
            else
            {
                _leftestPointOfWorld = new Vector2(_leftestPointOfWorld.x - 1f * _sellSize, _leftestPointOfWorld.y + height * _sellSize);

                if (Random.Range(1, 100) < 20f * _sellSize)
                {
                    GameObject resourceClone = Instantiate(_resources[Random.Range(0, _resources.Length)].gameObject, new Vector2(_leftestPointOfWorld.x, _leftestPointOfWorld.y + 1f), Quaternion.identity);
                }

                pointToSpawnTile = _leftestPointOfWorld;
            }

            GameObject mapPart = Instantiate(_locationPrefab, new Vector2(pointToSpawnTile.x, pointToSpawnTile.y), Quaternion.identity);
        }
    }
}