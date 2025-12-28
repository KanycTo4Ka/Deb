using UnityEngine;

public class ExplodingEnemyFactory : EnemyFactory
{
    [SerializeField] GameObject explodingEnemyPrefab;

    public override IEnemy getEnemy()
    {
        GameObject explodingEnemy = Instantiate(explodingEnemyPrefab);

        return explodingEnemy.GetComponent<ExplodingEnemy>();
    }
}
