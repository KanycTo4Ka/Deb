using UnityEngine;

public class ExplodingEnemyFactory : EnemyFactory
{
    [SerializeField] GameObject explodingEnemyPrefab;

    public override IEnemy getEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject explodingEnemy = Instantiate(explodingEnemyPrefab, position, rotation);

        return explodingEnemy.GetComponent<ExplodingEnemy>();
    }
}
