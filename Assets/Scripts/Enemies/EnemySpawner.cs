using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyProbability> enemyFactoriesWithProbs = new List<EnemyProbability>();
    List<EnemyFactory> enemyFactories = new List<EnemyFactory>();
    List<IEnemy> enemies = new List<IEnemy>();

    //[SerializeField] ScoreScript scoreScript;
    [SerializeField] Transform player;

    float timer = 0;
    [Range(0.5f, 5f)]
    [SerializeField] float spawnInterval; 

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

    private void FixedUpdate()
    {
        timer += 0.01f;
        if (timer >= spawnInterval)
        {
            IEnemy enemy = getRandomEnemy();
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-10, 10), 1.75f, transform.position.z + Random.Range(-10, 10));
            enemy.positionAndRotation(spawnPosition, Quaternion.identity);
            //enemy.EnemyHP.onDeath.AddListener(scoreScript.scoreUp);
            enemies.Add(enemy);
            timer = 0;
        }

        foreach (var enem in enemies)
        {
            enem.Player = player;
        }
    }
}
