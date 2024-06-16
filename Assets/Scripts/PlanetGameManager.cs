using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGameManager : MonoBehaviour
{
    public static PlanetGameManager instance;

    [SerializeField] Enemy _enemy;

    [SerializeField] Transform _enemySpawnPos1;

    [SerializeField] Transform _enemySpawnPos2;

    [SerializeField] Transform _target;

    int currentCycle;

    public float timeTillEnemies = 70;

    public float fuelRequiredToLeavePlanet;


    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        fuelRequiredToLeavePlanet = 5 * LevelParameters.iteration;
        timeTillEnemies = 20;
        EventManager.onEnemyDied.AddListener(EnemyDied);
    }

    void Update()
    {
        timeTillEnemies -= Time.deltaTime;
        EventManager.onValueChanged.Invoke();
        if (timeTillEnemies < 0)
        {
            timeTillEnemies = 60;

            int enemiesNumber = LevelParameters.RandomizeEnemiesNumber();
            for (int i = 0; i < enemiesNumber; i++)
            {
                GameObject enemyClone = Instantiate(_enemy.gameObject, new Vector2(Random.Range(_enemySpawnPos1.position.x, _enemySpawnPos2.position.x), Random.Range(_enemySpawnPos1.position.y, _enemySpawnPos2.position.y)), Quaternion.identity);
                enemyClone.GetComponent<PlanetEnemy>().target = _target;
            }
            LevelParameters.StartNextWave();
        }
    }


    public void EnemyDied()
    {
        
    }
}