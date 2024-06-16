using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceGameManager : MonoBehaviour
{
    public static SpaceGameManager instance;

    [SerializeField] Transform _enemySpawnPos1;

    [SerializeField] Transform _enemySpawnPos2;

    [SerializeField] Enemy _enemy;

    float timeBetweenEnemies;

    float _enemiesToSpawn;

    float _enemiesOnMap;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _enemiesToSpawn = LevelParameters.RandomizeEnemiesNumber();
    }

    void Update()
    {
        timeBetweenEnemies -= Time.deltaTime;
        Debug.Log(_enemiesToSpawn);
        if (_enemiesToSpawn != 0 && timeBetweenEnemies <= 0)
        {
            _enemiesToSpawn--;
            timeBetweenEnemies = 2;
            GameObject enemyClone = Instantiate(_enemy.gameObject, new Vector2(Random.Range(_enemySpawnPos1.position.x, _enemySpawnPos2.position.x), Random.Range(_enemySpawnPos1.position.y, _enemySpawnPos2.position.y)), Quaternion.identity);
            _enemiesOnMap++;
            Debug.Log("Spawned");
        }
    }

    public void EnemyDied()
    {
        _enemiesOnMap--;
        if(_enemiesOnMap == 0)
        {
            SceneManager.LoadScene("PlanetLevel");
            LevelParameters.StartNextIteration();
        }
    }
}
