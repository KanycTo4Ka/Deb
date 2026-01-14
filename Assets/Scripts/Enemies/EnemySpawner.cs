using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyProbability> enemyFactoriesWithProbs = new List<EnemyProbability>();
    List<EnemyFactory> enemyFactories = new List<EnemyFactory>();
    List<IEnemy> enemies = new List<IEnemy>();

    [Range(1, 100)]
    [SerializeField] int enemiesCount = 50;

    //[SerializeField] ScoreScript scoreScript;
    [SerializeField] Transform player;

    //public IEnemy getRandomEnemy()
    //{
    //    float probabilitySum = 0;

    //    foreach (var enemy in enemyFactoriesWithProbs)
    //        probabilitySum += enemy.probability;

    //    foreach (var enemy in enemyFactoriesWithProbs)
    //        enemy.probability = Mathf.Floor((enemy.probability / probabilitySum) * 100);

    //    foreach (var enemy in enemyFactoriesWithProbs)
    //        for (int i = 0; i < enemy.probability; i++)
    //            enemyFactories.Add(enemy.factory);
    //    return enemyFactories[Random.Range(0, enemyFactories.Count)].getEnemy();
    //}

    public void SpawnEnemies(Maze maze, Vector2 cellSize, Transform mazeRoot)
    {
        enemies.Clear();

        for (int i = 0; i < enemiesCount; i++)
        {
            int x = Random.Range(0, maze.cells.GetLength(0));
            int z = Random.Range(0, maze.cells.GetLength(1));

            Vector3 worldPos = new Vector3(x * cellSize.x, 0, z * cellSize.y);

            if (!NavMesh.SamplePosition(worldPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                continue;

            EnemyFactory factory = GetRandomFactory();
            IEnemy enemy = factory.getEnemy(hit.position, Quaternion.identity);

            enemy.Player = player;
            enemies.Add(enemy);
        }
    }

    EnemyFactory GetRandomFactory()
    {
        float total = 0;
        foreach (var e in enemyFactoriesWithProbs)
            total += e.probability;

        float rnd = Random.Range(0, total);

        foreach (var e in enemyFactoriesWithProbs)
        {
            rnd -= e.probability;
            if (rnd <= 0)
                return e.factory;
        }

        return enemyFactoriesWithProbs[0].factory;
    }

    private void Update()
    {
        foreach (var enem in enemies)
        {
            enem.Player = player;
        }
    }
}
