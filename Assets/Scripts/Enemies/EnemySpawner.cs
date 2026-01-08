using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyProbability> enemyFactoriesWithProbs = new List<EnemyProbability>();
    List<EnemyFactory> enemyFactories = new List<EnemyFactory>();
    List<IEnemy> enemies = new List<IEnemy>();

    [Range(1, 100)]
    [SerializeField] int enemiesCount = 50;

    //[SerializeField] ScoreScript scoreScript;
    [SerializeField] Transform player;

    public IEnemy getRandomEnemy()
    {
        float probabilitySum = 0;

        foreach (var enemy in enemyFactoriesWithProbs)
            probabilitySum += enemy.probability;

        foreach (var enemy in enemyFactoriesWithProbs)
            enemy.probability = Mathf.Floor((enemy.probability / probabilitySum) * 100);

        foreach (var enemy in enemyFactoriesWithProbs)
            for (int i = 0; i < enemy.probability; i++)
                enemyFactories.Add(enemy.factory);
        return enemyFactories[Random.Range(0, enemyFactories.Count)].getEnemy();
    }

    public void spawnEnemies()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            if (Random.Range(0, 3) == 2)
            {
                IEnemy enemy = getRandomEnemy();
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-10, 10), 1.75f, transform.position.z + Random.Range(-10, 10));
                enemy.positionAndRotation(spawnPosition, Quaternion.identity);
                //enemy.EnemyHP.onDeath.AddListener(scoreScript.scoreUp);
                enemies.Add(enemy);
            }
        }
    }

    private void Update()
    {
        foreach (var enem in enemies)
        {
            enem.Player = player;
        }
    }
}
