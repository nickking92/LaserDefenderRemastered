using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping =true;


    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }
    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesinWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesinWave(WaveConfig waveConfig)
        {
        for (int enemyCount = 0; enemyCount <waveConfig.GetNumberofEnemies(); enemyCount++)
        {
           var newEnemy= Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoint()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        }
    }

