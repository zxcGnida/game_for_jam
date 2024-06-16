using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelParameters
{
    public static int iteration = 1;

    public static int wave = 1;

    public static int RandomizeEnemiesNumber()
    {
        return Random.Range(1 + iteration, (1 + iteration + wave) * iteration * wave);
    }

    public static void StartNextIteration()
    {
        iteration++;
        wave = 1;
    }

    public static void StartNextWave()
    {
        wave++;
    }
}
